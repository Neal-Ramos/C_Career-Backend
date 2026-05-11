using System.Text.Json;
using Application.commons.DTOs;
using Application.commons.IRepository;
using Application.commons.IServices;
using Application.exeptions;
using AutoMapper;
using MediatR;

namespace Application.features.AI.Queries.AnalyzeApplication
{
    public class AnalyzeApplicationHandler: IRequestHandler<AnalyzeApplicationQuery, ApplicationCheckDto>
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _dbContext;
        private readonly IAIService _aIService;
        private readonly IJobsRepository _jobsRepository;
        private readonly IApplicationsRepository _applicationsRepository;
        public AnalyzeApplicationHandler(
            IMapper mapper,
            IDbContext dbContext,
            IAIService aIService,
            IJobsRepository jobsRepository,
            IApplicationsRepository applicationsRepository
        )
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _aIService = aIService;
            _jobsRepository = jobsRepository;
            _applicationsRepository = applicationsRepository;
        }
        public async Task<ApplicationCheckDto> Handle(
            AnalyzeApplicationQuery req,
            CancellationToken cancellationToken
        )
        {
            var application = await _applicationsRepository.GetApplicationByGuid(req.ApplicationId)?? throw new NotFoundExeption("Application Not Found");
            // if(application.AiResults != null)throw new InvalidInputExeption("Application is Already Analyzed");
            
            var job = await _jobsRepository.GetJobsById(application.JobId)?? throw new NotFoundExeption("Job Not Found");

            var result = await _aIService.AnalyzeApplicationAsync(
                Job: _mapper.Map<JobDto>(job),
                Application: _mapper.Map<ApplicationDto>(application)
            );

            application.AiResults = JsonSerializer.Serialize(result);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}