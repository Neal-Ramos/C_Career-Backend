using Application.commons.DTOs;
using Application.commons.IRepository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Domain.enums;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class JobApplicationsRepository: IApplicationsRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public JobApplicationsRepository(
            AppDbContext appDbContext,
            IMapper mapper
        )
        {
            _context = appDbContext;
            _mapper = mapper;
        }
        
        public async Task<Applications> AddAsync(
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
                Location = Location,
                GraduationYear = GraduationYear,
                BirthDate = BirthDate,
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
            int PageSize,
            string? Search,
            ApplicationStatusEnum? FilterStatus,
            string? FilterJobTitle,
            bool IncludeDeletedJob = false
        )
        {
            var query = _context.Applications.Where(a => a.Job.IsDeleted == IncludeDeletedJob).AsQueryable();
            if(FilterStatus != null) query = query.Where(a => a.Status == FilterStatus);
            if(!string.IsNullOrEmpty(Search)) query = query.Where(a => a.Email.Contains(Search));
            if(!string.IsNullOrEmpty(FilterJobTitle))query = query.Where(a => a.Job.Title == FilterJobTitle);

            return await query.OrderBy(j => j.Id)
                .Skip((Page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
        }
        public async Task<int> CountAsync(
            string? Search,
            ApplicationStatusEnum? FilterStatus,
            string? FilterJobTitle,
            bool IncludeDeletedJob = false
        )
        {
            var query = _context.Applications.Where(a => a.Job.IsDeleted == IncludeDeletedJob).AsQueryable();
            if(FilterStatus != null) query = query.Where(a => a.Status == FilterStatus);
            if(!string.IsNullOrEmpty(Search)) query = query.Where(a => a.Email.Contains(Search));
            if(!string.IsNullOrEmpty(FilterJobTitle))query = query.Where(a => a.Job.Title == FilterJobTitle);

            return query.Count();
        }
        public async Task<Applications?> GetApplicationByGuid(
            Guid ApplicationId
        )
        {
            return await _context.Applications.FirstOrDefaultAsync(a => a.ApplicationId == ApplicationId);
        }
        public async Task<ApplicationWithRelationsDto?> GetApplicationByGuidWithRelation(
            Guid ApplicationId
        )
        {
            var result = await _context.Applications
                .ProjectTo<ApplicationWithRelationsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(a => a.ApplicationId == ApplicationId);

            if(result?.Interviews != null)
            {
                result.Interviews = [.. result.Interviews.OrderByDescending(i => i.DateCreated)];
            }

            return result;
        }
    }
}