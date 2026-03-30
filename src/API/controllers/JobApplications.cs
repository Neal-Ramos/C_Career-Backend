using API.common.Responses;
using Application.features.JobApplications.Commands.AddApplication;
using Application.features.JobApplications.DTOs;
using Application.features.JobApplications.Queries.GetApplications;
using Azure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobApplications : ControllerBase
    {
        private readonly IMediator _mediator;
        public JobApplications(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("/AddApplication")]
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
                Message = "Success",
                Status = 200,
                Data = result
            });
        }

        [HttpGet("GetApplications")]
        [AllowAnonymous]
        public async Task<IActionResult> GetApplications(
            [FromQuery] string? FilterEmail,
            [FromQuery] string? FilterJob,
            [FromQuery] string? FilterStatus,
            CancellationToken cancellationToken,
            [FromQuery] int Page = 1,
            [FromQuery] int PageSize = 5
        ){
            var query = new GetApplicationsQuery
            {
                Page = Page,
                PageSize = PageSize,
                FilterEmail = FilterEmail,
                FilterJob = FilterJob,
                FilterStatus = FilterStatus,
            };

            var result = await _mediator.Send(query, cancellationToken);
            return Ok(new APIResponse<object>
            {
                Message = "Success",
                Status = 200,
                Data = result
            });
        }
    }
}