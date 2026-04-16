using Domain.enums;

namespace Domain.Entities
{
    public class Applications
    {
        public int Id {get; private set;}
        public Guid ApplicationId {get; private set;} = Guid.NewGuid();
        public string FirstName {get; set;} = null!;
        public string LastName {get; set;} = null!;
        public string Email {get; set;} = null!;
        public string ContactNumber {get; set;} = null!;
        public string UniversityName {get; set;} = null!;
        public string Degree {get; set;} = null!;
        public string Location {get; set;} = null!;
        public string? MiddleName {get; set;}
        public int GraduationYear {get; set;}
        public DateTime BirthDate {get; set;}
        public string CustomFields {get; set;} = "[]";
        public string FileSubmitted {get; set;} = null!;
        public ApplicationStatusEnum Status {get; set;} = ApplicationStatusEnum.Pending;
        public DateTime DateSubmitted {get; set;}
        public DateTime? DateReviewed {get; set;}

        //relation
        public Guid JobId {get; set;}
        public Jobs Job {get; set;} = null!;
        public Guid? AdminId {get; set;}
        public AdminAccounts? ProcessedBy{get; set;}
        
    }
}