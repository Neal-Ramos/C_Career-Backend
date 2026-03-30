using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.commons.DTOs;

namespace Application.commons.IRepository
{
    public interface IAuthCodeRepository
    {
        Task<AuthCodeDto> CreateCodeFor(
            string Code,
            DateTime DateCreated,
            DateTime DateExpiry,
            Guid OwnerId
        );
        Task<AuthCodeDto?> GetCodeByCodeAndEmail(
            string Code,
            string Email
        );
    }
}