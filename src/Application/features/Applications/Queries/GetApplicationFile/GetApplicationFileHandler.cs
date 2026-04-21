
using Application.commons.IServices;
using MediatR;

namespace Application.features.Applications.Queries.GetApplicationFile
{
    public class GetApplicationFileHandler: IRequestHandler<GetApplicationFileQuery, (Stream Content, string ContentType)>
    {
        private readonly IStorageService _storageService;
        public GetApplicationFileHandler(
            IStorageService storageService
        )
        {
            _storageService = storageService;
        }
        public async Task<(Stream Content, string ContentType)> Handle(
            GetApplicationFileQuery req,
            CancellationToken cancellationToken
        )
        {
            return await _storageService.DownloadAsync(req.PublicId);
        }
    }
}