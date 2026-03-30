using Application.commons.IServices;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Infrastructure.Services.CloudinaryServices;
using Microsoft.Extensions.Options;
namespace Infrastructure.Services
{
    public class StorageRepository: IStorageService
    {
        private readonly Cloudinary _cloudinary;
        public StorageRepository(IOptions<CloudinarySettings> options)
        {
            var settings = options.Value;
            var account = new Account(
                settings.CloudName,
                settings.ApiKey,
                settings.ApiSecret
            );
            _cloudinary = new Cloudinary(account);
            _cloudinary.Api.Secure = true;
        }

        public async Task<UploadAsyncDto> UploadAsync(
            string FileName,
            string Name,
            string ContentType,
            Stream Content
        )
        {
            var uploadParams = new RawUploadParams
            {
                File = new FileDescription(FileName, Content),
                Type = "private",
                Folder = "Applications"
            };

            RawUploadResult result = await _cloudinary.UploadAsync(uploadParams);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception(result.Error?.Message);

            return new UploadAsyncDto
            {
                PublicId = result.PublicId,
                Path = result.DisplayName
            };
        }
    }
}