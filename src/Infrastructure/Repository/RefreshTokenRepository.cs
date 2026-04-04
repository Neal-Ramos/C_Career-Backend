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
            DateTime ExpiryDate,
            DateTime DateCreated
        ){
            var newToken = new RefreshTokens
            {
                Token = Token,
                ExpiryDate = ExpiryDate,
                DateCreated = DateCreated
            };
            await _context.AddAsync(newToken);

            return newToken;
        }
    }
}