using Application.commons.DTOs;
using Application.commons.IRepository;
using AutoMapper;
using MediatR;

namespace Application.features.Jobs.Queries.GetAllJobs
{
    public class GetAllJobsHandler: IRequestHandler<GetAllJobsQuery, GetAllJobsDto>
    {
        public readonly IJobsRepository _jobsRepository;
        public readonly IMapper _mapper;

        public GetAllJobsHandler(
            IJobsRepository jobsRepository,
            IMapper mapper
        )
        {
            _jobsRepository = jobsRepository;
            _mapper = mapper;
        }

        public async Task<GetAllJobsDto> Handle(GetAllJobsQuery req, CancellationToken cancellationToken)
        {
            var jobs = await _jobsRepository.GetAllJobs(
                Page: req.Page,
                PageSize: req.PageSize,
                Search: req.Search
            );
            var jobsTotal = await _jobsRepository.CountAsync(
                Search: req.Search
            );

            return new GetAllJobsDto
            {
                Jobs = _mapper.Map<ICollection<JobDto>>(jobs),
                TotalRecord = jobsTotal,
                TotalPages = (int)Math.Ceiling((double)jobsTotal / req.PageSize)
            };
        }
    }
}