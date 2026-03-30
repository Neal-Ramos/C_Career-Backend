using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.commons.DTOs;
using MediatR;

namespace Application.features.Authentication.Commands.Login
{
    public class LoginCommand: IRequest<LoginDto>
    {
        public string UserName {get; set;} = null!;
        public string Password {get; set;} = null!;
    }
}