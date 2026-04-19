
using System.ComponentModel.DataAnnotations;

namespace API.contracts.AdminAccount
{
    public class UpdateAdminAccountReq
    {
        [Required]
        public string Email {get; set;} = null!;
        [Required]
        public string UserName {get; set;} = null!;
        [Required]
        public string FirstName {get; set;} = null!;
        [Required]
        public string LastName {get; set;} = null!;
        public string? MiddleName {get; set;}
        [Required]
        public DateTime BirthDate {get; set;}
    }
}