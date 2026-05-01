using System.Security.Claims;
using API.common.Responses;
using Application.exeptions;
using Application.features.Authentication.Commands.Login;
using Application.features.Authentication.Commands.Logout;
using Application.features.Authentication.Commands.OAuthLogin;
using Application.features.Authentication.Commands.RotateToken;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace API.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly CookieOptions _cookieOptions;
        public AuthenticationController(
            IMediator mediator,
            IOptions<CookieOptions> cookieOptions
        )
        {
            _mediator = mediator;
            _cookieOptions = cookieOptions.Value;
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(
            [FromBody] LoginCommand req,
            CancellationToken cancellationToken
        )
        {
            var result = await _mediator.Send(req, cancellationToken);

            if(result.RefreshToken != null)
            {
                Response.Cookies.Append("Refresh_Token", result.RefreshToken, _cookieOptions);
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
        [HttpPost("OAuthLogin")]
        public async Task<IActionResult> OAuthLogin(
            OAuthLoginCommand req,
            CancellationToken cancellationToken
        )
        {
            var result = await _mediator.Send(req, cancellationToken);

            Response.Cookies.Append("Refresh_Token", result.RefreshToken, _cookieOptions);

            return Ok(new APIResponse<object>
            {
                Message = result.Message,
                Data = result.AdminAccount,
                Meta = new Dictionary<string, object>
                {
                    ["AccessToken"] = result.AccessToken
                }
            });
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

            Response.Cookies.Append("Refresh_Token", result.NewRefreshToken, _cookieOptions);
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
            CancellationToken cancellationToken
        )
        {
            var UsedRefreshToken = Request.Cookies["Refresh_Token"]?? throw new InvalidInputExeption("No Refresh Token was Found!");
            var query = new LogoutCommand
            {
                UsedRefreshToken = UsedRefreshToken
            };
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(new APIResponse<object>
            {
                Message = result.Message
            });
        }
    }
}