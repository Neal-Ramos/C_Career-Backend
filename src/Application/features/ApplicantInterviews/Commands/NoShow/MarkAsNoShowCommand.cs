
using Application.commons.DTOs;
using MediatR;

namespace Application.features.ApplicantInterviews.Commands.NoShow
{
    public class MarkAsNoShowCommand: IRequest<ApplicantInterviewsDto>
    {
        public Guid ApplicationId {get; set;}
        public Guid AdminId {get; set;}
    }
}