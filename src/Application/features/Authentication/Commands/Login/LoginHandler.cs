using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.commons.DTOs;
using Application.commons.IRepository;
using Application.commons.IServices;
using Application.exeptions;
using AutoMapper;
using MediatR;

namespace Application.features.Authentication.Commands.Login
{
    public class LoginHandler: IRequestHandler<LoginCommand, LoginDto>
    {
        private readonly IAdminAccountsRepository _adminAccounts;
        private readonly IHashingService _IHashingService;
        private readonly IAuthCodeRepository _authCode;
        private readonly IGenerateCodeService _generateCode;
        private readonly IMapper _mapper;
        public LoginHandler(
            IAdminAccountsRepository adminAccountsRepository,
            IHashingService IHashingService,
            IAuthCodeRepository authCodeRepository,
            IGenerateCodeService generateCodeService,
            IMapper mapper
        )
        {
            _adminAccounts = adminAccountsRepository;
            _IHashingService = IHashingService;
            _authCode = authCodeRepository;
            _generateCode = generateCodeService;
            _mapper = mapper;
        }

        public async Task<LoginDto> Handle(
            LoginCommand req,
            CancellationToken cancellationToken
        )
        {
            var account = await _adminAccounts.GetByUsername(req.UserName);
            var code = await _generateCode.Generate();

            if(
                account == null ||
                !await _IHashingService.VerifyString(req.Password, account.Password)
            )
            {
                throw new InvalidCredentialsExeption();
            }

            await _authCode.CreateCodeFor(
                Code: code.Code,
                DateCreated: DateTime.UtcNow,
                DateExpiry: code.DateExpiry,
                OwnerId: account.AdminId
            );
            
            return _mapper.Map<LoginDto>(account);
        }
    }
}