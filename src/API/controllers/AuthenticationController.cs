using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using API.common.Responses;
using Application.commons.Helpers;
using Application.features.Authentication.Commands.Login;
using Application.features.Authentication.Commands.Logout;
using Application.features.Authentication.Commands.RotateToken;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
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
            var response = new APIResponse<object>
            {
                Message = result.Message,
                Data = result.AdminAccount,
            };
            if(result.AccessToken != null) response.Meta = new Dictionary<string, object>
            {
                ["AccessToken"] = result.AccessToken
            };

            return Ok(response);
        }
        [HttpPost("rotateToken")]
        [Authorize]
        public async Task<IActionResult> RotateToken(
            CancellationToken cancellationToken
        )
        {
            var UsedAccessToken = Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            var UsedRefreshToken = Request.Cookies["Refresh_Token"];
            var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = User.FindFirstValue(ClaimTypes.Role);
            
            var query = new RotateTokenCommand
            {
                UsedAccessToken = UsedAccessToken,
                UsedRefreshToken = UsedRefreshToken!,
                ClaimsAdminId = adminId!,
                ClaimsRole = role!,
            };

            var result = await _mediator.Send(query, cancellationToken);

            Response.Cookies.Append("Refresh_Token", result.NewRefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure   = Request.IsHttps,
                SameSite = SameSiteMode.Lax,
                Expires  = DateTime.UtcNow.AddDays(1)
            });

            return Ok(new APIResponse<object>
            {
                Data = new
                {
                    newAccessToken = result.NewAccessToken
                }
            });
        }
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout(
            LogoutCommand req,
            CancellationToken cancellationToken
        )
        {
            var result = await _mediator.Send(req, cancellationToken);

            return Ok(new APIResponse<object>
            {
                Message = result.Message
            });
        }
    }
}