
using Application.commons.IRepository;
using Application.commons.IServices;
using Application.exeptions;
using MediatR;

namespace Application.features.AdminAccounts.Commands.ChangePassword
{
    public class ChangePasswordHandler: IRequestHandler<ChangePasswordCommand>
    {
        private readonly IDbContext _dbContext;
        private readonly IHashingService _hashingService;
        private readonly IAdminAccountsRepository _adminAccountsRepository;
        public ChangePasswordHandler(
            IDbContext dbContext,
            IHashingService hashingService,
            IAdminAccountsRepository adminAccountsRepository
        )
        {
            _dbContext = dbContext;
            _adminAccountsRepository = adminAccountsRepository;
            _hashingService = hashingService;
        }
        public async Task Handle(
            ChangePasswordCommand req,
            CancellationToken cancellationToken
        )
        {
            var adminAccount = await _adminAccountsRepository.GetByAdminId(req.AdminId)?? throw new UnauthorizeExeption();
            var isPrevPassMatch = await _hashingService.VerifyString(req.CurrentPassword, adminAccount.Password);
            
            if(isPrevPassMatch == false || req.NewPassword != req.ConfirmNewPassword)throw new InvalidInputExeption("Current Password Does not Match!");

            adminAccount.Password = await _hashingService.HashString(req.NewPassword);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}