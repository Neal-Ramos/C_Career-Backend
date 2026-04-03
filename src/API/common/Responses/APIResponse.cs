namespace API.common.Responses
{
    public class APIResponse<T>
    {
        public string? Message {get; set;}
        public T? Data {get; set;}
        public string? Error {get; set;}
        public Dictionary<string, object>? Meta { get; set; }
    }
}