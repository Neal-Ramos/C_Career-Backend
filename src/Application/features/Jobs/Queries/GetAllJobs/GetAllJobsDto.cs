using Application.commons.DTOs;

namespace Application.features.Jobs.Queries.GetAllJobs
{
    public class GetAllJobsDto
    {
        public ICollection<JobDto>? Jobs {get; set;}
        public int TotalRecord {get; set;}
        public int TotalPages {get; set;}
    }
}