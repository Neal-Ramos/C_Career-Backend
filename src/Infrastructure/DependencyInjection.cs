using Application.commons.IRepository;
using Application.commons.IServices;
using Infrastructure.Persistence;
using Infrastructure.Repository;
using Infrastructure.Services;
using Infrastructure.Services.CloudinaryServices;
using Infrastructure.Services.CodeGenerator;
using Infrastructure.Services.HashingService;
using Infrastructure.Services.ResendServices;
using Infrastructure.Services.TokenServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Resend;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration,
            bool isDevelopment) 
        {
            //Register DbContext
            services.AddDbContext<AppDbContext>((sp, options) =>
            {
               options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")
               );

               options.EnableDetailedErrors();

               if (isDevelopment)
                {
                    options.EnableSensitiveDataLogging();
                }
            });

            //Register Repositories
            services.AddScoped<IJobsRepository, JobsRepository>();
            services.AddScoped<IApplicationsRepository, JobApplicationsRepository>();
            services.AddScoped<IAdminAccountsRepository, AdminAccountsRepository>();
            services.AddScoped<IAuthCodeRepository, AuthCodeRepository>();
            services.AddScoped<IRefreshTokensRepository, RefreshTokenRepository>();

            services.AddScoped<IGenerateCodeService, CodeGeneratorRepository>();
            services.AddScoped<IHashingService, BcryptRepository>();

            //Register Seeder
            services.AddScoped<IDbSeeder, DbSeeder>();
            

            //Register Cloudinary
            services.Configure<CloudinarySettings>(
                configuration.GetSection("CloudinarySettings")
            );
            services.AddScoped<IStorageService, StorageRepository>();

            //Register Resend
            services.AddOptions();
            services.AddHttpClient<ResendClient>();
            services.AddTransient<IResend, ResendClient>();
            services.Configure<ResendClientOptions>(options =>
            {
                options.ApiToken = configuration["Resend:ApiKey"]!;
            });
            services.AddScoped<ISendEmailService, ResendRepository>();

            //Register Token
            services.AddScoped<ITokenService, TokenServices>();

            return services;
        }
        
    }
}