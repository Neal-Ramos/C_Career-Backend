
using MediatR;

namespace Application.features.Authentication.Commands.Logout
{
    public class LogoutCommand: IRequest<LogoutDto>
    {
        public string UsedRefreshToken {get; set;} = null!;
    }
}