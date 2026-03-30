using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.commons.DTOs;
using Application.commons.IRepository;
using Application.features.JobApplications.DTOs;
using Application.features.JobApplications.Queries.GetApplications;
using Application.features.Jobs.DTOs;
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
        
        public async Task<JobApplicationDto> AddApplication(
            string FirstName,
            string MiddleName,
            string LastName,
            string Email,
            string ContactNumber,
            string UniversityName,
            string Degree,
            int GraduationYear,
            string FileSubmitted,
            Guid JobId
        )
        {
            var newApplication = new JobApplications
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
                JobId = JobId
            };
            await _context.JobApplications.AddAsync(newApplication);
            _context.SaveChanges();

            return new JobApplicationDto
            {
                Id = newApplication.Id,
                ApplicationId = newApplication.ApplicationId,
                FirstName = newApplication.FirstName,
                MiddleName = newApplication.MiddleName,
                LastName = newApplication.LastName,
                Email = newApplication.Email,
                ContactNumber = newApplication.ContactNumber,
                UniversityName = newApplication.UniversityName,
                Degree = newApplication.Degree,
                GraduationYear = newApplication.GraduationYear,
                FileSubmitted = FileSubmitted,
                Status = newApplication.Status,
                DateSubmitted = newApplication.DateSubmitted,
                DateReviewed = newApplication.DateReviewed,
                JobId = newApplication.JobId
            };
        }
        public async Task<GetJobApplicationsDto> GetApplications(
            int Page,
            int PageSize,
            string? FilterEmail,
            string? FilterJob,
            string? FilterStatus
        )
        {
            var query = _context.JobApplications.AsQueryable();

            var count = await query.CountAsync();
            var applications = await query
            .OrderBy(a => a.Id)
            .Skip((Page - 1) * PageSize)
            .Take(PageSize)
            .Select(a => new JobApplicationDto
            {
                ApplicationId = a.ApplicationId,
                FirstName = a.FirstName,
                MiddleName = a.MiddleName,
                LastName = a.LastName,
                Email = a.Email,
                ContactNumber = a.ContactNumber,
                UniversityName = a.UniversityName,
                Degree = a.Degree,
                GraduationYear = a.GraduationYear,
                FileSubmitted = a.FileSubmitted,
                Status = a.Status,
                DateSubmitted = a.DateSubmitted,
                DateReviewed = a.DateReviewed,
                JobId = a.JobId,
                Job = new JobsDto
                {
                    Id = a.Job.Id,
                    JobId = a.Job.JobId,
                    Title = a.Job.Title,
                    Description = a.Job.Description,
                    Roles = a.Job.Roles,
                    FileRequirements = a.Job.FileRequirements,
                    DateCreated = a.Job.DateCreated
                }
            })
            .ToListAsync();

            return new GetJobApplicationsDto
            {
                Applications = applications,
                TotalPages = (int)Math.Ceiling((double)count / PageSize),
                TotalRecords = count
            };
        }
    }
}