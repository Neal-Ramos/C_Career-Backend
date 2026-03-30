using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.configurations
{
    public class AuthCodesConfigurations
    {
        public void Configure(EntityTypeBuilder<AuthCodes> builder)
        {
            builder.ToTable("AuthCodes");

            builder.HasKey(a => a.Id);
            builder.HasIndex(a => a.AuthCodeId)
                .IsUnique();

            builder.Property(a => a.Id)
                .ValueGeneratedOnAdd();

            builder.Property(a => a.AuthCodeId)
                .HasDefaultValueSql("NEWID()")
                .IsRequired();

            builder.Property(a => a.Code)
                .HasMaxLength(6)
                .IsRequired();

            builder.Property(a => a.DateCreated)
                .IsRequired();

            builder.Property(a => a.DateExpiry)
                .IsRequired();

            builder.Property(a => a.DateUsed)
                .HasDefaultValue(false);

            builder.Property(a => a.IsUsed)
                .IsRequired();

            
            //relations
            builder.HasOne(a => a.Owner)
                .WithMany(o => o.AuthCodes)
                .HasForeignKey(a => a.OwnerId)
                .HasPrincipalKey(o => o.AdminId);
        }
    }
}