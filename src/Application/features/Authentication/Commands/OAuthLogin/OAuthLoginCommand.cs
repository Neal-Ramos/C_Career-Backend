using MediatR;

namespace Application.features.Authentication.Commands.OAuthLogin
{
    public class OAuthLoginCommand: IRequest<OAuthLoginDto>
    {
        public string Credential {get; set;} = null!;
    }
}