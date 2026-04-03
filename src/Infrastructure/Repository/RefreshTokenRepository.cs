using Application.commons.IRepository;
using Domain.Entities;
using Infrastructure.Persistence;

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

        public async Task<RefreshTokens> AddRefreshToken(
            string Token,
            DateTime ExpiryDate
        ){
            var newToken = new RefreshTokens
            {
                Token = Token,
                ExpiryDate = ExpiryDate
            };
            await _context.AddAsync(newToken);

            return newToken;
        }
    }
}