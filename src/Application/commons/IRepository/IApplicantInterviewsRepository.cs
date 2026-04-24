
using Domain.Entities;
using Domain.enums;

namespace Application.commons.IRepository
{
    public interface IApplicantInterviewsRepository
    {
        Task<ApplicantInterviews> AddAsync(
            DateTime DateInterview,
            DateTime DateCreated,
            Guid ApplicationId
        );
        Task<ApplicantInterviews?> GetByStatusAndApplicationId(
            Guid ApplicationId,
            ApplicantsInterviewStatus Status
        );
        Task<ApplicantInterviews?> GetById(
            Guid InterviewId
        );
    }
}