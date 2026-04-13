namespace Application.commons.IServices
{
    public interface IStorageService
    {
        Task<UploadAsyncDto> UploadAsync(
            string DocumentName,
            string FileName,
            string ContentType,
            string Format,
            Stream Content
        );
    }
}