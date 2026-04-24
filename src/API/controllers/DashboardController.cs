using API.common.Responses;
using Application.features.Dashboard.AdminDashboard;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DashboardController(
            IMediator mediator
        )
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AdminDashboard(
            [FromQuery] AdminDashboardCommand req,
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