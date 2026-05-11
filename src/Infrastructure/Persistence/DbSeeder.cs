using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence
{
    public interface IDbSeeder
    {
        Task SeedAsync();
    }

    public class DbSeeder : IDbSeeder
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public DbSeeder(
            AppDbContext appDbContext,
            IConfiguration configuration
        )
        {
            _context = appDbContext;
            _configuration = configuration;
        }

        public async Task SeedAsync()
        {
            var admin = await _context.AdminAccounts.FirstOrDefaultAsync(a => a.Email == _configuration["AdminAccount:Email"]!);
            if (admin == null)
            {
                _context.AdminAccounts.AddRange(
                    new AdminAccounts
                    {
                        Email = _configuration["AdminAccount:Email"]!,
                        UserName = "Admin",
                        Password = BCrypt.Net.BCrypt.HashPassword(_configuration["AdminAccount:Password"]!),
                        FirstName = "Admin",
                        LastName = "Admin",
                        BirthDate = new DateTime(2000, 1, 1)
                    }
                );
                await _context.SaveChangesAsync();
            }
            await _context.SaveChangesAsync();
        }
    }
}