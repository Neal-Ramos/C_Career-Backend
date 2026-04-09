namespace Application.commons.DTOs
{
    public class ApplicationDto
    {
        public Guid ApplicationId {get; set;}
        public string FirstName {get; set;} = null!;
        public string MiddleName {get; set;} = null!;
        public string LastName {get; set;} = null!;
        public string Email {get; set;} = null!;
        public string ContactNumber {get; set;} = null!;
        public string UniversityName {get; set;} = null!;
        public string Degree {get; set;} = null!;
        public string CustomFields {get; set;} = null!;
        public int GraduationYear {get; set;}
        public string FileSubmitted {get; set;} = null!;
        public string Status {get; set;} = null!;
        public DateTime DateSubmitted {get; set;}
        public DateTime? DateReviewed {get; set;} = null;

    }
}