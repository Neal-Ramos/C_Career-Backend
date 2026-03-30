using Domain.Entities;
using Infrastructure.Persistence.configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }

        public DbSet<Jobs> Jobs => Set<Jobs>();
        public DbSet<JobApplications> JobApplications => Set<JobApplications>();
        public DbSet<AdminAccounts> AdminAccounts => Set<AdminAccounts>();
        public DbSet<AuthCodes> AuthCodes => Set<AuthCodes>();
        public DbSet<RefreshTokens> RefreshTokens => Set<RefreshTokens>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(JobsConfigurations).Assembly);
        }
    }
}