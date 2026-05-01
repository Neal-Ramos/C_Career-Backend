using Application.commons.DTOs;

namespace Application.features.Authentication.Commands.OAuthLogin
{
    public class OAuthLoginDto
    {
        public AdminAccountDto AdminAccount {get; set;} = null!;
        public string RefreshToken {get; set;} = null!;
        public string AccessToken {get; set;} = null!;
        public string Message {get; set;} = "";
    }
}