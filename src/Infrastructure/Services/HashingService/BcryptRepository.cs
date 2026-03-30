using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public Task<Boolean> VerifyString(
            string str,
            string reference
        )
        {
            return Task.FromResult(BCrypt.Net.BCrypt.Verify(str, reference));
        }
    }
}