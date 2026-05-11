using API.common.Responses;
using Application.features.AI.Queries.AnalyzeApplication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AIController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AIController(
            IMediator mediator
        )
        {
            _mediator = mediator;
        }
        [HttpPost("AnalyzeApplication")]
        [AllowAnonymous]
        public async Task<IActionResult> AnalyzeApplication(
            [FromBody] AnalyzeApplicationQuery req,
            CancellationToken cancellationToken
        )
        {
            var result = await _mediator.Send(req, cancellationToken);

            return Ok(new APIResponse<object>
            {
                Data = result
            });
        }
    }
}