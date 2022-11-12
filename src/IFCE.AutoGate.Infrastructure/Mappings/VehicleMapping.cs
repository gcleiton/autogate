using IFCE.AutoGate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IFCE.AutoGate.Infrastructure.Mappings;

public class VehicleMapping : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("Vehicles");

        builder.HasKey(v => v.Id);

        builder
            .Property(v => v.Plate)
            .HasMaxLength(7)
            .IsRequired();

        builder
            .Property(v => v.Model)
            .HasMaxLength(64)
            .IsRequired();

        builder
            .Property(d => d.Tag)
            .IsRequired();

        builder
            .Property(v => v.CategoryId)
            .IsRequired();
        builder
            .Property(v => v.DriverId)
            .IsRequired();

        builder
            .HasOne(v => v.Category)
            .WithMany(c => c.Vehicles)
            .HasForeignKey(v => v.CategoryId)
            .HasPrincipalKey(c => c.Id);

        builder
            .HasOne(v => v.Driver)
            .WithMany(d => d.Vehicles)
            .HasForeignKey(v => v.DriverId)
            .HasPrincipalKey(d => d.Id);

        builder.HasIndex(v => new { v.Plate, v.Tag }).IsUnique();
    }
}
