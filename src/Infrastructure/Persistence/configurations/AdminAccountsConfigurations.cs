using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.configurations
{
    public class AdminAccountsConfigurations: IEntityTypeConfiguration<AdminAccounts>
    {
        public void Configure(EntityTypeBuilder<AdminAccounts> builder)
        {
            builder.ToTable("AdminAccounts");
            
            builder.HasKey(a => a.Id);
            builder.HasIndex(a => a.AdminId)
                .IsUnique();


            builder.Property(a => a.Id)
                .ValueGeneratedOnAdd();
                
            builder.Property(a => a.AdminId)
                .HasDefaultValueSql("NEWID()")
                .IsRequired();

            builder.Property(a => a.Email)
                .IsRequired();

            builder.Property(a => a.UserName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Password)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.MiddleName)
                .IsRequired(false)
                .HasMaxLength(100);

            //relations
            builder.HasMany(a => a.AuthCodes)
                .WithOne(auth => auth.Owner)
                .HasForeignKey(auth => auth.OwnerId)
                .HasPrincipalKey(a => a.AdminId);

            builder.HasMany(a => a.CreatedJobs)
                .WithOne(j => j.AdminAccounts)
                .HasForeignKey(j => j.AdminId)
                .HasPrincipalKey(a => a.AdminId);
                
            builder.HasMany(a => a.JobsEditedHistory)
                .WithOne(e => e.EditedBy)
                .HasForeignKey(e => e.EditorId)
                .HasPrincipalKey(a => a.AdminId);

            builder.HasMany(a => a.ProcessedApplications)
                .WithOne(app => app.ProcessedBy)
                .HasForeignKey(app => app.AdminId)
                .HasPrincipalKey(a => a.AdminId);
        }
    }
}