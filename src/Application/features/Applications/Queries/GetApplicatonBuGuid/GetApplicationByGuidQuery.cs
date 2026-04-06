
using Application.commons.DTOs;
using MediatR;

namespace Application.features.Applications.Queries.GetApplicatonBuGuid
{
    public class GetApplicationByGuidQuery: IRequest<ApplicationDto?>
    {
        public Guid ApplicationId {get; set;}
    }
}