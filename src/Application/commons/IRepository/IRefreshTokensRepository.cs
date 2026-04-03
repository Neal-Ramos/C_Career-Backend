using Domain.Entities;

namespace Application.commons.IRepository
{
    public interface IRefreshTokensRepository
    {
        Task<RefreshTokens> AddRefreshToken(
            string Token,
            DateTime ExpiryDate
        );
    }
}