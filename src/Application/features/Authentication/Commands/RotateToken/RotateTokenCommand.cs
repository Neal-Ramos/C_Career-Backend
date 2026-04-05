using MediatR;

namespace Application.features.Authentication.Commands.RotateToken
{
    public class RotateTokenCommand: IRequest<RotateTokenDto>
    {
        public string UsedAccessToken {get; set;} = null!;
        public string UsedRefreshToken {get; set;} = null!;
        public string ClaimsAdminId {get; set;} = null!;
        public string ClaimsRole {get; set;} = null!;
    }
}