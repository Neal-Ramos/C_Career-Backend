
using Application.commons.DTOs;
using Application.commons.Helpers;
using Application.commons.IRepository;
using Application.commons.IServices;
using Application.exeptions;
using AutoMapper;
using Domain.enums;
using MediatR;

namespace Application.features.ApplicantInterviews.Commands.Reschedule
{
    public class RescheduleHandler: IRequestHandler<RescheduleCommand, ApplicantInterviewsDto>
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _dbContext;
        private readonly ISendEmailService _sendEmailService;
        private readonly IApplicationsRepository _applicationsRepository;
        private readonly IApplicantInterviewsRepository _applicantInterviewsRepository;
        public RescheduleHandler(
            IMapper mapper,
            IDbContext dbContext,
            ISendEmailService sendEmailService,
            IApplicationsRepository applicationsRepository,
            IApplicantInterviewsRepository applicantInterviewsRepository
        )
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _sendEmailService = sendEmailService;
            _applicationsRepository = applicationsRepository;
            _applicantInterviewsRepository = applicantInterviewsRepository;
        }
        public async Task<ApplicantInterviewsDto> Handle(
            RescheduleCommand req,
            CancellationToken cancellationToken
        )
        {
            var application = await _applicationsRepository.GetApplicationByGuid(req.ApplicationId)?? throw new NotFoundExeption("Application Does not Exist");
            var currInterview = await _applicantInterviewsRepository.GetByStatusAndApplicationId(
                req.ApplicationId, 
                ApplicantsInterviewStatus.Pending
            )?? throw new NotFoundExeption("This Application Has no Scheduled Interview");
            if(currInterview.Status != ApplicantsInterviewStatus.Pending) throw new InvalidInputExeption("The Applicant is now Interviewed");

            currInterview.Status = ApplicantsInterviewStatus.Rescheduled;

            var newInterview = await _applicantInterviewsRepository.AddAsync(
                DateInterview: req.NewSchedule,
                DateCreated: DateHelper.GetPHTime(),
                ApplicationId: req.ApplicationId
            );

            await _dbContext.SaveChangesAsync(cancellationToken);
            await _sendEmailService.SendEmailAsync(
                To: application.Email,
                Subject: "Your Application is now Reviewed",
                HtmlContent: $"<div><strong>You got an interview on date {req.NewSchedule}</div>"
            );

            return _mapper.Map<ApplicantInterviewsDto>(newInterview);
        }
    }
}