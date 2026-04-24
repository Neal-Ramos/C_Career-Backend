using Application.commons.IRepository;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class RefreshTokenRepository: IRefreshTokensRepository
    {
        private readonly AppDbContext _context;
        public RefreshTokenRepository (
            AppDbContext appDbContext
        )
        {
            _context = appDbContext;
        }

        public async Task<RefreshTokens> AddAsync(
            string Token,
            Guid OwnerId,
            DateTime ExpiryDate,
            DateTime DateCreated
        ){
            var newToken = new RefreshTokens
            {
                Token = Token,
                OwnerId = OwnerId,
                ExpiryDate = ExpiryDate,
                DateCreated = DateCreated
            };
            await _context.AddAsync(newToken);

            return newToken;
        }
        public async Task<RefreshTokens?> GetByToken(
            string Token
        )
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(r => r.Token == Token);
        }
        public async Task<RefreshTokens?> GetByOwnerId(
            Guid OwnerId
        )
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(a => a.OwnerId == OwnerId);
        }
    }
}