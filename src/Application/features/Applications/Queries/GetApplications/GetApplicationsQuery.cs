using MediatR;

namespace Application.features.Applications.Queries.GetApplications
{
    public class GetApplicationsQuery: IRequest<GetApplicationsDto>
    {
        public int Page {get; set;}
        public int PageSize {get; set;}
    }
}