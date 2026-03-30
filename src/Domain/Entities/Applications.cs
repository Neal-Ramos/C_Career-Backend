using Domain.enums;

namespace Domain.Entities
{
    public class JobApplications
    {
        public int Id {get; private set;}
        public Guid ApplicationId {get; private set;}
        public string FirstName {get; set;} = null!;
        public string MiddleName {get; set;} = null!;
        public string LastName {get; set;} = null!;
        public string Email {get; set;} = null!;
        public string ContactNumber {get; set;} = null!;
        public string UniversityName {get; set;} = null!;
        public string Degree {get; set;} = null!;
        public int GraduationYear {get; set;}
        public string FileSubmitted {get; set;} = null!;
        public ApplicationStatusEnum Status {get; set;} = ApplicationStatusEnum.Pending;
        public DateTime DateSubmitted {get; set;} = DateTime.UtcNow;
        public DateTime? DateReviewed {get; set;} = null;

        //relation
        public Guid JobId {get; set;}
        public Jobs Job {get; set;} = null!;
    }
}