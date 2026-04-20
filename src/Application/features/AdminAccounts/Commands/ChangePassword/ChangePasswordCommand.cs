
using MediatR;

namespace Application.features.AdminAccounts.Commands.ChangePassword
{
    public class ChangePasswordCommand: IRequest
    {
        public Guid AdminId {get; set;}
        public string CurrentPassword {get; set;} = null!;
        public string NewPassword {get; set;} = null!;
        public string ConfirmNewPassword {get; set;} = null!;
    }
}