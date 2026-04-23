using Application.commons.DTOs;
using Domain.Entities;
using Domain.enums;

namespace Application.commons.IRepository
{
    public interface IApplicationsRepository
    {
        Task<Applications> AddApplication(
            string FirstName,
            string? MiddleName,
            string LastName,
            string Email,
            string ContactNumber,
            string UniversityName,
            string Degree,
            string Location,
            int GraduationYear,
            DateTime BirthDate,
            string SubmittedFile,
            DateTime DateSubmitted,
            string CustomFields,
            Guid JobId
        );
        Task<ICollection<Applications>> GetApplications(
            int Page,
            int PageSize,
            string? Search,
            ApplicationStatusEnum? FilterStatus,
            string? FilterJobTitle,
            bool IncludeDeletedJob = false
        );
        Task<int> GetApplicationsTotal(
            string? Search,
            ApplicationStatusEnum? FilterStatus,
            string? FilterJobTitle,
            bool IncludeDeletedJob = false
        );
        Task <Applications?> GetApplicationByGuid(
            Guid ApplicationId
        );
        Task <ApplicationWithRelationsDto?> GetApplicationByGuidWithRelation(
            Guid ApplicationId
        );
    }
}