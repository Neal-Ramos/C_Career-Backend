using Application.commons.IRepository;
using Domain.Entities;
using Infrastructure.Persistence.configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AppDbContext:DbContext, IDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }

        public DbSet<Jobs> Jobs => Set<Jobs>();
        public DbSet<Applications> Applications => Set<Applications>();
        public DbSet<AdminAccounts> AdminAccounts => Set<AdminAccounts>();
        public DbSet<AuthCodes> AuthCodes => Set<AuthCodes>();
        public DbSet<RefreshTokens> RefreshTokens => Set<RefreshTokens>();
        public DbSet<JobsEditHistory> JobsEditHistory => Set<JobsEditHistory>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(JobsConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationsConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthCodesConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(JobsConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RefreshTokenConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(JobsEditHistoryConfigurations).Assembly);
        }
    }
}
// for migration => dotnet ef migrations add "Key" --project src\Infrastructure --startup-project src\API
// for database => update dotnet ef database update --project src\Infrastructure --startup-project src\API