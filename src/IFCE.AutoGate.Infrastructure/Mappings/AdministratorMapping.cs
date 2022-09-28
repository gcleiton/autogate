using IFCE.AutoGate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IFCE.AutoGate.Infrastructure.Mappings;

public class AdministratorMapping : IEntityTypeConfiguration<Administrator>
{
    public void Configure(EntityTypeBuilder<Administrator> builder)
    {
        builder.ToTable("Administrators");

        builder.HasKey(a => a.Id);

        builder
            .Property(a => a.Name)
            .HasMaxLength(150)
            .IsRequired();

        builder
            .Property(a => a.Email)
            .HasMaxLength(80)
            .IsRequired();

        builder
            .Property(a => a.Password)
            .HasMaxLength(256)
            .IsRequired(false);

        builder
            .Property(a => a.RecoveryPasswordCode)
            .HasMaxLength(256)
            .IsRequired(false);

        builder
            .Property(a => a.RecoveryPasswordExpiresAt)
            .HasColumnType("TIMESTAMP")
            .IsRequired(false);

        builder
            .HasIndex(a => a.Email)
            .IsUnique();
    }
}
