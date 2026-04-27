
using Application.commons.DTOs;
using Domain.enums;
using MediatR;

namespace Application.features.Jobs.Commands.UpdateJob
{
    public class UpdateJobCommand: IRequest<JobDto>
    {
        public Guid JobId {get; set;}
        public Guid EditorId {get; set;}
        public string Title {get; set;} = null!;
        public string Description {get; set;} = null!;
        public string Roles {get; set;} = null!;
        public string CustomFields {get; set;} = null!;
        public string FileRequirements {get; set;} = null!;
        public string? EditSummary {get; set;}
        public string? Salary {get; set;}
        public EmploymentTypeEnum EmploymentType {get; set;}
        public WorkArrangementEnum WorkArrangement {get; set;}
    }
}