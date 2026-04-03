using Application.commons.DTOs;
using Application.commons.IRepository;
using AutoMapper;
using MediatR;

namespace Application.features.Jobs.Queries.GetJobsById
{
    public class GetJobByIdHandler: IRequestHandler<GetJobsByIdQuery, JobDto?>
    {
        private readonly IJobsRepository _jobsRepository;
        private readonly IMapper _mapper;

        public GetJobByIdHandler(
            IJobsRepository jobsRepository,
            IMapper mapper
        )
        {
            _jobsRepository = jobsRepository;
            _mapper = mapper;
        }
    
        public async Task<JobDto?> Handle(GetJobsByIdQuery req, CancellationToken cancellationToken)
        {
            var job = await _jobsRepository.GetJobsById(req.JobId);

            return _mapper.Map<JobDto>(job);
        }
    }
}