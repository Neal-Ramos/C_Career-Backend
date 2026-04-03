namespace Application.features.Applications.DTOs
{
    public class FileUploadDTO
    {
        public string FileName { get; set; } = null!;
        public string Name {get; set;} = null!;
        public string ContentType { get; set; } = null!;
        public Stream Content { get; set; } = null!;

    }
}