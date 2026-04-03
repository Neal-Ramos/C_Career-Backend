using Application.commons.DTOs;
using MediatR;

namespace Application.features.Jobs.Queries.GetJobsById
{
    public class GetJobsByIdQuery: IRequest<JobDto?>
    {
        public Guid JobId {get; set;}
    }
}