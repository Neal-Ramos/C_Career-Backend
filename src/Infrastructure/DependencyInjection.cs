using Application.commons.IRepository;
using Application.commons.IServices;
using Infrastructure.Persistence;
using Infrastructure.Repository;
using Infrastructure.Services.AIService;
using Infrastructure.Services.HashingService;
using Infrastructure.Services.OAuthService;
using Infrastructure.Services.ResendServices;
using Infrastructure.Services.SupabaseService;
using Infrastructure.Services.TokenServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Resend;
using Supabase;

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
                configuration.GetConnectionString("CCareerDB")
               );

               options.EnableDetailedErrors();

               if (isDevelopment)
                {
                    options.EnableSensitiveDataLogging();
                }
            });
            services.AddScoped<IDbContext>(provider => 
                provider.GetRequiredService<AppDbContext>());

            //Register Repositories
            services.AddScoped<IJobsRepository, JobsRepository>();
            services.AddScoped<IApplicationsRepository, JobApplicationsRepository>();
            services.AddScoped<IAdminAccountsRepository, AdminAccountsRepository>();
            services.AddScoped<IAuthCodeRepository, AuthCodeRepository>();
            services.AddScoped<IRefreshTokensRepository, RefreshTokenRepository>();
            services.AddScoped<IJobsEditHistoryRepository, JobsEditHistoryRepository>();
            services.AddScoped<IApplicantInterviewsRepository, ApplicantInterviewsRepository>();
            services.AddScoped<IOAuthService, GoogleOAuthService>();
            services.AddScoped<IAIService, GroqService>();
            services.AddScoped<IHashingService, BcryptRepository>();

            //Register Seeder
            services.AddScoped<IDbSeeder, DbSeeder>();
            

            //Register Supabase
            services.AddScoped((provider) =>
            {
                var supabaseUrl = configuration["Supabase:Url"]!;
                var supabaseKey = configuration["Supabase:ServiceKey"]!;
                var options = new SupabaseOptions
                {
                    AutoRefreshToken = true,
                    AutoConnectRealtime = true
                };
                var client = new Supabase.Client(supabaseUrl, supabaseKey, options);
                client.InitializeAsync().GetAwaiter().GetResult();
                return client;
            });
            services.AddScoped<IStorageService, SupabaseFileService>();

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