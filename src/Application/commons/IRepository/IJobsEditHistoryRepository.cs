namespace Application.commons.IRepository
{
    public interface IJobsEditHistoryRepository
    {
        Task AddAsync(
            Guid EditorId,
            Guid JobId,
            DateTime DateEdited,
            string? EditSummary
        );
    }
}