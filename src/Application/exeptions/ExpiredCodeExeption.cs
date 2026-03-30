using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.exeptions
{
    public class ExpiredCodeExeption: Exception
    {
        public ExpiredCodeExeption(): base("Expired Code"){}
    }
}