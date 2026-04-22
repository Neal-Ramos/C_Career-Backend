
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.configurations
{
    public class ApplicantInterviewsConfigurations: IEntityTypeConfiguration<ApplicantInterviews>
    {
        public void Configure(EntityTypeBuilder<ApplicantInterviews> builder)
        {
            builder.ToTable("ApplicantInterviews");

            builder.HasKey(a => a.Id);
            builder.HasIndex(a => a.InterviewId)
                .IsUnique();

            builder.Property(a => a.Id)
                .ValueGeneratedOnAdd();
            builder.Property(a => a.InterviewId)
                .HasDefaultValueSql("NEWID()")
                .IsRequired();
            builder.Property(a => a.DateInterview)
                .IsRequired();
            builder.Property(a => a.DateCreated)
                .IsRequired();
            builder.Property(a => a.Status)
                .IsRequired();

            //relation
            builder.HasOne(a => a.Application)
                .WithMany(a => a.Interviews)
                .HasForeignKey(a => a.ApplicationId)
                .HasPrincipalKey(a => a.ApplicationId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}