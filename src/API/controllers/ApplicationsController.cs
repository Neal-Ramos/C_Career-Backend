using API.common.Responses;
using Application.features.Applications.Commands.AddApplication;
using Application.features.Applications.DTOs;
using Application.features.Applications.Queries.GetApplications;
using Application.features.Applications.Queries.GetApplicatonBuGuid;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ApplicationsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddApplication(
            [FromForm] string firstName,
            [FromForm] string? middleName,
            [FromForm] string lastName,
            [FromForm] string email,
            [FromForm] string contactNumber,
            [FromForm] string universityName,
            [FromForm] string degree,
            [FromForm] string graduationYear,
            [FromForm] string customFields,
            [FromForm] Guid jobId,
            CancellationToken cancellationToken
        )
        {
            var SubmittedFile = Request.Form.Files.Select(file => new FileUploadDTO
            {
                FileName = file.FileName,
                ContentType = file.ContentType,
                Content = file.OpenReadStream()
            }).ToList();
            var query = new AddApplicationCommand
            {
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                Email = email,
                ContactNumber = contactNumber,
                UniversityName = universityName,
                Degree = degree,
                CustomFields = customFields,
                GraduationYear = int.Parse(graduationYear),
                SubmittedFile = SubmittedFile,
                JobId = jobId
            };

            var result = await _mediator.Send(query, cancellationToken);
            
            return Ok(new APIResponse<object>
            {
                Message = "Application Submitted!",
                Data = result
            });
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetApplications(
            CancellationToken cancellationToken,
            [FromQuery] int Page = 1,
            [FromQuery] int PageSize = 5
        ){
            var query = new GetApplicationsQuery
            {
                Page = Page,
                PageSize = PageSize
            };

            var result = await _mediator.Send(query, cancellationToken);
            
            return Ok(new APIResponse<object>
            {
                Data = result.Applications,
                Meta = new Dictionary<string, object>
                {
                    ["TotalRecord"] = result.TotalRecord,
                    ["TotalPages"] = result.TotalPages
                }
            });
        }
        [HttpGet("{ApplicationId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetApplicationByGuid(
            Guid ApplicationId,
            CancellationToken cancellationToken
        )
        {
            var query = new GetApplicationByGuidQuery
            {
                ApplicationId = ApplicationId
            };
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(new APIResponse<object>
            {
                Data = result
            });
        }
    }
}