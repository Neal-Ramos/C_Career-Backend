using Application.commons.DTOs;
using Application.commons.Helpers;
using Application.commons.IRepository;
using Application.exeptions;
using AutoMapper;
using MediatR;

namespace Application.features.Applications.Commands.PatchApplicationStatus
{
    public class PatchApplicationStatusHandler: IRequestHandler<PatchApplicationStatusCommand, ApplicationDto>
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _dbContext;
        private readonly IApplicationsRepository _applicationsRepository;
        public PatchApplicationStatusHandler(
            IMapper mapper,
            IDbContext dbContext,
            IApplicationsRepository applicationsRepository
        )
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _applicationsRepository = applicationsRepository;
        }
        public async Task<ApplicationDto> Handle(
            PatchApplicationStatusCommand req,
            CancellationToken cancellationToken
        )
        {
            var application = await _applicationsRepository.GetApplicationByGuid(req.ApplicationId)?? throw new NotFoundExeption();
            if(application.DateReviewed != null)throw new ConflictExeption("Application Already Processed");

            application.DateReviewed = DateHelper.GetPHTime();
            application.Status = req.Status;
            application.AdminId = req.AdminId;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return _mapper.Map<ApplicationDto>(application); 
        }
    }
}