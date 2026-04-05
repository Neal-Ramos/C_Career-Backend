using Domain.Entities;

namespace Application.commons.IRepository
{
    public interface IRefreshTokensRepository
    {
        Task<RefreshTokens> AddRefreshToken(
            string Token,
            Guid OwnerId,
            DateTime ExpiryDate,
            DateTime DateCreated
        );
        Task<RefreshTokens?> GetByToken(
            string Token
        );
        Task<RefreshTokens?> GetByOwnerId(
            Guid OwnerId
        );
    }
}