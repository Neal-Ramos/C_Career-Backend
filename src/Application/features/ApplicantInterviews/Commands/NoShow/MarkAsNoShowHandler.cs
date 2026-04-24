
using Application.commons.DTOs;
using Application.commons.IRepository;
using Application.exeptions;
using AutoMapper;
using Domain.enums;
using MediatR;

namespace Application.features.ApplicantInterviews.Commands.NoShow
{
    public class MarkAsNoShowHandler: IRequestHandler<MarkAsNoShowCommand, ApplicantInterviewsDto>
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _dbContext;
        private readonly IApplicationsRepository _applicationsRepository;
        private readonly IApplicantInterviewsRepository _applicantInterviewsRepository;
        public MarkAsNoShowHandler(
            IMapper mapper,
            IDbContext dbContext,
            IApplicationsRepository applicationsRepository,
            IApplicantInterviewsRepository applicantInterviewsRepository
        )
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _applicationsRepository = applicationsRepository;
            _applicantInterviewsRepository = applicantInterviewsRepository;
        }
        public async Task<ApplicantInterviewsDto> Handle(
            MarkAsNoShowCommand req,
            CancellationToken cancellationToken
        )
        {
            var application = await _applicationsRepository.GetApplicationByGuid(req.ApplicationId)?? throw new NotFoundExeption("Application Not Found");
            var currInterview = await _applicantInterviewsRepository.GetByStatusAndApplicationId(
                req.ApplicationId, 
                ApplicantsInterviewStatus.Pending
            )?? throw new NotFoundExeption("This Application Has no Scheduled Interview");

            application.Status = ApplicationStatusEnum.Declined;
            application.AdminId = req.AdminId;
            application.InterviewRemarks = "<p><strong>Did Not Show on Interview</strong></p>";
            currInterview.Status = ApplicantsInterviewStatus.NoShow;
            
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ApplicantInterviewsDto>(currInterview);
        }
    }
}