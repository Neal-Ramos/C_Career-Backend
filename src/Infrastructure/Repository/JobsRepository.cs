using Application.commons.IRepository;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class JobsRepository: IJobsRepository
    {
        private readonly AppDbContext _context;
        public JobsRepository (AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task<ICollection<Jobs>> GetAllJobs(
            int Page,
            int PageSize,
            string? Search,
            bool IsDeleted = false
        )
        {
            var query = _context.Jobs.AsQueryable();
            if(Search != null) query = query.Where(a => a.Title.StartsWith(Search));

            return await query
            .Where(j => j.IsDeleted == IsDeleted)
            .OrderBy(j => j.Id)
            .Skip((Page - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();
        }
        public async Task<int> GetJobsTotal(
            string? Search,
            bool IsDeleted = false
        )
        {
            var query = _context.Jobs.Where(j => j.IsDeleted == IsDeleted).AsQueryable();
            if(Search != null) query = query.Where(a => a.Title.Contains(Search));

            return await _context.Jobs.CountAsync();
        }
        public async Task<Jobs?> GetJobsById(
            Guid JobId
        )
        {
            return await _context.Jobs.FirstOrDefaultAsync(j => j.JobId == JobId);
        }
        public async Task<Jobs> CreateJob(
            string Title,
            string? Description,
            string Roles,
            string? FileRequirements,
            Guid CreatorId,
            DateTime DateCreated,
            string CustomFields
        )
        {
            var newJob = new Jobs
            {
                Title = Title,
                Description = Description,
                Roles = Roles,
                FileRequirements = FileRequirements,
                DateCreated = DateCreated,
                AdminId = CreatorId,
                CustomFields = CustomFields
            };
            await _context.Jobs.AddAsync(newJob);

            return newJob;
        }
        public async Task<Jobs?> DeleteJobByGuid(
            Guid JobId
        )
        {
            var job = await _context.Jobs.FirstOrDefaultAsync(a => a.JobId == JobId);
            if(job != null) _context.Jobs.Remove(job);
            
            return job;
        }
    }
}