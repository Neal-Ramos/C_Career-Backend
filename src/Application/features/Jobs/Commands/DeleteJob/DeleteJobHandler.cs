using Application.commons.IRepository;
using Application.exeptions;
using AutoMapper;
using MediatR;

namespace Application.features.Jobs.Commands.DeleteJob
{
    public class DeleteJobHandler: IRequestHandler<DeleteJobCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _dbContext;
        private readonly IJobsRepository _jobsRepository;
        public DeleteJobHandler(
            IMapper mapper,
            IDbContext dbContext,
            IJobsRepository jobsRepository
        )
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _jobsRepository = jobsRepository;
        }
        public async Task<Guid> Handle(
            DeleteJobCommand req,
            CancellationToken cancellationToken
        )
        {
            var deleteJob = await _jobsRepository.GetJobsById(req.JobId)?? throw new NotFoundExeption("Job Is Not Existing");
            deleteJob.IsDeleted = true;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return deleteJob.JobId;
        }
    }
}