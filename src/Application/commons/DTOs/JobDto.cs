using Domain.enums;

namespace Application.commons.DTOs
{
    public class JobDto
    {
        public Guid JobId {set; get;}
        public string Title {set; get;} = null!;
        public string? Description {set; get;}
        public string Roles {set; get;} = null!;
        public string CustomFields {get; set;} = null!;
        public string? FileRequirements {set; get;}
        public DateTime DateCreated {set; get;}
        public string? Salary {get; set;}
        public EmploymentTypeEnum EmploymentType {get; set;}
        public WorkArrangementEnum WorkArrangement {get; set;}
    }
}