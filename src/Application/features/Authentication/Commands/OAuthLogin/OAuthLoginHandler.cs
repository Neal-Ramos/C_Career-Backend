
using Application.commons.DTOs;
using Application.commons.Helpers;
using Application.commons.IRepository;
using Application.commons.IServices;
using Application.exeptions;
using AutoMapper;
using MediatR;

namespace Application.features.Authentication.Commands.OAuthLogin
{
    public class OAuthLoginHandler: IRequestHandler<OAuthLoginCommand, OAuthLoginDto>
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _dbContext;
        private readonly IOAuthService _oAuthService;
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokensRepository _refreshTokensRepository;
        private readonly IAdminAccountsRepository _adminAccountsRepository;
        public OAuthLoginHandler(
            IMapper mapper,
            IDbContext dbContext,
            IOAuthService oAuthService,
            ITokenService tokenService,
            IRefreshTokensRepository refreshTokensRepository,
            IAdminAccountsRepository adminAccountsRepository
        )
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _oAuthService = oAuthService;
            _tokenService = tokenService;
            _refreshTokensRepository = refreshTokensRepository;
            _adminAccountsRepository = adminAccountsRepository;
        }
        public async Task<OAuthLoginDto> Handle(
            OAuthLoginCommand req,
            CancellationToken cancellationToken
        )
        {
            var oAuthAccount = await _oAuthService.Validate(req.Credential);
            var adminAccount = await _adminAccountsRepository.GetByEmail(oAuthAccount.Email)?? throw new UnauthorizeExeption("Email is not Authorized");
            if(adminAccount.GoogleOAuthSub != null && adminAccount.GoogleOAuthSub != oAuthAccount.Sub) throw new UnauthorizeExeption("Email is not Authorized");

            var accessToken = _tokenService.GenerateJwtToken(
                AdminId: adminAccount.AdminId,
                Role: "Admin"
            );
            var refreshToken = _tokenService.GenerateRefreshToken();
            await _refreshTokensRepository.AddAsync(
                Token: refreshToken,
                OwnerId: adminAccount.AdminId,
                ExpiryDate: DateHelper.GetPHTime().AddDays(1),
                DateCreated: DateHelper.GetPHTime()
            );

            adminAccount.GoogleOAuthSub = oAuthAccount.Sub;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new OAuthLoginDto
            {
                AdminAccount = _mapper.Map<AdminAccountDto>(adminAccount),
                RefreshToken = refreshToken,
                AccessToken = accessToken,
                Message = "Login Success"
            };
        }
    }
}