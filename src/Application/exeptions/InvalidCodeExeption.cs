using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.exeptions
{
    public class InvalidCodeExeption: Exception
    {
        public InvalidCodeExeption(): base("Invalid Code"){}
    }
}