
using Application.commons.IRepository;
using Domain.enums;
using MediatR;

namespace Application.features.Dashboard.AdminDashboard
{
    public class AdminDashboardHandler: IRequestHandler<AdminDashboardCommand, AdminDashboardDto>
    {
        private readonly IJobsRepository _jobsRepository;
        private readonly IApplicationsRepository _applicationsRepository;
        public AdminDashboardHandler(
            IJobsRepository jobsRepository,
            IApplicationsRepository applicationsRepository
        )
        {
            _jobsRepository = jobsRepository;
            _applicationsRepository = applicationsRepository;
        }
        public async Task<AdminDashboardDto> Handle(
            AdminDashboardCommand req,
            CancellationToken cancellationToken
        )
        {
            var TotalApplication = _applicationsRepository.CountAsync();
            var TotalPendingApplication = _applicationsRepository.CountAsync(FilterStatus: ApplicationStatusEnum.Pending);
            var TotalAcceptedApplication = _applicationsRepository.CountAsync(FilterStatus: ApplicationStatusEnum.Approved);
            var TotalJobs = _jobsRepository.CountAsync(Search: null);

            await Task.WhenAll(
                TotalApplication,
                TotalPendingApplication,
                TotalAcceptedApplication,
                TotalJobs
            );


            return new AdminDashboardDto
            {
                TotalApplication = await TotalApplication,
                TotalPendingApplication = await TotalPendingApplication,
                TotalAcceptedApplication = await TotalAcceptedApplication,
                TotalJobs = await TotalJobs
            };
        }
    }
}