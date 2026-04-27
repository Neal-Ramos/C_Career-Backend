using System.ComponentModel.DataAnnotations;
using Domain.enums;

namespace API.contracts.Jobs
{
    public class PostJobRequest
    {
        [Required]
        public string Title {get; set;} = null!;
        [Required]
        public string Description {get; set;} = null!;
        [Required]
        public string Roles {get; set;} = null!;
        public string CustomFields {get; set;} = null!;
        public string FileRequirements {get; set;}  = null!;
        public string? Salary {get; set;}
        public EmploymentTypeEnum EmploymentType {get; set;}
        public WorkArrangementEnum WorkArrangement {get; set;}
    }
}