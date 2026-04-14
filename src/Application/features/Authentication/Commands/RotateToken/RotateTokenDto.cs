using Application.commons.DTOs;

namespace Application.features.Authentication.Commands.RotateToken
{
    public class RotateTokenDto
    {
        public string NewAccessToken {get; set;} = null!;
        public string NewRefreshToken {get; set;} = null!;
    }
}