using Application.commons.IRepository;
using Application.commons.IServices;
using Application.exeptions;
using MediatR;

namespace Application.features.Authentication.Commands.RotateToken
{
    public class RotateTokenHandler: IRequestHandler<RotateTokenCommand, RotateTokenDto>
    {
        private readonly IRefreshTokensRepository _refreshTokensRepository;
        private readonly ITokenService _tokenService;
        private readonly IDbContext _dbContext;
        public RotateTokenHandler(
            IRefreshTokensRepository refreshTokensRepository,
            ITokenService tokenService,
            IDbContext dbContext
        )
        {
            _refreshTokensRepository = refreshTokensRepository;
            _tokenService = tokenService;
            _dbContext = dbContext;
        }

        public async Task<RotateTokenDto> Handle(
            RotateTokenCommand req,
            CancellationToken cancellationToken
        )
        {
            var token = await _refreshTokensRepository.GetByToken(req.UsedRefreshToken)?? throw new UnauthorizeExeption("No Token Found");
            if(token.ExpiryDate < DateTime.UtcNow)
            {
                token.IsRevoked = true;
                throw new InvalidInputExeption("Unauthorized");
            }

            var newAccessToken = _tokenService.GenerateJwtToken(
                AdminId: Guid.Parse(req.ClaimsAdminId),
                Role: req.ClaimsRole
            );
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            token.Token = newRefreshToken;
            token.ExpiryDate = DateTime.UtcNow.AddDays(1);

            await _dbContext.SaveChangesAsync(cancellationToken);
            return new RotateTokenDto
            {
                NewAccessToken = newAccessToken,
                NewRefreshToken = newRefreshToken
            };
        }
    }
}