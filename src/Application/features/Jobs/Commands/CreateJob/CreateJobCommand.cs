
using Application.commons.DTOs;
using Domain.enums;
using MediatR;

namespace Application.features.Jobs.Commands.CreateJob
{
    public class CreateJobCommand: IRequest<JobDto>
    {
        public string Title {get; set;} = null!;
        public string? Description {get; set;}
        public string Roles {get; set;} = null!;
        public string CustomFields {get; set;} = null!;
        public string? FileRequirements {get; set;}
        public Guid CreatorId {get; set;}
        public string? Salary {get; set;}
        public EmploymentTypeEnum EmploymentType {get; set;}
        public WorkArrangementEnum WorkArrangement {get; set;}
    }
}