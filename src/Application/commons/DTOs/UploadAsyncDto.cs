namespace Application.commons.IServices
{
    public class UploadAsyncDto
    {
        public string DocumentName {get; set;} = null!;
        public string FileName {get; set;} = null!;
        public string ContentType {get; set;} = null!;
        public string Format {get; set;} = null!;
        public string PublicId {get; set;} = null!;
        public string Path {get; set;} = null!; 
    }
}