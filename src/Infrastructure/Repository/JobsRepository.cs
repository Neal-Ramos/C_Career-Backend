using Application.commons.IRepository;
using Application.features.Jobs.DTOs;
using Application.features.Jobs.Queries.GetAllJobs;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class JobsRepository: IJobsRepository
    {
        private readonly AppDbContext context;
        public JobsRepository (AppDbContext appDbContext)
        {
            context = appDbContext;
        }

        public async Task<GetAllJobsDto> GetAllJobs(
            int Page,
            int PageSize,
            string? Search
        )
        {
            var query = context.Jobs.AsQueryable();
            if (!string.IsNullOrEmpty(Search))query = query.Where(j => j.Title.Contains(Search) || j.Roles.Contains(Search));

            var count = await query.CountAsync();
            var jobs = await query
            .OrderBy(j => j.Id)
            .Skip((Page - 1) * PageSize)
            .Take(PageSize)
            .Select(j => new JobsDto
            {
                JobId = j.JobId,
                Title = j.Title,
                Description = j.Description,
                Roles = j.Roles,
                FileRequirements = j.FileRequirements,
                DateCreated = j.DateCreated,

            }).ToListAsync();

            return new GetAllJobsDto
            {
                Jobs= jobs,
                TotalPages = (int)Math.Ceiling((double)count / PageSize),
                TotalRecords = count
            };
        }

        public async Task<JobsDto?> GetJobsById(
            Guid JobId
        )
        {
            var result = await context.Jobs
                .FirstOrDefaultAsync(j => j.JobId == JobId);
            
            if(result == null)
            {
                return null;
            }
            else
            {
                return new JobsDto
                {
                    Id = result.Id,
                    JobId = result.JobId,
                    Title = result.Title,
                    Description = result.Description,
                    Roles = result.Roles,
                    FileRequirements = result.FileRequirements,
                    DateCreated = result.DateCreated
                };
            }
        }
    }
}