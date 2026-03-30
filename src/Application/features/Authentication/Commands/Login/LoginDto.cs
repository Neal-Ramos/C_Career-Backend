using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.features.Authentication.Commands.Login
{
    public class LoginDto
    {
        public Guid AdminId {get; set;}
        public string Email {get; set;} = null!;
        public string UserName {get; set;} = null!;
    }
}