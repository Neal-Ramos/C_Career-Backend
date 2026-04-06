
using Application.commons.DTOs;
using Application.commons.IRepository;
using AutoMapper;
using MediatR;

namespace Application.features.Applications.Queries.GetApplicatonBuGuid
{
    public class GetApplicationByGuidHandler: IRequestHandler<GetApplicationByGuidQuery, ApplicationDto?>
    {
        private readonly IApplicationsRepository _applicationsRepository;
        private readonly IMapper _mapper;
        public GetApplicationByGuidHandler(
            IApplicationsRepository applicationsRepository,
            IMapper mapper
        )
        {
            _applicationsRepository = applicationsRepository;
            _mapper = mapper;
        }
        public async Task<ApplicationDto?> Handle(
            GetApplicationByGuidQuery req,
            CancellationToken cancellationToken
        )
        {
            var application = await _applicationsRepository.GetApplicationByGuid(req.ApplicationId);
            return _mapper.Map<ApplicationDto>(application);
        }
    }
}