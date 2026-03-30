using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.features.Authentication.Commands.VerifyOtp
{
    public class VerifyOtpDto
    {
        public string AccessToken {get; set;} = null!;
        public DateTime AccessTokenEpiry {get; set;}
        public string RefreshToken {get; set;} = null!;
    }
}