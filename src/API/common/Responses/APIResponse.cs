namespace API.common.Responses
{
    public class APIResponse<T>
    {
        public int Status {get; set;}
        public string? Message {get; set;}
        public T? Data {get; set;}
        public string? Error {get; set;}
    }
}