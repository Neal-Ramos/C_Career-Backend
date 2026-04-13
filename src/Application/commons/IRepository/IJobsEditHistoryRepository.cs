namespace Application.commons.IRepository
{
    public interface IJobsEditHistoryRepository
    {
        Task AddEditHistory(
            Guid EditorId,
            Guid JobId,
            DateTime DateEdited,
            string? EditSummary
        );
    }
}