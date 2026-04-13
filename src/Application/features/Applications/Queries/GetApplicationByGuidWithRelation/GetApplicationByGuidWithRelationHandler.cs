using Application.commons.DTOs;
using Application.commons.IRepository;
using Application.exeptions;
using MediatR;

namespace Application.features.Applications.Queries.GetApplicationByGuidWithRelation
{
    public class GetApplicationByGuidWithRelationHandler: IRequestHandler<GetApplicationByGuidWithRelationQuery, ApplicationWithRelationsDto>
    {
        private readonly IApplicationsRepository _applicationsRepository;
        public GetApplicationByGuidWithRelationHandler(
            IApplicationsRepository applicationsRepository
        )
        {
            _applicationsRepository = applicationsRepository;
        }
        public async Task<ApplicationWithRelationsDto> Handle(
            GetApplicationByGuidWithRelationQuery req,
            CancellationToken cancellationToken
        )
        {
            return await _applicationsRepository.GetApplicationByGuidWithRelation(req.ApplicationId)?? throw new NotFoundExeption();
        }
    }
}