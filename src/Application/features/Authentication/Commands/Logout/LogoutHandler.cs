
using Application.commons.IRepository;
using Application.exeptions;
using MediatR;

namespace Application.features.Authentication.Commands.Logout
{
    public class LogoutHandler: IRequestHandler<LogoutCommand, LogoutDto>
    {
        private readonly IRefreshTokensRepository _refreshTokensRepository;
        private readonly IDbContext _dbContext;
        public LogoutHandler(
            IRefreshTokensRepository refreshTokensRepository,
            IDbContext dbContext
        )
        {
            _refreshTokensRepository = refreshTokensRepository;
            _dbContext = dbContext;
        }
        public async Task<LogoutDto> Handle(
            LogoutCommand req,
            CancellationToken cancellationToken
        )
        {
            var token = await _refreshTokensRepository.GetByToken(req.UsedRefreshToken)?? throw new NotFoundExeption("RefreshToken not Found");
            token.IsRevoked = true;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return new LogoutDto
            {
                Message = "Logout Success"
            };
        }
    }
}