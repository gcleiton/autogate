using System.Reflection;
using IFCE.AutoGate.Core.Contracts;
using IFCE.AutoGate.Domain.Entities;
using IFCE.AutoGate.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace IFCE.AutoGate.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Administrator> Administrators { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.ApplyForeignKeysBehavior();
        builder.ApplyTrackingBehavior();

        base.OnModelCreating(builder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyTracking();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void ApplyTracking()
    {
        var entityEntries = ChangeTracker.Entries()
            .Where(e => e.Entity is IEntity && e.State is EntityState.Added or EntityState.Modified);

        foreach (var entityEntry in entityEntries)
            if (entityEntry.State == EntityState.Added)
                ((ITracking)entityEntry.Entity).AddCreator(null);
            else
                ((ITracking)entityEntry.Entity).ChangeModifier(null);
    }
}
