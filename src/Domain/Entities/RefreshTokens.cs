using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RefreshTokens
    {
        public int Id {get; private set;}
        public Guid RefreshTokenId {get; private set;}
        public string HashedToken {get; set;} = null!;
        public DateTime ExpiryDate {get; set;}
        public DateTime DateCreated {get; set;}
    }
}