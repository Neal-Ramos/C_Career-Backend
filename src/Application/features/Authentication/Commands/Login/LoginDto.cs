using Application.commons.DTOs;

namespace Application.features.Authentication.Commands.Login
{
    public class LoginDto
    {
        public AdminAccountDto? AdminAccount {get; set;}
        public string? RefreshToken {get; set;}
        public string? AccessToken {get; set;}
        public string Message {get; set;} = "";
    }
}