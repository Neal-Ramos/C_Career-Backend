using Domain.Entities;
using Domain.enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.configurations
{
    public class ApplicationsConfigurations: IEntityTypeConfiguration<JobApplications>
    {
        public void Configure(EntityTypeBuilder<JobApplications> builder)
        {
            builder.ToTable("JobApplications");

            builder.HasKey(a => a.Id);
            builder.HasIndex(a => a.ApplicationId)
                .IsUnique();

            builder.Property(a => a.Id)
                .ValueGeneratedOnAdd();

            builder.Property(a => a.ApplicationId)
                .HasDefaultValueSql("NEWID()")
                .IsRequired();

            builder.Property(a => a.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.MiddleName)
                .HasMaxLength(100);

            builder.Property(a => a.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.ContactNumber)
                .IsRequired()
                .HasMaxLength(11);

            builder.Property(a => a.UniversityName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Degree)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.GraduationYear)
                .IsRequired();

            builder.Property(a => a.FileSubmitted);

            builder.Property(a => a.DateSubmitted)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(a => a.Status)
                .HasConversion<string>()
                .HasDefaultValue(ApplicationStatusEnum.Pending);
                
            builder.Property(a => a.DateReviewed);

            //relation
            builder.HasOne(a => a.Job)
                .WithMany(j => j.JobApplications)
                .HasForeignKey(a => a.JobId)
                .HasPrincipalKey(j => j.JobId);
        }
    }
}