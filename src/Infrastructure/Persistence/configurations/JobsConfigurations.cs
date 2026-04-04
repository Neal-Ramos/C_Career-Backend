using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.configurations
{
    public class JobsConfigurations: IEntityTypeConfiguration<Jobs>
    {
        public void Configure(EntityTypeBuilder<Jobs> builder)
        {
            builder.ToTable("Jobs");

            builder.HasKey(j => j.Id);
            builder.HasIndex(j => j.JobId)
                .IsUnique();

            builder.Property(j => j.Id)
                .ValueGeneratedOnAdd();
                
            builder.Property(j => j.JobId)
                .HasDefaultValueSql("NEWID()")
                .IsRequired();

            builder.Property(j => j.Title)
                .HasMaxLength(200)
                .IsRequired();
            
            builder.Property(j => j.Description)
                .IsRequired();

            builder.Property(j => j.Roles)
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(j => j.FileRequirements)
                .IsRequired();

            builder.Property(j => j.DateCreated)
                .IsRequired();

            builder.Property(j => j.EditedBy);
            
            //relation
            builder.HasOne(j => j.AdminAccounts)
                .WithMany(a => a.CreatedJobs)
                .HasForeignKey(a => a.CreatorId)
                .HasPrincipalKey(j => j.AdminId);

            builder.HasMany(j => j.JobApplications)
                .WithOne(a => a.Job)
                .HasForeignKey(a => a.JobId)
                .HasPrincipalKey(j => j.JobId);
            }
    }
}