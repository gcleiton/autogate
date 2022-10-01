using System.Reflection;
using IFCE.AutoGate.Core.Contracts;
using IFCE.AutoGate.Core.Messages;
using IFCE.AutoGate.Domain.Contracts.Gateways;
using IFCE.AutoGate.Domain.Entities;
using IFCE.AutoGate.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace IFCE.AutoGate.Infrastructure;

public class ApplicationDbContext : DbContext, IUnitOfWork
{
    private readonly IMediatorHandler _mediatorHandler;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMediatorHandler mediatorHandler) :
        base(options)
    {
        _mediatorHandler = mediatorHandler;
    }

    public DbSet<Administrator> Administrators { get; set; }

    public async Task Commit()
    {
        var isSuccess = await SaveChangesAsync() > 0;
        if (isSuccess) PublishEvents();
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyDefaultIgnoreModels();
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
        var entityEntries = GetTrackedEntities()
            .Where(e => e.State is EntityState.Added or EntityState.Modified);

        foreach (var entityEntry in entityEntries)
            if (entityEntry.State == EntityState.Added)
                ((ITracking)entityEntry.Entity).AddCreator(null);
            else
                ((ITracking)entityEntry.Entity).ChangeModifier(null);
    }

    private async Task PublishEvents()
    {
        var entitiesWithEvents = GetTrackedEntities()
            .Where(e => e.Entity.Notifications.Any());

        var events = GetEventsFromEntities(entitiesWithEvents);
        ClearEventsFromEntities(entitiesWithEvents);

        var tasks = events.Select(async e => await _mediatorHandler.PublishEvent(e));
        await Task.WhenAll(tasks);
    }

    private IEnumerable<EntityEntry<IEntity>> GetTrackedEntities()
    {
        return ChangeTracker
            .Entries<IEntity>();
    }

    private IEnumerable<Event> GetEventsFromEntities(IEnumerable<EntityEntry<IEntity>> entities)
    {
        return entities
            .SelectMany(e => e.Entity.Notifications)
            .ToList();
    }

    private void ClearEventsFromEntities(IEnumerable<EntityEntry<IEntity>> entities)
    {
        entities.ToList().ForEach(e => e.Entity.ClearNotifications());
    }
}