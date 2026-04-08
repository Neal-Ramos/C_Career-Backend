
namespace API.contracts.Jobs
{
    public class PostJobRequest
    {
        public string Title {get; set;} = null!;
        public string? Description {get; set;}
        public string Roles {get; set;} = null!;
        public string CustomFields {get; set;} = null!;
        public string? FileRequirements {get; set;}
    }
}