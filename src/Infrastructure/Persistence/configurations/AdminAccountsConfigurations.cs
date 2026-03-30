using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                .WithOne(a => a.Owner)
                .HasForeignKey(a => a.OwnerId)
                .HasPrincipalKey(a => a.AdminId);

            builder.HasMany(a => a.CreatedJobs)
                .WithOne(j => j.CreatedBy)
                .HasForeignKey(j => j.CreatorId)
                .HasPrincipalKey(a => a.AdminId);
        }
    }
}