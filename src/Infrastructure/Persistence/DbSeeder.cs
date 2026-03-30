using System.Text.Json;
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
                        Email = "nealramos72",
                        UserName = "Neal",
                        Password = BCrypt.Net.BCrypt.HashPassword("admin"),
                        FirstName = "Neal",
                        LastName = "Ramos",
                    }
                );
                await context.SaveChangesAsync();
            }
            var admin = context.AdminAccounts.First();
            if (!context.Jobs.Any() && admin != null)
            {
                context.Jobs.AddRange(
                    new Jobs
                    {
                        Title = "Frontend Developer",
                        Description = "Build responsive interfaces using React and TailwindCSS.",
                        Roles = JsonSerializer.Serialize(new[]
                        {
                            new { Name = "React", Level = "Intermediate" },
                            new { Name = "Tailwind", Level = "Intermediate" },
                            new { Name = "TypeScript", Level = "Advanced" }
                        }),
                        FileRequirements = JsonSerializer.Serialize(new[]
                        {
                            new { FileName = "Resume", Required = true },
                            new { FileName = "Portfolio", Required = false }
                        }),
                        CreatedBy = admin
                    },
                    new Jobs
                    {
                        Title = "Backend Developer",
                        Description = "Develop APIs and backend systems using .NET Core and PostgreSQL.",
                        Roles = JsonSerializer.Serialize(new[]
                        {
                            new { Name = ".NET Core", Level = "Advanced" },
                            new { Name = "PostgreSQL", Level = "Intermediate" },
                            new { Name = "REST API", Level = "Advanced" }
                        }),
                        FileRequirements = JsonSerializer.Serialize(new[]
                        {
                            new { FileName = "Resume", Required = true }
                        }),
                        CreatedBy = admin
                    },
                    new Jobs
                    {
                        Title = "Fullstack Developer",
                        Description = "Work on both frontend and backend systems using React and Node.js.",
                        Roles = JsonSerializer.Serialize(new[]
                        {
                            new { Name = "React", Level = "Intermediate" },
                            new { Name = "Node.js", Level = "Intermediate" },
                            new { Name = "MongoDB", Level = "Intermediate" }
                        }),
                        FileRequirements = JsonSerializer.Serialize(new[]
                        {
                            new { FileName = "Resume", Required = true },
                            new { FileName = "Portfolio", Required = true }
                        }),
                        CreatedBy = admin
                    },
                    new Jobs
                    {
                        Title = "DevOps Engineer",
                        Description = "Maintain CI/CD pipelines and infrastructure using AWS and Docker.",
                        Roles = JsonSerializer.Serialize(new[]
                        {
                            new { Name = "AWS", Level = "Advanced" },
                            new { Name = "Docker", Level = "Intermediate" },
                            new { Name = "Terraform", Level = "Intermediate" }
                        }),
                        FileRequirements = JsonSerializer.Serialize(new[]
                        {
                            new { FileName = "Resume", Required = true },
                            new { FileName = "Certifications", Required = false }
                        }),
                        CreatedBy = admin
                    },
                    new Jobs
                    {
                        Title = "UI/UX Designer",
                        Description = "Design beautiful user interfaces and seamless user experiences.",
                        Roles = JsonSerializer.Serialize(new[]
                        {
                            new { Name = "Figma", Level = "Advanced" },
                            new { Name = "Adobe XD", Level = "Intermediate" }
                        }),
                        FileRequirements = JsonSerializer.Serialize(new[]
                        {
                            new { FileName = "Portfolio", Required = true }
                        }),
                        CreatedBy = admin
                    },
                    new Jobs
                    {
                        Title = "Mobile App Developer",
                        Description = "Build cross-platform mobile apps using Flutter and Dart.",
                        Roles = JsonSerializer.Serialize(new[]
                        {
                            new { Name = "Flutter", Level = "Advanced" },
                            new { Name = "Dart", Level = "Advanced" }
                        }),
                        FileRequirements = JsonSerializer.Serialize(new[]
                        {
                            new { FileName = "Resume", Required = true },
                            new { FileName = "Portfolio", Required = true }
                        }),
                        CreatedBy = admin
                    },
                    new Jobs
                    {
                        Title = "Data Scientist",
                        Description = "Analyze large datasets using Python and machine learning models.",
                        Roles = JsonSerializer.Serialize(new[]
                        {
                            new { Name = "Python", Level = "Advanced" },
                            new { Name = "Machine Learning", Level = "Intermediate" },
                            new { Name = "Pandas", Level = "Advanced" }
                        }),
                        FileRequirements = JsonSerializer.Serialize(new[]
                        {
                            new { FileName = "Resume", Required = true }
                        }),
                        CreatedBy = admin
                    },
                    new Jobs
                    {
                        Title = "AI/ML Engineer",
                        Description = "Develop AI models and integrate them into applications.",
                        Roles = JsonSerializer.Serialize(new[]
                        {
                            new { Name = "TensorFlow", Level = "Advanced" },
                            new { Name = "PyTorch", Level = "Intermediate" },
                            new { Name = "Python", Level = "Advanced" }
                        }),
                        FileRequirements = JsonSerializer.Serialize(new[]
                        {
                            new { FileName = "Resume", Required = true },
                            new { FileName = "Portfolio", Required = false }
                        }),
                        CreatedBy = admin
                    },
                    new Jobs
                    {
                        Title = "Product Manager",
                        Description = "Lead product strategy and work closely with engineering teams.",
                        Roles = JsonSerializer.Serialize(new[]
                        {
                            new { Name = "Agile", Level = "Advanced" },
                            new { Name = "Scrum", Level = "Intermediate" }
                        }),
                        FileRequirements = JsonSerializer.Serialize(new[]
                        {
                            new { FileName = "Resume", Required = true }
                        }),
                        CreatedBy = admin
                    },
                    new Jobs
                    {
                        Title = "QA Engineer",
                        Description = "Test software applications to ensure quality and reliability.",
                        Roles = JsonSerializer.Serialize(new[]
                        {
                            new { Name = "Selenium", Level = "Intermediate" },
                            new { Name = "Cypress", Level = "Intermediate" }
                        }),
                        FileRequirements = JsonSerializer.Serialize(new[]
                        {
                            new { FileName = "Resume", Required = true }
                        }),
                        CreatedBy = admin
                    },
                    new Jobs
                    {
                        Title = "Cloud Engineer",
                        Description = "Manage cloud infrastructure using AWS, Azure, or GCP.",
                        Roles = JsonSerializer.Serialize(new[]
                        {
                            new { Name = "AWS", Level = "Advanced" },
                            new { Name = "Azure", Level = "Intermediate" },
                            new { Name = "GCP", Level = "Intermediate" }
                        }),
                        FileRequirements = JsonSerializer.Serialize(new[]
                        {
                            new { FileName = "Resume", Required = true },
                            new { FileName = "Certifications", Required = false }
                        }),
                        CreatedBy = admin
                    },
                    new Jobs
                    {
                        Title = "Security Engineer",
                        Description = "Implement security best practices and perform audits.",
                        Roles = JsonSerializer.Serialize(new[]
                        {
                            new { Name = "Penetration Testing", Level = "Intermediate" },
                            new { Name = "Network Security", Level = "Advanced" }
                        }),
                        FileRequirements = JsonSerializer.Serialize(new[]
                        {
                            new { FileName = "Resume", Required = true }
                        }),
                        CreatedBy = admin
                    },
                    new Jobs
                    {
                        Title = "Database Administrator",
                        Description = "Manage and optimize databases for performance and reliability.",
                        Roles = JsonSerializer.Serialize(new[]
                        {
                            new { Name = "PostgreSQL", Level = "Advanced" },
                            new { Name = "SQL Server", Level = "Intermediate" }
                        }),
                        FileRequirements = JsonSerializer.Serialize(new[]
                        {
                            new { FileName = "Resume", Required = true }
                        }),
                        CreatedBy = admin
                    },
                    new Jobs
                    {
                        Title = "Technical Writer",
                        Description = "Create documentation and guides for technical products.",
                        Roles = JsonSerializer.Serialize(new[]
                        {
                            new { Name = "Markdown", Level = "Advanced" },
                            new { Name = "Docs", Level = "Intermediate" }
                        }),
                        FileRequirements = JsonSerializer.Serialize(new[]
                        {
                            new { FileName = "Portfolio", Required = true }
                        }),
                        CreatedBy = admin
                    },
                    new Jobs
                    {
                        Title = "Support Engineer",
                        Description = "Provide technical support and troubleshoot customer issues.",
                        Roles = JsonSerializer.Serialize(new[]
                        {
                            new { Name = "Customer Support", Level = "Intermediate" },
                            new { Name = "Troubleshooting", Level = "Advanced" }
                        }),
                        FileRequirements = JsonSerializer.Serialize(new[]
                        {
                            new { FileName = "Resume", Required = true }
                        }),
                        CreatedBy = admin
                    }
                );

                await context.SaveChangesAsync();
            }
        }
    }
}