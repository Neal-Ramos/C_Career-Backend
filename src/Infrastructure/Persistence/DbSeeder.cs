using System.Text.Json;
using Application.commons.Helpers;
using Domain.Entities;

namespace Infrastructure.Persistence
{
    public interface IDbSeeder
    {
        Task SeedAsync();
    }

    public class DbSeeder : IDbSeeder
    {
        private readonly AppDbContext context;

        public DbSeeder(AppDbContext appDbContext)
        {
            context = appDbContext;
        }

        public async Task SeedAsync()
        {
            
            if (!context.AdminAccounts.Any())
            {
                context.AdminAccounts.AddRange(
                    new AdminAccounts
                    {
                        Email = "nealramos72@gmail.com",
                        UserName = "Neal",
                        Password = BCrypt.Net.BCrypt.HashPassword("admin"),
                        FirstName = "Neal",
                        LastName = "Ramos",
                    }
                );
                await context.SaveChangesAsync();
            }
            // var admin = context.AdminAccounts.First();
            // if (!context.Jobs.Any() && admin != null)
            // {
            //     context.Jobs.AddRange(
            //         new Jobs
            //         {
            //             Title = "Frontend Developer",
            //             Description = "Build responsive interfaces using React and TailwindCSS.",
            //             Roles = JsonSerializer.Serialize(new[]
            //             {
            //                 new { Name = "React", Level = "Intermediate" },
            //                 new { Name = "Tailwind", Level = "Intermediate" },
            //                 new { Name = "TypeScript", Level = "Advanced" }
            //             }),
            //             FileRequirements = JsonSerializer.Serialize(new[]
            //             {
            //                 new { label = "Resume", Required = true },
            //                 new { label = "Portfolio", Required = false }
            //             }),
            //             AdminAccounts = admin,
            //             DateCreated = DateHelper.GetPHTime()
            //         }
            //     );
            // }
            await context.SaveChangesAsync();
        }
    }
}