using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.commons.DTOs;
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

        public async Task<AuthCodeDto> CreateCodeFor(
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
            _context.SaveChanges();

            return new AuthCodeDto
            {
                AuthCodeId = newCode.AuthCodeId,
                Code = newCode.Code,
                DateCreated = newCode.DateCreated,
                DateExpiry = newCode.DateExpiry,
                DateUsed = newCode.DateUsed,
                IsUsed = newCode.IsUsed,
                OwnerId = newCode.OwnerId,
            };
        }
        public async Task<AuthCodeDto?> GetCodeByCodeAndEmail(
            string Code,
            string Email
        )
        {
            return await _context.AuthCodes.Select(a => new AuthCodeDto
            {
                AuthCodeId = a.AuthCodeId,
                Code = a.Code,
                DateCreated = a.DateCreated,
                DateExpiry = a.DateExpiry,
                DateUsed = a.DateUsed,
                IsUsed = a.IsUsed,
                OwnerId = a.OwnerId,
                Owner = new AdminAccountsDto
                {
                    Email = a.Owner.Email,
                    UserName = a.Owner.UserName,
                    FirstName = a.Owner.FirstName,
                    LastName = a.Owner.LastName,
                    MiddleName = a.Owner.MiddleName
                }
            }).FirstOrDefaultAsync(a => a.Code == Code && a.Owner.Email == Email);
        }
    }
}