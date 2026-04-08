
using Application.commons.DTOs;
using Application.commons.Helpers;
using Application.commons.IRepository;
using AutoMapper;
using MediatR;

namespace Application.features.Jobs.Commands.CreateJob
{
    public class CreateJobHandler: IRequestHandler<CreateJobCommand, JobDto>
    {
        private readonly IJobsRepository _jobsRepository;
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateJobHandler(
            IJobsRepository jobsRepository,
            IDbContext dbContext,
            IMapper mapper
        )
        {
            _jobsRepository = jobsRepository;
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<JobDto> Handle(
            CreateJobCommand req,
            CancellationToken cancellationToken
        )
        {
            var newJob = await _jobsRepository.CreateJob(
                Title : req.Title,
                Description : req.Description,
                Roles : req.Roles,
                FileRequirements : req.FileRequirements,
                CreatorId : req.CreatorId,
                DateCreated : DateHelper.GetPHTime(),
                CustomFields: req.CustomFields
            );
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<JobDto>(newJob);
        }
    }
}