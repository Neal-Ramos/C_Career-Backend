using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AuthCodes
    {
        public int Id {get; private set;}
        public Guid AuthCodeId {get; private set;} = Guid.NewGuid();
        public string Code {get; set;} = null!;
        public DateTime DateCreated {get; set;}
        public DateTime DateExpiry {get; set;}
        public DateTime? DateUsed {get; set;}
        public bool IsUsed {get; set;} = false;
        
        //Relations
        public Guid OwnerId {get; set;}
        public AdminAccounts Owner {get; set;} = null!;
    }
}