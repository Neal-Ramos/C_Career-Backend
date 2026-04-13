
namespace Application.commons.DTOs
{
    public class ApplicationWithRelationsDto: ApplicationDto
    {
        public Guid JobId {get; set;}
        public JobDto Job {get; set;} = null!;
    }
}