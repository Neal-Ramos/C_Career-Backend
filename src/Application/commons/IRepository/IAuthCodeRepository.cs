using Domain.Entities;

namespace Application.commons.IRepository
{
    public interface IAuthCodeRepository
    {
        Task<AuthCodes> AddAsync(
            string Code,
            DateTime DateCreated,
            DateTime DateExpiry,
            Guid OwnerId
        );
        Task<AuthCodes?> GetCodeByCodeAndEmail(
            string Code,
            string Email
        );
    }
}