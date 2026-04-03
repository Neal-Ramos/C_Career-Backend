using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.configurations
{
    public class RefreshTokenConfiguration: IEntityTypeConfiguration<RefreshTokens>
    {
        public void Configure(EntityTypeBuilder<RefreshTokens> builder)
        {
            builder.ToTable("RefreshTokens");

            builder.HasKey(a => a.Id);
            builder.HasIndex(a => a.Token);

            builder.Property(a => a.Id)
                .ValueGeneratedOnAdd();

            builder.Property(a => a.Token)
                .IsRequired();

            builder.Property(a => a.IsRevoked)
                .HasDefaultValue(false);

            builder.Property(a => a.ExpiryDate)
                .IsRequired();

            builder.Property(a => a.DateCreated)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}