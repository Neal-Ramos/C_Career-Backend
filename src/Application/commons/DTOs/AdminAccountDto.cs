
namespace Application.commons.DTOs
{
    public class AdminAccountDto
    {
        public Guid AdminId {get; private set;}
        public string Email {get; set;} = null!;
        public string UserName {get; set;} = null!;
        public string FirstName {get; set;} = null!;
        public string LastName {get; set;} = null!;
        public string? MiddleName {get; set;}
        public DateTime BirthDate {get; set;}
    }
}