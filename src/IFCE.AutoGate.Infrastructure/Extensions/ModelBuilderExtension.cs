using IFCE.AutoGate.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace IFCE.AutoGate.Infrastructure.Extensions;

public static class ModelBuilderExtension
{
    public static void ApplyTrackingBehavior(this ModelBuilder builder)
    {
        var dateAttributes = new[] { "CreatedAt", "ModifiedAt" };
        var idAttributes = new[] { "CreatedBy", "ModifiedBy" };

        var entities = builder.GetEntities<IEntity>();

        var dateProperties = entities.SelectMany(e =>
            e.GetProperties().Where(p => p.ClrType == typeof(DateTime) && dateAttributes.Contains(p.Name)));

        foreach (var dateProperty in dateProperties)
        {
            dateProperty.SetColumnType("TIMESTAMP");
            dateProperty.SetDefaultValueSql("NOW()");
        }

        var idProperties = entities.SelectMany(e =>
            e.GetProperties().Where(p => p.ClrType == typeof(Guid) && idAttributes.Contains(p.Name)));

        foreach (var idProperty in idProperties) idProperty.IsNullable = true;
    }

    public static void ApplyForeignKeysBehavior(this ModelBuilder builder)
    {
        var foreignKeys = builder.GetEntities<IEntity>().SelectMany(e => e.GetForeignKeys());

        foreach (var foreignKey in foreignKeys) foreignKey.DeleteBehavior = DeleteBehavior.ClientSetNull;
    }

    private static IEnumerable<IMutableEntityType> GetEntities<T>(this ModelBuilder builder)
    {
        return builder.Model.GetEntityTypes().Where(e => e.ClrType.GetInterface(typeof(T).Name) != null).ToList();
    }
}
