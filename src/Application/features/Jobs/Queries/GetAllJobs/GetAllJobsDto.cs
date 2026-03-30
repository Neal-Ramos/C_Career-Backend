using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.features.Jobs.DTOs;

namespace Application.features.Jobs.Queries.GetAllJobs
{
    public class GetAllJobsDto
    {
        public List<JobsDto> Jobs {get; set;} = [];
        public int TotalPages {get; set;}
        public int TotalRecords {get; set;}
    }
}