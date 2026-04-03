using Domain.Entities;

namespace Application.commons.IRepository
{
    public interface IApplicationsRepository
    {
        Task<Applications> AddApplication(
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
        Task<ICollection<Applications>> GetApplications(
            int Page,
            int PageSize
        );
        Task<int> GetApplicationsTotal(
        );
    }
}