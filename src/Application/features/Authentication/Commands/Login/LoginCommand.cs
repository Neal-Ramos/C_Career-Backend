using MediatR;

namespace Application.features.Authentication.Commands.Login
{
    public class LoginCommand: IRequest<LoginDto>
    {   
        public string Username {get; set;} = null!;
        public string Password {get; set;} = null!;
        public string? OtpCode {get; set;}
    }
}