namespace Domain.Entities
{
    public class Jobs
    {
        public int Id {get; private set;}
        public Guid JobId {set; get;} = Guid.NewGuid();
        public string Title {set; get;} = null!;
        public string? Description {set; get;}
        public string Roles {set; get;} = "[]";
        public string CustomFields {get; set;} = "[]";
        public string? FileRequirements {set; get;}
        public DateTime DateCreated {set; get;}
        public bool IsDeleted {get; set;} = false;

        //
        public Guid? AdminId {get; set;}
        public AdminAccounts? AdminAccounts {get; set;} = null!;
        public ICollection<Applications>? JobApplications {get; set;}
        public ICollection<JobsEditHistory>? EditHistory {get; set;}
    }
}