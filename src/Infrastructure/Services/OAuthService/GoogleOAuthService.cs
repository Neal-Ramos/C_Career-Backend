
using Application.commons.DTOs;
using Application.commons.IServices;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services.OAuthService
{
    public class GoogleOAuthService: IOAuthService
    {
        private readonly string _ClientId;
        public GoogleOAuthService(
            IConfiguration configuration
        )
        {
            _ClientId = configuration["OAuthClient:Id"]!;
        }

        public async Task<OAuthResponseDto> Validate(
            string Credential
        )
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(
                    Credential,
                    new GoogleJsonWebSignature.ValidationSettings
                    {
                        Audience = [_ClientId]
                    }
                );

                return new OAuthResponseDto
                {
                    Iss = payload.Issuer,
                    Sub = payload.Subject,
                    Email = payload.Email,
                    Email_verified = payload.EmailVerified,
                    Nbf = payload.NotBeforeTimeSeconds,
                    Name = payload.Name,
                    Pcture = payload.Picture,
                    Given_name = payload.GivenName,
                    Family_name = payload.FamilyName,
                    Iat = payload.IssuedAtTimeSeconds,
                    Exp = payload.ExpirationTimeSeconds,
                    Jti = payload.JwtId
                };
            }
            catch (InvalidJwtException exeption)
            {
                throw new UnauthorizedAccessException("Invalid Google Token", exeption);
            }
        }
    }
}