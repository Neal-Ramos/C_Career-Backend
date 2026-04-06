
using Application.commons.DTOs;
using MediatR;

namespace Application.features.Jobs.Commands.CreateJob
{
    public class CreateJobCommand: IRequest<JobDto>
    {
        public string Title {get; set;} = null!;
        public string? Description {get; set;}
        public string Roles {get; set;} = null!;
        public string? FileRequirements {get; set;}
        public Guid CreatorId {get; set;}
    }
}