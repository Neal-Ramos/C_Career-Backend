
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.configurations
{
    public class JobsEditHistoryConfigurations: IEntityTypeConfiguration<JobsEditHistory>
    {
        public void Configure(EntityTypeBuilder<JobsEditHistory> builder)
        {
            builder.ToTable("JobsEditHistory");

            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.EditId)
                .IsUnique();
            builder.HasIndex(e => e.EditorId);
            builder.HasIndex(e => e.JobId);

            builder.Property(a => a.Id)
                .ValueGeneratedOnAdd();
            builder.Property(a => a.EditId)
                .HasDefaultValueSql("NEWID()")
                .IsRequired();
            builder.Property(a => a.DateEdited)
                .IsRequired();
            builder.Property(a => a.EditSummary)
                .HasMaxLength(1000)
                .IsRequired();

            //Relations
            builder.HasOne(e => e.EditedBy)
                .WithMany(a => a.JobsEditedHistory)
                .HasForeignKey(a => a.EditorId)
                .HasPrincipalKey(e => e.AdminId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(e => e.Job)
                .WithMany(a => a.EditHistory)
                .HasForeignKey(a => a.JobId)
                .HasPrincipalKey(e => e.JobId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}