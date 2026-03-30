using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.commons.DTOs;
using Application.features.Authentication.Commands.Login;
using AutoMapper;

namespace Application
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<AdminAccountsDto, LoginDto>();
        }
    }
}