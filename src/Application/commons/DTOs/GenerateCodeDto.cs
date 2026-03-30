using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.commons.DTOs
{
    public class GenerateCodeDto
    {
        public string Code {get; set;} = null!;
        public DateTime DateExpiry {get; set;}
    }
}