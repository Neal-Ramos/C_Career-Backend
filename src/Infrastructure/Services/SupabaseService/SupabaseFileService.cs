using Application.commons.IServices;

namespace Infrastructure.Services.SupabaseService
{
    public class SupabaseFileService: IStorageService
    {
        private readonly Supabase.Client  _client;
        public SupabaseFileService(Supabase.Client client)
        {
            _client = client;
        }

        public async Task<UploadAsyncDto> UploadAsync(
            string FolderName,
            string DocumentName,
            string FileName,
            string ContentType,
            string Format,
            Stream Content
        )
        {
            using var memoryStream = new MemoryStream();
            await Content.CopyToAsync(memoryStream);
            byte[] fileBytes = memoryStream.ToArray();

            string storagePath = $"{FolderName}/{FileName}";

            var options = new Supabase.Storage.FileOptions
            {
                ContentType = ContentType,
                Upsert = true
            };

            await _client.Storage
                .From("C_Career")
                .Upload(fileBytes, storagePath, options);

            return new UploadAsyncDto
            {
                DocumentName = DocumentName,
                FileName = FileName,
                ContentType = ContentType,
                Format = Format,
                PublicId = storagePath
            };
        }
        public async Task<string> CreateSignedUrl(
            string PublicId
        )
        {
            return await _client.Storage.From("C_Career")
                .CreateSignedUrl(PublicId, 5 * 60);
        }
        public async Task<(Stream Content, string ContentType)> DownloadAsync(
            string PublicId
        )
        {
            var bytes = await _client.Storage
                .From("C_Career")
                .Download(PublicId, null);

            return (new MemoryStream(bytes), GetContentType(PublicId));
        }
        private static string GetContentType(string path)
        {
            return Path.GetExtension(path).ToLower() switch
            {
                ".pdf"  => "application/pdf",
                ".png"  => "image/png",
                ".jpg"  => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".doc"  => "application/msword",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                _       => "application/octet-stream"
            };
        }
    }
}