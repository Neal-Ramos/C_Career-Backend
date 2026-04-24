
using Application.commons.DTOs;
using MediatR;

namespace Application.features.ApplicantInterviews.Commands.Reschedule
{
    public class RescheduleCommand: IRequest<ApplicantInterviewsDto>
    {
        public Guid ApplicationId {get; set;}
        public DateTime NewSchedule {get; set;}
    }
}