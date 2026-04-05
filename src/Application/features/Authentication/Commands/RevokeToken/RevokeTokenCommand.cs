using MediatR;

namespace Application.features.Authentication.Commands.RevokeToken
{
    public class RevokeTokenCommand: IRequest<RevokeTokenDto>
    {
        public string Token {get; set;} = null!;
    }
}