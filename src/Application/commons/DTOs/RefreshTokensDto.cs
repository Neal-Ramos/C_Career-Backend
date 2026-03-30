using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.commons.DTOs
{
    public class RefreshTokensDto
    {
        public Guid RefreshTokenId {get; set;}
        public string HashedToken {get; set;} = null!;
        public DateTime ExpiryDate {get; set;}
        public DateTime DateCreated {get; set;}
    }
}