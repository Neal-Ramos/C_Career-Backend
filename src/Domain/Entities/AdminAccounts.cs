namespace Domain.Entities
{
    public class AdminAccounts
    {
        public int Id {get; private set;}
        public Guid AdminId {get; private set;} = Guid.NewGuid();
        public string Email {get; set;} = null!;
        public string UserName {get; set;} = null!;
        public string Password {get; set;} = null!;
        public string FirstName {get; set;} = null!;
        public string LastName {get; set;} = null!;
        public string? MiddleName {get; set;}

        //relations
        public ICollection<AuthCodes>? AuthCodes {get; set;}
        public ICollection<Jobs>? CreatedJobs {get; set;}
        public ICollection<JobsEditHistory>? JobsEditedHistory {get; set;}
        public ICollection<Applications>? ProcessedApplications {get; set;}
    }
}