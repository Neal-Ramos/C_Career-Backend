
namespace API.contracts.Jobs
{
    public class UpdateJobReq: PostJobRequest
    {
        public string JobId {get; set;} = null!;
        public string? EditSummary {get; set;}
    }
}