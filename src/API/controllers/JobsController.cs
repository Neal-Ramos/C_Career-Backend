using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.features.Jobs.Queries.GetAllJobs;
using API.common.Responses;
using Application.features.Jobs.Queries.GetJobsById;
using Microsoft.AspNetCore.Authorization;
using Application.features.Jobs.Commands.CreateJob;
using API.contracts.Jobs;
using System.Security.Claims;
using Application.features.Jobs.Commands.UpdateJob;

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
        [HttpGet]
        [AllowAnonymous]
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
                    ["TotalRecords"] = result.TotalRecord,
                    ["TotalPages"] = result.TotalPages
                }
            });
        }
        [HttpGet("{JobId}")]
        [AllowAnonymous]
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
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateJob(
            [FromBody] PostJobRequest req,
            CancellationToken cancellationToken
        ){
            var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier)?? throw new UnauthorizedAccessException();
            var query = new CreateJobCommand
            {
                Title = req.Title,
                Description = req.Description,
                Roles = req.Roles,
                CustomFields = req.CustomFields,
                FileRequirements = req.FileRequirements,
                CreatorId = Guid.Parse(adminId)
            };

            var result = await _mediatR.Send(query, cancellationToken);
            
            return Ok(new APIResponse<object>
            {
                Message = "Job Created!",
                Data = result
            });
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateJob(
            [FromBody] UpdateJobReq req,
            CancellationToken cancellationToken
        ){
            var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier)?? throw new UnauthorizedAccessException();
            var query = new UpdateJobCommand
            {
                JobId= Guid.Parse(req.JobId),
                EditorId= Guid.Parse(adminId),
                Title= req.Title,
                Description = req.Description,
                Roles= req.Roles,
                CustomFields= req.CustomFields,
                FileRequirements= req.FileRequirements,
                EditSummary= req.EditSummary
            };
            var result = await _mediatR.Send(query, cancellationToken);

            return Ok(new APIResponse<object>
            {
                Message = "Job Updated",
                Data = result
            });
        }
    }
}