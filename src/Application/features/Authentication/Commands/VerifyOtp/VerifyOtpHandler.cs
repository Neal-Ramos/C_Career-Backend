using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.commons.DTOs;
using Application.commons.IRepository;
using Application.commons.IServices;
using Application.exeptions;
using MediatR;

namespace Application.features.Authentication.Commands.VerifyOtp
{
    public class VerifyOtpHandler: IRequestHandler<VerifyOtpCommand, VerifyOtpDto>
    {
        private readonly IAuthCodeRepository _authCode;
        private readonly IRefreshTokensRepository _refreshTokensRepository;
        private readonly ITokenService _tokenService;
        private readonly IHashingService _hashingService;
        public VerifyOtpHandler(
            IAuthCodeRepository authCodeRepository,
            IRefreshTokensRepository refreshTokensRepository,
            ITokenService tokenService,
            IHashingService hashingService
        )
        {
            _authCode = authCodeRepository;
            _refreshTokensRepository = refreshTokensRepository;
            _tokenService = tokenService;
            _hashingService = hashingService;
        }

        public async Task<VerifyOtpDto> Handle(
            VerifyOtpCommand req,
            CancellationToken cancellationToken
        )
        {
            var getCode = await _authCode.GetCodeByCodeAndEmail(
                Email: req.Email,
                Code: req.Code
            )?? throw new InvalidCodeExeption();

            var AccessToken = _tokenService.GenerateJwtToken(
                AdminId: getCode.OwnerId,
                Email: getCode.Owner.Email,
                Role: "Admin"
            );
            var RefreshToken = _tokenService.GenerateRefreshToken();

            await _refreshTokensRepository.CreateRefreshToken(
                HashedToken: await _hashingService.HashString(RefreshToken),
                ExpiryDate: DateTime.UtcNow.AddDays(1),
                DateCreated: DateTime.UtcNow
            );

            return new VerifyOtpDto
            {
                AccessToken = AccessToken,
                AccessTokenEpiry = DateTime.UtcNow.AddMinutes(3),
                RefreshToken = RefreshToken
            };
        }
    }
}