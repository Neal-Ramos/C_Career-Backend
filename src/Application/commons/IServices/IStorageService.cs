namespace Application.commons.IServices
{
    public interface IStorageService
    {
        Task<UploadAsyncDto> UploadAsync(
            string FolderName,
            string DocumentName,
            string FileName,
            string ContentType,
            string Format,
            Stream Content
        );
        Task<string> CreateSignedUrl(
            string PublicId
        );
        Task<(Stream Content, string ContentType)> DownloadAsync(
            string PublicId
        );
    }
}