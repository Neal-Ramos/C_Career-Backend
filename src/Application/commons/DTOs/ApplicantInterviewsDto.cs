using Domain.enums;

namespace Application.commons.DTOs
{
    public class ApplicantInterviewsDto
    {
        public int Id {get; private set;}
        public Guid InterviewId {get; set;}
        public DateTime DateInterview {get; set;}
        public DateTime DateCreated {get; set;}
        public ApplicantsInterviewStatus Status {get; set;}
    }
}