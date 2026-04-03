namespace Application.commons.IServices
{
    public interface ITokenService
    {
        string GenerateJwtToken(Guid AdminId, string Role);
        string GenerateRefreshToken();
    }
}