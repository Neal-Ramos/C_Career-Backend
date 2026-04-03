using System.Security.Cryptography;

namespace Application.commons.Helpers
{
    public class OtpGenerator
    {
        public string GenerateOtpCode()
        {
            var bytes = RandomNumberGenerator.GetBytes(6);
            var code = string.Concat(bytes.Select(b => (b % 10).ToString()));

            return code;
        }
    }
}