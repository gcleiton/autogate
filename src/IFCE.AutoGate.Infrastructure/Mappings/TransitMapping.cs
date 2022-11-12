using IFCE.AutoGate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IFCE.AutoGate.Infrastructure.Mappings;

public class TransitMapping : IEntityTypeConfiguration<Transit>
{
    public void Configure(EntityTypeBuilder<Transit> builder)
    {
        builder.ToTable("Transits");

        builder
            .Property(t => t.DriverId)
            .IsRequired();

        builder
            .Property(t => t.VehicleId)
            .IsRequired();

        builder
            .Property(t => t.TransitTypeId)
            .IsRequired();

        builder.Property(t => t.TransitDate)
            .HasColumnType("TIMESTAMP")
            .IsRequired();

        builder
            .HasOne(t => t.Driver)
            .WithMany(d => d.Transits)
            .HasForeignKey(t => t.DriverId)
            .HasPrincipalKey(d => d.Id);

        builder
            .HasOne(t => t.Vehicle)
            .WithMany(v => v.Transits)
            .HasForeignKey(t => t.VehicleId)
            .HasPrincipalKey(v => v.Id);
    }
}
