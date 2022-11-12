using System.Linq.Expressions;
using IFCE.AutoGate.Core.Contracts;
using IFCE.AutoGate.Core.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;

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

    public static void ApplySoftDeleteBehavior(this ModelBuilder builder)
    {
        var entities = builder.GetEntities<ISoftDelete>();
        var disabledAtProperty = entities.SelectMany(e =>
            e.GetProperties().Where(p => p.ClrType == typeof(DateTime?) && p.Name == "DisabledAt")).FirstOrDefault();

        if (disabledAtProperty is not null)
        {
            disabledAtProperty.SetColumnType("TIMESTAMP");
            disabledAtProperty.IsNullable = true;
            disabledAtProperty.SetDefaultValue(null);
        }
    }

    public static void ApplyForeignKeysBehavior(this ModelBuilder builder)
    {
        var foreignKeys = builder.GetEntities<IEntity>().SelectMany(e => e.GetForeignKeys());

        foreach (var foreignKey in foreignKeys) foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
    }

    public static void ApplyGlobalFilters<T>(this ModelBuilder builder, Expression<Func<T, bool>> expression)
    {
        var entities = builder.GetEntities<T>().Select(e => e.ClrType);

        foreach (var entity in entities)
        {
            var newParam = Expression.Parameter(entity);
            var newBody = ReplacingExpressionVisitor.Replace(expression.Parameters.Single(), newParam, expression.Body);
            builder.Entity(entity).HasQueryFilter(Expression.Lambda(newBody, newParam));
        }
    }

    public static void ApplyDefaultIgnoreModels(this ModelBuilder builder)
    {
        builder.Ignore<Event>();
    }

    private static IEnumerable<IMutableEntityType> GetEntities<T>(this ModelBuilder builder)
    {
        return builder.Model.GetEntityTypes().Where(e => e.ClrType.GetInterface(typeof(T).Name) != null).ToList();
    }
}
