
using MediatR;

namespace Application.features.Applications.Queries.GetApplicationFile
{
    public class GetApplicationFileQuery: IRequest<(Stream Content, string ContentType)>
    {
        public string PublicId {get; set;} = null!;
    }
}