
using Application.commons.DTOs;
using Application.commons.Helpers;
using Application.commons.IRepository;
using Application.exeptions;
using AutoMapper;
using MediatR;

namespace Application.features.Jobs.Commands.UpdateJob
{
    public class UpdateJobHandler: IRequestHandler<UpdateJobCommand, JobDto>
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _dbContext;
        private readonly IJobsRepository _jobsRepository;
        private readonly IJobsEditHistoryRepository _jobsEditHistoryRepository;
        public UpdateJobHandler(
            IMapper mapper,
            IDbContext dbContext,
            IJobsRepository jobsRepository,
            IJobsEditHistoryRepository jobsEditHistoryRepository
        )
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _jobsRepository = jobsRepository;
            _jobsEditHistoryRepository = jobsEditHistoryRepository;
        }
        public async Task<JobDto> Handle(
            UpdateJobCommand req,
            CancellationToken cancellationToken
        )
        {
            var job = await _jobsRepository.GetJobsById(req.JobId)?? throw new NotFoundExeption();
            job.Title = req.Title;
            job.Description = req.Description;
            job.Roles = req.Roles;
            job.CustomFields = req.CustomFields;
            job.FileRequirements = req.FileRequirements;
            await _jobsEditHistoryRepository.AddAsync(
                EditorId: req.EditorId,
                JobId: req.JobId,
                DateEdited: DateHelper.GetPHTime(),
                EditSummary: req.EditSummary
            );

            await _dbContext.SaveChangesAsync();
            return _mapper.Map<JobDto>(job);
        }
    }
}