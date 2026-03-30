using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.commons.DTOs;
using Application.commons.IRepository;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class AdminAccountsRepository: IAdminAccountsRepository
    {
        private readonly AppDbContext _context;
        public AdminAccountsRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<AdminAccountsDto?> GetByUsername(string UserName)
        {
            return await _context.AdminAccounts
                .Select(a => new AdminAccountsDto
                {
                    AdminId = a.AdminId,
                    Email = a.Email,
                    UserName = a.UserName,
                    Password = a.Password,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    MiddleName = a.MiddleName,
                })
                .FirstOrDefaultAsync(a => a.UserName == UserName);
        }
    }
}