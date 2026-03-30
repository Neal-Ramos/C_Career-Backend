using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.features.JobApplications.DTOs;

namespace Application.features.JobApplications.Queries.GetApplications
{
    public class GetJobApplicationsDto
    {
        public List<JobApplicationDto> Applications {get; set;} = [];
        public int TotalPages {get; set;}
        public int TotalRecords {get; set;}
    }
}