using IFCE.AutoGate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IFCE.AutoGate.Infrastructure.Mappings;

public class VehicleCategoryMapping : IEntityTypeConfiguration<VehicleCategory>
{
    public void Configure(EntityTypeBuilder<VehicleCategory> builder)
    {
        builder.ToTable("vehicle_categories");

        builder.HasKey(c => c.Id);

        builder
            .Property(c => c.Name)
            .HasMaxLength(64)
            .IsRequired();
    }
}
