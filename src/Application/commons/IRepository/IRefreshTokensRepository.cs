using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.commons.DTOs;

namespace Application.commons.IRepository
{
    public interface IRefreshTokensRepository
    {
        Task<RefreshTokensDto?> GetRefreshTokenById(Guid Id);
        Task<RefreshTokensDto> CreateRefreshToken(
            string HashedToken,
            DateTime ExpiryDate,
            DateTime DateCreated
        );
    }
}