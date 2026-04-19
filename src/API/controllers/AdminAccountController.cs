using System.Security.Claims;
using API.common.Responses;
using API.contracts.AdminAccount;
using Application.features.AdminAccounts.Commands.UpdateAdminAccount;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminAccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AdminAccountController(
            IMediator mediator
        )
        {
            _mediator = mediator;
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateAdminAccount(
            [FromBody] UpdateAdminAccountReq req,
            CancellationToken cancellationToken
        )
        {
            var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier)?? throw new UnauthorizedAccessException();
            var query = new UpdateAdminAccountCommand
            {
                AdminId = Guid.Parse(adminId),
                Email = req.Email,
                UserName = req.UserName,
                FirstName = req.FirstName,
                LastName = req.LastName,
                MiddleName = req.MiddleName,
                BirthDate = req.BirthDate,
            };

            var result = await _mediator.Send(query, cancellationToken);

            return Ok(new APIResponse<object>
            {
                Message = "Account Updated!",
                Data = result
            });
        }
    }
}