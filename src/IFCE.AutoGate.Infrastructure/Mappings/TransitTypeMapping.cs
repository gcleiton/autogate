using IFCE.AutoGate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IFCE.AutoGate.Infrastructure.Mappings;

public class TransitTypeMapping : IEntityTypeConfiguration<TransitType>
{
    public void Configure(EntityTypeBuilder<TransitType> builder)
    {
        builder.ToTable("TransitTypes");

        builder.HasKey(t => t.Id);

        builder
            .Property(t => t.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder
            .Property(t => t.Name)
            .HasMaxLength(16)
            .IsRequired();
    }
}
