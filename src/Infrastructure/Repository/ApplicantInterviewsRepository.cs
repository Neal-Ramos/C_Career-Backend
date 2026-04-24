using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.commons.IRepository;
using Domain.Entities;
using Domain.enums;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class ApplicantInterviewsRepository: IApplicantInterviewsRepository
    {
        private readonly AppDbContext _context;
        public ApplicantInterviewsRepository(
            AppDbContext context
        )
        {
            _context = context;
        }

        public async Task<ApplicantInterviews> AddAsync(
            DateTime DateInterview,
            DateTime DateCreated,
            Guid ApplicationId
        )
        {
            var applicantInterviews = new ApplicantInterviews
            {
                DateInterview = DateInterview,
                DateCreated = DateCreated,
                ApplicationId = ApplicationId
            };
            await _context.ApplicantInterviews.AddAsync(applicantInterviews);

            return applicantInterviews;
        }
        public async Task<ApplicantInterviews?> GetByStatusAndApplicationId(
            Guid ApplicationId,
            ApplicantsInterviewStatus Status
        )
        {
            return await _context.ApplicantInterviews.FirstOrDefaultAsync(a => a.Status == Status && a.ApplicationId == ApplicationId);
        }
        public async Task<ApplicantInterviews?> GetById(
            Guid InterviewId
        )
        {
            return await _context.ApplicantInterviews.FirstOrDefaultAsync(a => a.InterviewId == InterviewId);
        }
    }
}