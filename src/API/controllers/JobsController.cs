using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.features.Jobs.Queries.GetAllJobs;
using API.common.Responses;
using Application.features.Jobs.Queries.GetJobsById;

namespace API.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly IMediator _mediatR;

        public JobsController(IMediator mediatR)
        {
            _mediatR = mediatR;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllJobs(
            CancellationToken cancellationToken,
            [FromQuery] string? Search,
            [FromQuery] int Page = 1,
            [FromQuery] int PageSize = 5
        )
        {
            var query = new GetAllJobsQuery{
                Page = Page,
                PageSize = PageSize,
                Search = Search
            };
            var result = await _mediatR.Send(query, cancellationToken);

            return Ok(new APIResponse<object>
            {
                Data = result.Jobs,
                Meta = new Dictionary<string, object>
                {
                    ["TotalRecord"] = result.TotalRecord,
                    ["TotalPages"] = result.TotalPages
                }
            });
        }

        [HttpGet("{JobId}")]
        public async Task<IActionResult> GetJobsById(
            Guid JobId,
            CancellationToken cancellationToken
        )
        {
            var query = new GetJobsByIdQuery
            {
                JobId = JobId
            };
            var result = await _mediatR.Send(query, cancellationToken);

            return Ok(new APIResponse<object>
            {
                Data = result
            });
        }
    }
}