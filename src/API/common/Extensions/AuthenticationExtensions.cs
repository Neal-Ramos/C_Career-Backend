using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.features.Authentication.Commands.RevokeToken;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.common.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            var jwtSecret = configuration["JwtSettings:Secret"];

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSecret!)
                    )
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = async context =>
                    {
                        if (context.Exception is SecurityTokenExpiredException)
                        {
                            var usedRefreshToken = context.HttpContext.Request.Cookies["Refresh_Token"];

                            if(usedRefreshToken != null)
                            {
                                var mediator = context.HttpContext.RequestServices.GetRequiredService<IMediator>();
                                await mediator.Send(new RevokeTokenCommand{Token = usedRefreshToken});
                            }
                        }
                    },
                };
                
            });

            return services;
        }
    }
}