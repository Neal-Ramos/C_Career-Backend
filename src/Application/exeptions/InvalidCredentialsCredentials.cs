using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.exeptions
{
    public class InvalidCredentialsExeption: Exception
    {
        public InvalidCredentialsExeption()
            :base("Invalid Credentials!"){}
    }
}