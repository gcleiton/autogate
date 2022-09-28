using IFCE.AutoGate.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace IFCE.AutoGate.Infrastructure.Extensions;

public static class ModelBuilderExtension
{
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
