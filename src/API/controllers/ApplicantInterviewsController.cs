using System.Security.Claims;
using API.common.Responses;
using API.contracts.ApplicantInterviews;
using Application.features.ApplicantInterviews.Commands.NoShow;
using Application.features.ApplicantInterviews.Commands.Reschedule;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicantInterviewsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ApplicantInterviewsController(
            IMediator mediator
        )
        {
            _mediator = mediator;
        }
        [HttpPatch("{ApplicationId}/Reschedule")]
        [Authorize]
        public async Task<IActionResult> Reschedule(
            [FromRoute] Guid ApplicationId,
            [FromBody] RescheduleReq req,
            CancellationToken cancellationToken
        )
        {
            var query = new RescheduleCommand
            {
                ApplicationId = ApplicationId,
                NewSchedule = req.NewSchedule
            };

            var result = await _mediator.Send(query, cancellationToken);
            return Ok(new APIResponse<object>
            {
                Data = result
            });
        }
        [HttpPatch("{ApplicationId}/NoShow")]
        [Authorize]
        public async Task<IActionResult> NoShow(
            [FromRoute] Guid ApplicationId,
            CancellationToken cancellationToken
        )
        {
            var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier)?? throw new UnauthorizedAccessException();
            var query = new MarkAsNoShowCommand
            {
                ApplicationId = ApplicationId,
                AdminId = Guid.Parse(adminId)
            };
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(new APIResponse<object>
            {
                Data = result
            });
        }
    }
}