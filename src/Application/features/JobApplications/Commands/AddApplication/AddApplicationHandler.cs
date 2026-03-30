using System.Text.Json;
using Application.commons.IRepository;
using Application.commons.IServices;
using Application.features.JobApplications.DTOs;
using MediatR;

namespace Application.features.JobApplications.Commands.AddApplication
{
    public class AddApplicationHandler: IRequestHandler<AddApplicationCommand, JobApplicationDto>
    {
        private readonly IApplicationsRepository _applicationsRepository;
        private readonly IStorageService _storageRepository;
        private readonly ISendEmailService _sendEmail;

        public AddApplicationHandler(
            IApplicationsRepository applicationsRepository,
            IStorageService storageRepository,
            ISendEmailService sendEmail
        )
        {
            _applicationsRepository = applicationsRepository;
            _storageRepository = storageRepository;
            _sendEmail = sendEmail;
        }

        public async Task<JobApplicationDto> Handle(AddApplicationCommand req, CancellationToken cancellationToken)
        {
            var SubmittedFile = await Task.WhenAll(
                req.SubmittedFile.Select(file =>
                    _storageRepository.UploadAsync(
                        FileName: file.FileName,
                        Name: file.Name,
                        ContentType: file.ContentType,
                        Content: file.Content
                    )
                )
            );

            var result = await _applicationsRepository.AddApplication(
                FirstName: req.FirstName,
                MiddleName: req.MiddleName,
                LastName: req.LastName,
                Email: req.Email,
                ContactNumber: req.ContactNumber,
                UniversityName: req.UniversityName,
                Degree: req.Degree,
                GraduationYear: req.GraduationYear,
                SubmittedFile: JsonSerializer.Serialize(SubmittedFile),
                JobId: req.JobId
            );
            await _sendEmail.SendEmailAsync(
                To: req.Email,
                Subject: "Application Submitted!",
                HtmlContent: "<div><strong>Submit Application<strong> 👋🏻 from .NET</div>"
            );
            return result;
        }
    }
}