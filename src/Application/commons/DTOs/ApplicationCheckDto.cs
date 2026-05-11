
namespace Application.commons.DTOs
{
    public class ApplicationCheckDto
    {
        public int Score {get; set;}
        public string Verdict {get; set;} = null!;
        public string Reason {get; set;} = null!;
        public string InterviewSuggestion {get; set;} = null!;
    }
}