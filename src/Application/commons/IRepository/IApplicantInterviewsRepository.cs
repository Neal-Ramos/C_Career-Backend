
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
        Task<ApplicantInterviews?> GetByStatusAndOwnerId(
            Guid ApplicationId,
            ApplicantsInterviewStatus Status
        );
    }
}