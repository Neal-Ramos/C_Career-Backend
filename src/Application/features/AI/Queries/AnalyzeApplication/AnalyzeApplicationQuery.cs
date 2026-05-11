using Application.commons.DTOs;
using MediatR;

namespace Application.features.AI.Queries.AnalyzeApplication
{
    public class AnalyzeApplicationQuery: IRequest<ApplicationCheckDto>
    {
        public Guid ApplicationId {get; set;}
    }
}