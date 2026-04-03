using Application.commons.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Jobs, JobDto>();
            CreateMap<Applications, ApplicationDto>();
            CreateMap<AdminAccounts, AdminAccountDto>();
        }
    }
}