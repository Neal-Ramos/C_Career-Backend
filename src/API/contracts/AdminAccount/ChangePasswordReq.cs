
using System.ComponentModel.DataAnnotations;

namespace API.contracts.AdminAccount
{
    public class ChangePasswordReq
    {
        [Required]
        public string CurrentPassword {get; set;} = null!;
        [Required]
        public string NewPassword {get; set;} = null!;
        [Required]
        public string ConfirmNewPassword {get; set;} = null!;
    }
}