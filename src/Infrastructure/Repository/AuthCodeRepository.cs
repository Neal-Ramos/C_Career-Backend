using Application.commons.IRepository;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class AuthCodeRepository: IAuthCodeRepository
    {
        private readonly AppDbContext _context;
        public AuthCodeRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<AuthCodes> CreateCodeFor(
            string Code,
            DateTime DateCreated,
            DateTime DateExpiry,
            Guid OwnerId
        )
        {
            var newCode = new AuthCodes
            {
                Code = Code,
                DateCreated = DateCreated,
                DateExpiry = DateExpiry,
                OwnerId = OwnerId
            };
            await _context.AuthCodes.AddAsync(newCode);
            
            return newCode;
        }
        public async Task<AuthCodes?> GetCodeByCodeAndEmail(
            string Code,
            string Email
        )
        {
            return await _context.AuthCodes.FirstOrDefaultAsync(a => a.Code == Code && a.Owner.Email == Email);
        }
    }
}