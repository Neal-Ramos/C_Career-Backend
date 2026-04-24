
using Application.commons.IRepository;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repository
{
    public class JobsEditHistoryRepository: IJobsEditHistoryRepository
    {
        private readonly AppDbContext _context;
        public JobsEditHistoryRepository(
            AppDbContext appDbContext
        )
        {
            _context = appDbContext;
        }
        public async Task AddAsync(
            Guid EditorId,
            Guid JobId,
            DateTime DateEdited,
            string? EditSummary
        )
        {
            await _context.AddAsync(new JobsEditHistory
            {
                EditorId = EditorId,
                JobId = JobId,
                DateEdited = DateEdited,
                EditSummary = EditSummary
            });
        }
    }
}