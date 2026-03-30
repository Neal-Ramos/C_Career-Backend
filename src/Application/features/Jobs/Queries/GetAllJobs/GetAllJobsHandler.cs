

using Application.commons.IRepository;
using Application.features.Jobs.DTOs;
using MediatR;

namespace Application.features.Jobs.Queries.GetAllJobs
{
    public class GetAllJobsHandler: IRequestHandler<GetAllJobsQuery, GetAllJobsDto>
    {
        public readonly IJobsRepository _jobsRepository;

        public GetAllJobsHandler(IJobsRepository jobsRepository)
        {
            _jobsRepository = jobsRepository;
        }

        public async Task<GetAllJobsDto> Handle(GetAllJobsQuery req, CancellationToken cancellationToken)
        {
            var result = await _jobsRepository.GetAllJobs(
                Page:req.Page,
                PageSize:req.PageSize,
                Search: req.Search
            );
            return result;
        }
    }
}