using Application.commons.DTOs;
using Application.commons.Helpers;
using Application.commons.IRepository;
using Application.commons.IServices;
using Application.exeptions;
using AutoMapper;
using MediatR;

namespace Application.features.Authentication.Commands.Login
{
    public class LoginHandler: IRequestHandler<LoginCommand, LoginDto>
    {
        private readonly IAdminAccountsRepository _adminAccountsRepository;
        private readonly IRefreshTokensRepository _refreshTokensRepository;
        private readonly IAuthCodeRepository _authCodeRepository;
        private readonly ISendEmailService _sendEmailService;
        private readonly IHashingService _hashingService;
        private readonly ITokenService _tokenService;
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;
        public LoginHandler(
            IAdminAccountsRepository adminAccountsRepository,
            IRefreshTokensRepository refreshTokensRepository,
            IAuthCodeRepository authCodeRepository,
            ISendEmailService sendEmailService,
            IHashingService hashingService,
            ITokenService tokenService,
            IDbContext dbContext,
            IMapper mapper
            
        )
        {
            _adminAccountsRepository = adminAccountsRepository;
            _refreshTokensRepository = refreshTokensRepository;
            _authCodeRepository = authCodeRepository;
            _sendEmailService = sendEmailService;
            _hashingService = hashingService;
            _tokenService = tokenService;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<LoginDto> Handle(
            LoginCommand req,
            CancellationToken cancellationToken
        )
        {
            var adminAccount = await _adminAccountsRepository.GetByUsername(req.Username) ?? throw new InvalidInputExeption();
            // var isPasswordMatch = await _hashingService.VerifyString(req.Password, adminAccount.Password);
            // if(!isPasswordMatch) throw new InvalidInputExeption();

            if(req.OtpCode == null)// Send Otp Code
            {
                var OtpCode = new OtpGenerator().GenerateOtpCode();
                var OtpExpiry = DateHelper.GetPHTime().AddMinutes(2);
                await _authCodeRepository.CreateCodeFor(
                    Code: OtpCode,
                    DateCreated: DateHelper.GetPHTime(),
                    DateExpiry: OtpExpiry,
                    OwnerId: adminAccount.AdminId
                );
                // await _sendEmailService.SendEmailAsync(
                //     To: adminAccount.Email,
                //     Subject: "Otp",
                //     HtmlContent: $"<div><strong>Your Otp Code is {OtpCode}</div>"
                // );
                await _dbContext.SaveChangesAsync(cancellationToken);
                return new LoginDto
                {
                    Message = "Otp Sent!"
                };
            }
            else// Validate Otp Code
            {
                // var OtpCode = await _authCodeRepository.GetCodeByCodeAndEmail(
                //     Email: adminAccount.Email,
                //     Code: req.OtpCode
                // )?? throw new InvalidInputExeption();
                // if(OtpCode.DateExpiry <= DateHelper.GetPHTime()) throw new InvalidInputExeption("Code Expired");

                var AccessToken = _tokenService.GenerateJwtToken(
                    AdminId: adminAccount.AdminId,
                    Role: "Admin"
                );
                var RefreshToken = _tokenService.GenerateRefreshToken();

                await _refreshTokensRepository.AddRefreshToken(
                    Token: RefreshToken,
                    OwnerId: adminAccount.AdminId,
                    ExpiryDate: DateHelper.GetPHTime().AddDays(1),
                    DateCreated: DateHelper.GetPHTime()
                );
                // OtpCode.IsUsed = true;

                await _dbContext.SaveChangesAsync(cancellationToken);
                return new LoginDto
                {
                    AdminAccount = _mapper.Map<AdminAccountDto>(adminAccount),
                    RefreshToken = RefreshToken,
                    AccessToken = AccessToken,
                    Message = "Login Successful"
                };
            }
        }
    }
}