using Domain.enums;
using MediatR;

namespace Application.features.Applications.Queries.GetApplications
{
    public class GetApplicationsQuery: IRequest<GetApplicationsDto>
    {
        public int Page {get; set;}
        public int PageSize {get; set;}
        public string? Search {get; set;}
        public ApplicationStatusEnum? FilterStatus {get; set;}
        public string? FilterJobTitle {get; set;}
    }
}