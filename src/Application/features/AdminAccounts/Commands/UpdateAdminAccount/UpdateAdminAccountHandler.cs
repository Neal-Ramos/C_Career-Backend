
using Application.commons.DTOs;
using Application.commons.IRepository;
using Application.exeptions;
using AutoMapper;
using MediatR;

namespace Application.features.AdminAccounts.Commands.UpdateAdminAccount
{
    public class UpdateAdminAccountHandler: IRequestHandler<UpdateAdminAccountCommand, AdminAccountDto>
    {
        private readonly IMapper _mapper;
        private readonly IDbContext  _dbContext;
        private readonly IAdminAccountsRepository _adminAccountsRepository;
        public UpdateAdminAccountHandler(
            IMapper mapper,
            IDbContext dbContext,
            IAdminAccountsRepository adminAccountsRepository
        )
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _adminAccountsRepository = adminAccountsRepository;
        }
        public async Task<AdminAccountDto> Handle(
            UpdateAdminAccountCommand req,
            CancellationToken cancellationToken
        )
        {
            var adminAccount = await _adminAccountsRepository.GetByAdminId(req.AdminId)?? throw new UnauthorizeExeption();

            adminAccount.Email = req.Email;
            adminAccount.UserName = req.UserName;
            adminAccount.FirstName = req.FirstName;
            adminAccount.LastName = req.LastName;
            adminAccount.MiddleName = req.MiddleName;
            adminAccount.BirthDate = req.BirthDate;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return _mapper.Map<AdminAccountDto>(adminAccount);
        }
    }
}