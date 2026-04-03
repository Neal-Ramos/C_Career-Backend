using API.common.Responses;
using Application.features.Applications.Commands.AddApplication;
using Application.features.Applications.DTOs;
using Application.features.Applications.Queries.GetApplications;
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
        public async Task<IActionResult> AddApplication(
            [FromForm] string FirstName,
            [FromForm] string MiddleName,
            [FromForm] string LastName,
            [FromForm] string Email,
            [FromForm] string ContactNumber,
            [FromForm] string UniversityName,
            [FromForm] string Degree,
            [FromForm] string GraduationYear,
            [FromForm] Guid JobId,
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
                FirstName = FirstName,
                MiddleName = MiddleName,
                LastName = LastName,
                Email = Email,
                ContactNumber = ContactNumber,
                UniversityName = UniversityName,
                Degree = Degree,
                GraduationYear = int.Parse(GraduationYear),
                SubmittedFile = SubmittedFile,
                JobId = JobId
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
    }
}