using Domain.Entities;

namespace Application.commons.IRepository
{
    public interface IAdminAccountsRepository
    {
        Task<AdminAccounts?> GetByUsername(
            string UserName
        );
    }
}