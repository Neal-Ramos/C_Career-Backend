using Application.commons.DTOs;
using Application.features.JobApplications.DTOs;
using Application.features.JobApplications.Queries.GetApplications;

namespace Application.commons.IRepository
{
    public interface IApplicationsRepository
    {
        Task<JobApplicationDto> AddApplication(
            string FirstName,
            string MiddleName,
            string LastName,
            string Email,
            string ContactNumber,
            string UniversityName,
            string Degree,
            int GraduationYear,
            string SubmittedFile,
            Guid JobId
        );
        Task<GetJobApplicationsDto> GetApplications(
            int Page,
            int PageSize,
            string? FilterEmail,
            string? FilterJob,
            string? FilterStatus
        );
    }
}