using Domain.Entities;

namespace Application.commons.IRepository
{
    public interface IAdminAccountsRepository
    {
        Task<AdminAccounts?> GetByUsername(
            string UserName
        );
        Task<AdminAccounts?> GetByAdminId(
            Guid AdminId
        );
        Task<AdminAccounts?> GetByEmail(
            string Email
        );
    }
}