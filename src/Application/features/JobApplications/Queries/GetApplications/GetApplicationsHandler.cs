using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.commons.DTOs;
using Application.commons.IRepository;
using MediatR;

namespace Application.features.JobApplications.Queries.GetApplications
{
    public class GetApplicationsHandler: IRequestHandler<GetApplicationsQuery, GetJobApplicationsDto>
    {
        private readonly IApplicationsRepository _applications;
        public GetApplicationsHandler(
            IApplicationsRepository applicationsRepository
        )
        {
            _applications = applicationsRepository;
        }

        public async Task<GetJobApplicationsDto> Handle(
            GetApplicationsQuery req,
            CancellationToken cancellationToken
        )
        {
            var result = await _applications.GetApplications(
                Page: req.Page,
                PageSize: req.PageSize,
                FilterEmail: req.FilterEmail,
                FilterJob: req.FilterJob,
                FilterStatus: req.FilterStatus
            );
            return result;
        }
    }
}