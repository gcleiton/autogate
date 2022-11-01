using IFCE.AutoGate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IFCE.AutoGate.Infrastructure.Mappings;

public class DriverMapping : IEntityTypeConfiguration<Driver>
{
    public void Configure(EntityTypeBuilder<Driver> builder)
    {
        builder.ToTable("drivers");

        builder.HasKey(d => d.Id);

        builder
            .Property(a => a.Name)
            .HasMaxLength(80)
            .IsRequired();

        builder
            .Property(a => a.PhotoUrl)
            .HasMaxLength(256)
            .IsRequired(false);

        builder
            .Property(d => d.Email)
            .HasMaxLength(80)
            .IsRequired();

        builder
            .Property(d => d.Phone)
            .HasMaxLength(11)
            .IsRequired();

        builder
            .Property(d => d.License)
            .HasMaxLength(10)
            .IsRequired();

        builder
            .Property(d => d.Tag)
            .IsRequired();

        builder
            .Property(d => d.BornAt)
            .HasColumnType("TIMESTAMP")
            .IsRequired();

        builder
            .HasMany(d => d.Vehicles)
            .WithOne(v => v.Driver)
            .HasForeignKey(v => v.DriverId)
            .HasPrincipalKey(d => d.Id);

        builder
            .HasIndex(d => new { d.Email, d.Tag })
            .IsUnique();
    }
}
