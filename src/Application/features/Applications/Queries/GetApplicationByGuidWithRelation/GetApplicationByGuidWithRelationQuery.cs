using Application.commons.DTOs;
using MediatR;

namespace Application.features.Applications.Queries.GetApplicationByGuidWithRelation
{
    public class GetApplicationByGuidWithRelationQuery: IRequest<ApplicationWithRelationsDto>
    {
        public Guid ApplicationId {get; set;}
    }
}