using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.common.Responses;
using Application.features.Authentication.Commands.Login;
using Application.features.Authentication.Commands.VerifyOtp;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Authentication : ControllerBase
    {
        private readonly IMediator _mediator;
        public Authentication(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginCommand req,
            CancellationToken cancellationToken
        )
        {
            var query = new LoginCommand
            {
                UserName = req.UserName,
                Password = req.Password
            };
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(new APIResponse<object>
            {
                Message = "Otp Code Sent!",
                Status = 200,
                Data = result
            });
        }

        [HttpPost("VerifyOtp")]
        public async Task<IActionResult> VerifyOtp(
            [FromBody] VerifyOtpCommand req,
            CancellationToken cancellationToken
        )
        {
            var query = new VerifyOtpCommand
            {
                Code = req.Code,
                Email = req.Email
            };

            var result = await _mediator.Send(query, cancellationToken);

            Response.Cookies.Append("Refresh_Token", result.RefreshToken, new CookieOptions
            {
                HttpOnly = true,   // JS cannot read it
                Secure   = true,   // HTTPS only
                SameSite = SameSiteMode.Strict, // blocks CSRF
                Expires  = DateTime.UtcNow.AddDays(1)
            });

            return Ok(new APIResponse<object>
            {
                Message = "Success",
                Status = 200,
                Data = new
                {
                    accessToken = result.AccessToken,
                    accessTokenEpiry = result.AccessTokenEpiry
                }
            });
        }
    }
}