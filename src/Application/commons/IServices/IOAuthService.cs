
using Application.commons.DTOs;

namespace Application.commons.IServices
{
    public interface IOAuthService
    {
        public Task<OAuthResponseDto> Validate(
            string Credential
        );
    }
}