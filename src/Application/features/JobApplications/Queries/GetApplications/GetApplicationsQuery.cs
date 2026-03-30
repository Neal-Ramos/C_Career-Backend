using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.commons.DTOs;
using MediatR;

namespace Application.features.JobApplications.Queries.GetApplications
{
    public class GetApplicationsQuery: IRequest<GetJobApplicationsDto>
    {
        public int Page {get; set;}
        public int PageSize {get; set;}
        public string? FilterEmail {get; set;}
        public string? FilterJob {get; set;}
        public string? FilterStatus {get; set;}
    }
}