using API.common.Responses;
using Application.features.Authentication.Commands.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthenticationController(
            IMediator mediator
        )
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginCommand req,
            CancellationToken cancellationToken
        )
        {
            var query = new LoginCommand{
                Username = req.Username,
                Password = req.Password,
                OtpCode = req.OtpCode
            };

            var result = await _mediator.Send(query, cancellationToken);

            if(result.RefreshToken != null)
            {
                Response.Cookies.Append("Refresh_Token", result.RefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure   = Request.IsHttps,
                    SameSite = SameSiteMode.Lax,
                    Expires  = DateTime.UtcNow.AddDays(1)
                });
            }

            return Ok(new APIResponse<object>
            {
                Message = result.Message,
                Data = result.AdminAccount,
                Meta = new Dictionary<string, object>
                {
                    ["AccessToken"] = result.AccessToken,
                    ["AccessTokenExpiration"] = result.AccessTokenExpirations
                }
            });
        }
    }
}