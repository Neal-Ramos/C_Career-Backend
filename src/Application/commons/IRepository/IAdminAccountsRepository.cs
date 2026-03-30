using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.commons.DTOs;

namespace Application.commons.IRepository
{
    public interface IAdminAccountsRepository
    {
        Task<AdminAccountsDto?> GetByUsername(
            string UserName
        );
    }
}