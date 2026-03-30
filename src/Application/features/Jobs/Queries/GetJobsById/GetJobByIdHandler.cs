using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.commons.IRepository;
using Application.features.Jobs.DTOs;
using MediatR;

namespace Application.features.Jobs.Queries.GetJobsById
{
    public class GetJobByIdHandler: IRequestHandler<GetJobsByIdQuery, JobsDto?>
    {
        private readonly IJobsRepository _jobsRepository;

        public GetJobByIdHandler(IJobsRepository jobsRepository)
        {
            _jobsRepository = jobsRepository;
        }
    
        public async Task<JobsDto?> Handle(GetJobsByIdQuery req, CancellationToken cancellationToken)
        {
            var result = await _jobsRepository.GetJobsById(req.JobId);

            return result;
        }
    }
}