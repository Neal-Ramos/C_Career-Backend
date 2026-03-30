
using Application.commons.DTOs;
using Application.features.JobApplications.DTOs;

namespace Application.features.Jobs.DTOs
{
    public class JobsDto
    {
        public int Id {get; set;}
        public Guid JobId {set; get;} = Guid.NewGuid();
        public string Title {set; get;} = null!;
        public string? Description {set; get;}
        public string Roles {set; get;} = null!;
        public string? FileRequirements {set; get;}
        public DateTime DateCreated {set; get;} = DateTime.UtcNow;
        public string? EditedBy {get; set;}

        //
        public Guid CreatorId {get; set;}
        public AdminAccountsDto CreatedBy {get; set;} = null!;
        public ICollection<JobApplicationDto>? JobApplications {get; set;}
    } 
}