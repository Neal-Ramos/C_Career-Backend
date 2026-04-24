using Domain.Entities;

namespace Application.commons.IRepository
{
    public interface IJobsRepository
    {
        Task<ICollection<Jobs>> GetAllJobs(
            int Page,
            int PageSize,
            string? Search,
            bool IsDeleted = false
        );
        Task<int> CountAsync(
            string? Search,
            bool IsDeleted = false
        );
        Task<Jobs?> GetJobsById(
            Guid JobId
        );
        Task<Jobs> AddAsync(
            string Title,
            string? Description,
            string Roles,
            string? FileRequirements,
            Guid CreatorId,
            DateTime DateCreated,
            string CustomFields
        );
        Task<Jobs?> DeleteJobByGuid(
            Guid JobId
        );
    }
}