using Application.commons.IServices;

namespace Infrastructure.Services.HashingService
{
    public class BcryptRepository: IHashingService
    {
        public Task<string> HashString(
            string str
        )
        {
            return Task.FromResult(BCrypt.Net.BCrypt.HashPassword(str));
        }

        public Task<bool> VerifyString(
            string str,
            string reference
        )
        {
            return Task.FromResult(BCrypt.Net.BCrypt.Verify(str, reference));
        }
    }
}