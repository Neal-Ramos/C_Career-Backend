

using Application.features.Jobs.DTOs;
using MediatR;

namespace Application.features.Jobs.Queries.GetAllJobs
{
    public class GetAllJobsQuery: IRequest<GetAllJobsDto>
    {
        public int Page {get; set;}
        public int PageSize {get; set;}
        public string? Search {get; set;}
    }
}