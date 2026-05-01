using Application.commons.IRepository;
using Domain.Entities;
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

        public async Task<AdminAccounts?> GetByUsername(string UserName)
        {
            return await _context.AdminAccounts.FirstOrDefaultAsync(a => a.UserName == UserName);
        }
        public async Task<AdminAccounts?> GetByAdminId(
            Guid AdminId
        )
        {
            return await _context.AdminAccounts.FirstOrDefaultAsync(a => a.AdminId == AdminId);
        }
        public async Task<AdminAccounts?> GetByEmail(
            string Email
        )
        {
            return await _context.AdminAccounts.FirstOrDefaultAsync(a => a.Email == Email);
        }
    }
}