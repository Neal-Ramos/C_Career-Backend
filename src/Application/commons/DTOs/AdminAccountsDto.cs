using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.features.Jobs.DTOs;

namespace Application.commons.DTOs
{
    public class AdminAccountsDto
    {
        public int Id {get; set;}
        public Guid AdminId {get; set;}
        public string Email {get; set;} = null!;
        public string UserName {get; set;} = null!;
        public string Password {get; set;} = null!;
        public string FirstName {get; set;} = null!;
        public string LastName {get; set;} = null!;
        public string? MiddleName {get; set;}

        //relations
        public ICollection<AuthCodeDto>? AuthCodes {get; set;}
        public ICollection<JobsDto>? CreatedJobs {get; set;}
    }
}