
using Domain.enums;

namespace Domain.Entities
{
    public class ApplicantInterviews
    {
        public int Id {get; private set;}
        public Guid InterviewId {get; set;}
        public DateTime DateInterview {get; set;}
        public DateTime DateCreated {get; set;}
        public ApplicantsInterviewStatus Status {get; set;} = ApplicantsInterviewStatus.Pending;

        //relations
        public Guid? ApplicationId {get; set;}
        public Applications? Application {get; set;}
    }
}