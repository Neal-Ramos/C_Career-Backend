
namespace Application.commons.DTOs
{
    public class ApplicationWithRelationsDto: ApplicationDto
    {
        public JobDto Job {get; set;} = null!;
        public ICollection<ApplicantInterviewsDto>? Interviews{get; set;}
    }
}