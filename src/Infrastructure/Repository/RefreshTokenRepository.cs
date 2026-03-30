using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.commons.DTOs;
using Application.commons.IRepository;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repository
{
    public class RefreshTokenRepository: IRefreshTokensRepository
    {
        private readonly AppDbContext _context;
        public RefreshTokenRepository(
            AppDbContext appDbContext
        )
        {
            _context = appDbContext;
        }

        public async Task<RefreshTokensDto?> GetRefreshTokenById(
            Guid Id
        )
        {
            return await _context.RefreshTokens
                .Select(r => new RefreshTokensDto
                {
                    RefreshTokenId = r.RefreshTokenId,
                    HashedToken = r.HashedToken,
                    ExpiryDate = r.ExpiryDate,
                    DateCreated = r.DateCreated
                })
                .FirstOrDefaultAsync(r => r.RefreshTokenId == Id);
        }
        public async Task<RefreshTokensDto> CreateRefreshToken(
            string HashedToken,
            DateTime ExpiryDate,
            DateTime DateCreated
        )
        {
            var newToken = new RefreshTokens
                {
                    HashedToken = HashedToken,
                    ExpiryDate = ExpiryDate,
                    DateCreated = DateCreated
                };

            var add = await _context.RefreshTokens.AddAsync(newToken);

            return new RefreshTokensDto
            {
                RefreshTokenId = newToken.RefreshTokenId,
                HashedToken = newToken.HashedToken,
                ExpiryDate = newToken.ExpiryDate,
                DateCreated = newToken.DateCreated
            };
        }
    }
}