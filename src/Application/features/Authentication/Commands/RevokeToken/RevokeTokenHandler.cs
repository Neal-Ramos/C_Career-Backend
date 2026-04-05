using Application.commons.IRepository;
using MediatR;

namespace Application.features.Authentication.Commands.RevokeToken
{
    public class RevokeTokenHandler: IRequestHandler<RevokeTokenCommand, RevokeTokenDto>
    {
        private readonly IRefreshTokensRepository _refreshTokensRepository;
        private readonly IDbContext _dbContext;
        public RevokeTokenHandler(
            IRefreshTokensRepository refreshTokensRepository,
            IDbContext dbContext
        )
        {
            _refreshTokensRepository = refreshTokensRepository;
            _dbContext = dbContext;
        }

        public async Task<RevokeTokenDto> Handle(
            RevokeTokenCommand req,
            CancellationToken cancellationToken
        )
        {
            var token = await _refreshTokensRepository.GetByToken(req.Token);
            token!.IsRevoked = true;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new RevokeTokenDto{};
        }
    }
}