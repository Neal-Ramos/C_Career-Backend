using System.Security.Claims;
using API.common.Responses;
using API.contracts.Application;
using Application.features.Applications.Commands.AddApplication;
using Application.features.Applications.Commands.PatchApplicationStatus;
using Application.features.Applications.DTOs;
using Application.features.Applications.Queries.GetApplicationByGuidWithRelation;
using Application.features.Applications.Queries.GetApplicationFile;
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
        [AllowAnonymous]
        public async Task<IActionResult> AddApplication(
            [FromForm] string firstName,
            [FromForm] string? middleName,
            [FromForm] string lastName,
            [FromForm] string email,
            [FromForm] string contactNumber,
            [FromForm] string universityName,
            [FromForm] string degree,
            [FromForm] string location,
            [FromForm] string graduationYear,
            [FromForm] DateTime birthDate,
            [FromForm] string customFields,
            [FromForm] Guid jobId,
            CancellationToken cancellationToken
        )
        {
            var SubmittedFile = (await Task.WhenAll(Request.Form.Files.Select(async file =>
            {
                var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                return new FileUploadDTO
                {
                    FileName = file.FileName,
                    ContentType = file.ContentType,
                    Content = memoryStream,
                    Name = file.Name
                };
            }))).ToList();
            var query = new AddApplicationCommand
            {
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                Email = email,
                ContactNumber = contactNumber,
                UniversityName = universityName,
                Degree = degree,
                Location = location,
                CustomFields = customFields,
                BirthDate = birthDate,
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
        [Authorize]
        public async Task<IActionResult> GetApplicationByGuidWithRelation(
            [FromRoute] Guid ApplicationId,
            CancellationToken cancellationToken
        )
        {
            var query = new GetApplicationByGuidWithRelationQuery
            {
                ApplicationId = ApplicationId
            };
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(new APIResponse<object>
            {
                Data = result
            });
        }
        [HttpPatch("{applicationId}")]
        [Authorize]
        public async Task<IActionResult> PatchApplicationStatus(
            [FromRoute] Guid applicationId,
            [FromBody] PatchApplicationStatusReq req,
            CancellationToken cancellationToken
        )
        {
            var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier)?? throw new UnauthorizedAccessException();
            var query = new PatchApplicationStatusCommand
            {
                ApplicationId = applicationId,
                AdminId = Guid.Parse(adminId),
                Status = req.Status
            };

            var result = await _mediator.Send(query, cancellationToken);

            return Ok(new APIResponse<object>
            {
                Message = $"Application is Now {result.Status}",
                Data = result
            });
        }
        [HttpGet("File")]
        [AllowAnonymous]
        public async Task<IActionResult> GetApplicationFile(
            [FromQuery] string PublicId,
            CancellationToken cancellationToken
        )
        {
            var query = new GetApplicationFileQuery
            {
                PublicId = PublicId
            };
            var (Stream, ContentType) = await _mediator.Send(query, cancellationToken);

            return File(Stream, ContentType);
        }
    }
}