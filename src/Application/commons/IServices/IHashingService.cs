namespace Application.commons.IServices
{
    public interface IHashingService
    {
        Task<string> HashString(string str);
        Task<bool> VerifyString(
            string str,
            string reference
        );
    }
}