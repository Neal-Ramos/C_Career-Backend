using Application.commons.IRepository;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class JobApplicationsRepository: IApplicationsRepository
    {
        private readonly AppDbContext _context;
        public JobApplicationsRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        
        public async Task<Applications> AddApplication(
            string FirstName,
            string? MiddleName,
            string LastName,
            string Email,
            string ContactNumber,
            string UniversityName,
            string Degree,
            int GraduationYear,
            string FileSubmitted,
            DateTime DateSubmitted,
            string CustomFields,
            Guid JobId
        )
        {
            var newApplication = new Applications
            {
                FirstName = FirstName,
                MiddleName = MiddleName,
                LastName = LastName,
                Email = Email,
                ContactNumber = ContactNumber,
                UniversityName = UniversityName,
                Degree = Degree,
                GraduationYear = GraduationYear,
                FileSubmitted = FileSubmitted,
                DateSubmitted = DateSubmitted,
                CustomFields = CustomFields,
                JobId = JobId
            };
            await _context.Applications.AddAsync(newApplication);

            return newApplication;
        }
        public async Task<ICollection<Applications>> GetApplications(
            int Page,
            int PageSize
        )
        {
            return await _context.Applications.OrderBy(j => j.Id)
                .Skip((Page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
        }
        public async Task<int> GetApplicationsTotal(
        )
        {
            return _context.Applications.Count();
        }
        public async Task<Applications?> GetApplicationByGuid(
            Guid ApplicationId
        )
        {
            return await _context.Applications.FirstOrDefaultAsync(a => a.ApplicationId == ApplicationId);
        }
    }
}