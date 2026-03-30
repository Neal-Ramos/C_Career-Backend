using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.commons.DTOs;
using MediatR;

namespace Application.features.Authentication.Commands.VerifyOtp
{
    public class VerifyOtpCommand: IRequest<VerifyOtpDto>
    {
        public string Code {get; set;} = null!;
        public string Email {get; set;} = null!;
    }
}