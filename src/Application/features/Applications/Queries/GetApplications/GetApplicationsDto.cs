using Application.commons.DTOs;

namespace Application.features.Applications.Queries.GetApplications
{
    public class GetApplicationsDto
    {
        public ICollection<ApplicationWithRelationsDto>? Applications {get; set;}
        public int TotalRecord {get; set;}
        public int TotalPages {get; set;}
    }
}