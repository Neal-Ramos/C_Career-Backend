using Application.commons.DTOs;
using Application.commons.IRepository;
using AutoMapper;
using MediatR;

namespace Application.features.Applications.Queries.GetApplications
{
    public class GetApplicationsHandler: IRequestHandler<GetApplicationsQuery, GetApplicationsDto>
    {
        private readonly IApplicationsRepository _applications;
        private readonly IMapper _mapper;
        public GetApplicationsHandler(
            IApplicationsRepository applicationsRepository,
            IMapper mapper
        )
        {
            _applications = applicationsRepository;
            _mapper = mapper;
        }

        public async Task<GetApplicationsDto> Handle(
            GetApplicationsQuery req,
            CancellationToken cancellationToken
        )
        {
            var applications = await _applications.GetApplications(
                Page: req.Page,
                PageSize: req.PageSize,
                Search: req.Search,
                FilterStatus: req.FilterStatus,
                FilterJobTitle: req.FilterJobTitle
            );
            var applicationsTotal = await _applications.CountAsync(
                Search: req.Search,
                FilterStatus: req.FilterStatus,
                FilterJobTitle: req.FilterJobTitle
            );

            return new GetApplicationsDto
            {
                Applications = _mapper.Map<ICollection<ApplicationDto>>(applications),
                TotalRecord = applicationsTotal,
                TotalPages = (int)Math.Ceiling((double)applicationsTotal / req.PageSize)
            };
        }
    }
}