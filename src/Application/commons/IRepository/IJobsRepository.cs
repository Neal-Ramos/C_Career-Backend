using Domain.Entities;

namespace Application.commons.IRepository
{
    public interface IJobsRepository
    {
        Task<ICollection<Jobs>> GetAllJobs(
            int Page,
            int PageSize,
            string? Search
        );
        Task<int> GetJobsTotal(
            string? Search
        );
        Task<Jobs?> GetJobsById(
            Guid JobId
        );
    }
}