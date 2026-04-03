using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.commons.DTOs
{
    public class AdminAccountDto
    {
        public Guid AdminId {get; private set;}
        public string Email {get; set;} = null!;
        public string UserName {get; set;} = null!;
        public string FirstName {get; set;} = null!;
        public string LastName {get; set;} = null!;
        public string? MiddleName {get; set;}
    }
}