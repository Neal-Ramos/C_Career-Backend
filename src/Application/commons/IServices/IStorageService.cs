namespace Application.commons.IServices
{
    public interface IStorageService
    {
        Task<UploadAsyncDto> UploadAsync(
            string FileName,
            string Name,
            string ContentType,
            Stream Content
        );
    }
}