using System.Text.Json;
using Application.commons.DTOs;
using Application.commons.Helpers;
using Application.commons.IRepository;
using Application.commons.IServices;
using AutoMapper;
using MediatR;

namespace Application.features.Applications.Commands.AddApplication
{
    public class AddApplicationHandler: IRequestHandler<AddApplicationCommand, ApplicationDto>
    {
        private readonly IApplicationsRepository _applicationsRepository;
        private readonly IStorageService _storageRepository;
        private readonly ISendEmailService _sendEmail;
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public AddApplicationHandler(
            IApplicationsRepository applicationsRepository,
            IStorageService storageRepository,
            ISendEmailService sendEmail,
            IDbContext dbContext,
            IMapper mapper
        )
        {
            _applicationsRepository = applicationsRepository;
            _storageRepository = storageRepository;
            _sendEmail = sendEmail;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApplicationDto> Handle(AddApplicationCommand req, CancellationToken cancellationToken)
        {
            var SubmittedFile = await Task.WhenAll(
                req.SubmittedFile.Select(file =>
                    _storageRepository.UploadAsync(
                        DocumentName: file.Name,
                        FileName: file.FileName,
                        ContentType: file.ContentType,
                        Format: Path.GetExtension(file.FileName).TrimStart('.'),
                        Content: file.Content
                    )
                )
            );

            var newApplication = await _applicationsRepository.AddApplication(
                FirstName: req.FirstName,
                MiddleName: req.MiddleName,
                LastName: req.LastName,
                Email: req.Email,
                ContactNumber: req.ContactNumber,
                UniversityName: req.UniversityName,
                Degree: req.Degree,
                Location: req.Location,
                GraduationYear: req.GraduationYear,
                BirthDate: req.BirthDate,
                SubmittedFile: JsonSerializer.Serialize(SubmittedFile),
                DateSubmitted: DateHelper.GetPHTime(),
                JobId: req.JobId,
                CustomFields: req.CustomFields
            );
            await _sendEmail.SendEmailAsync(
                To: req.Email,
                Subject: "Application Submitted!",
                HtmlContent: "<div><strong>Submit Application<strong> 👋🏻 from .NET</div>"
            );
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ApplicationDto>(newApplication);
        }
    }
}