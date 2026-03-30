using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Domain.Entities
{
    public class Jobs
    {
        public int Id {get; set;}
        public Guid JobId {set; get;} = Guid.NewGuid();
        public string Title {set; get;} = null!;
        public string? Description {set; get;}
        public string Roles {set; get;} = JsonSerializer.Serialize("[]");
        public string? FileRequirements {set; get;}
        public DateTime DateCreated {set; get;} = DateTime.UtcNow;
        public string? EditedBy {get; set;}

        //
        public Guid CreatorId {get; set;}
        public AdminAccounts CreatedBy {get; set;} = null!;
        public ICollection<JobApplications>? JobApplications {get; set;}
    }
}