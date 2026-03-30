using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.commons.DTOs
{
    public class AuthCodeDto
    {
        public Guid AuthCodeId {get; set;}
        public string Code {get; set;} = null!;
        public DateTime DateCreated {get; set;}
        public DateTime DateExpiry {get; set;}
        public DateTime? DateUsed {get; set;}
        public bool IsUsed {get; set;} = false;
        public Guid OwnerId {get; set;}
        public AdminAccountsDto Owner {get; set;} = null!;
    }
}