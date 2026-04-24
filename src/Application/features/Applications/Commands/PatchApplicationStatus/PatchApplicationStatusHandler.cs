using Application.commons.DTOs;
using Application.commons.Helpers;
using Application.commons.IRepository;
using Application.commons.IServices;
using Application.exeptions;
using AutoMapper;
using Domain.enums;
using MediatR;

namespace Application.features.Applications.Commands.PatchApplicationStatus
{
    public class PatchApplicationStatusHandler: IRequestHandler<PatchApplicationStatusCommand, ApplicationDto>
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _dbContext;
        private readonly ISendEmailService _sendEmailService;
        private readonly IApplicationsRepository _applicationsRepository;
        private readonly IApplicantInterviewsRepository _applicantInterviewsRepository;
        public PatchApplicationStatusHandler(
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
        public async Task<ApplicationDto> Handle(
            PatchApplicationStatusCommand req,
            CancellationToken cancellationToken
        )
        {
            var application = await _applicationsRepository.GetApplicationByGuid(req.ApplicationId)?? throw new NotFoundExeption();
            var pendingInterview = await _applicantInterviewsRepository.GetByStatusAndApplicationId(
                application.ApplicationId,
                ApplicantsInterviewStatus.Pending
            );

            if(req.Status == ApplicationStatusEnum.Interview && pendingInterview == null)// For Setting Interview
            {
                if(!req.DateInterview.HasValue)throw new InvalidInputExeption("Interview Date Is Required");
                if(application.Status != ApplicationStatusEnum.Pending)throw new ConflictExeption("Only Pending Application is Allowed For Interview");
                application.Status = req.Status;
                await _applicantInterviewsRepository.AddAsync(
                    DateInterview : req.DateInterview.Value,
                    DateCreated : DateHelper.GetPHTime(),
                    ApplicationId : req.ApplicationId
                );
                await _dbContext.SaveChangesAsync(cancellationToken);
                await _sendEmailService.SendEmailAsync(
                    To: application.Email,
                    Subject: "Your Application is now Reviewed",
                    HtmlContent: $"<div><strong>You got an interview on date {req.DateInterview}</div>"
                );
            }
            if(pendingInterview != null && (req.Status == ApplicationStatusEnum.Approved || req.Status == ApplicationStatusEnum.Declined))// For Approving or Declining Application
            {
                if(application.Status != ApplicationStatusEnum.Interview) throw new ConflictExeption("Only Interviewed Applicants Can be Approved or Declined");
                if(application.AdminId != null) throw new ConflictExeption("This Application Is Processed");
                if(pendingInterview.Status == ApplicantsInterviewStatus.Pending)pendingInterview.Status = ApplicantsInterviewStatus.Done;
                application.DateReviewed = DateHelper.GetPHTime();
                application.Status = req.Status;
                application.AdminId = req.AdminId;
                application.InterviewRemarks = req.InterviewRemarks;

                pendingInterview.Status = ApplicantsInterviewStatus.Done;
                await _dbContext.SaveChangesAsync(cancellationToken);
                await _sendEmailService.SendEmailAsync(
                    To: application.Email,
                    Subject: $"Your Application is {req.Status}",
                    HtmlContent: $"<div><strong>Your Application Got {req.Status}</div>"
                );
            }

            return _mapper.Map<ApplicationDto>(application); 
        }
    }
}