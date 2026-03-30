using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.commons.IServices
{
    public interface IHashingService
    {
        Task<string> HashString(string str);
        Task<Boolean> VerifyString(
            string str,
            string reference
        );
    }
}