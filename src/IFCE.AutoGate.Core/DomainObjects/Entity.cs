using IFCE.AutoGate.Core.Contracts;
using IFCE.AutoGate.Core.Messages;

namespace IFCE.AutoGate.Core.DomainObjects;

public abstract class Entity : IEntity, ITracking
{
    private readonly List<Event> _notifications;

    protected Entity()
    {
        Id = Guid.NewGuid();
        _notifications = new List<Event>();
    }

    public IReadOnlyCollection<Event> Notifications => _notifications.AsReadOnly();

    public Guid Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public int? CreatedBy { get; protected set; }
    public DateTime ModifiedAt { get; protected set; }
    public int? ModifiedBy { get; protected set; }


    public void ChangeModifier(int? id)
    {
        ModifiedBy = id;
        ModifiedAt = DateTime.Now;
    }

    public void AddCreator(int? id)
    {
        if (CreatedBy is not null)
            throw new InvalidOperationException("There is already a creator set for this entity");

        CreatedBy = id;
        CreatedAt = DateTime.Now;
    }

    #region Overrides

    public override string ToString()
    {
        return $"{GetType().Name} [Id={Id}]";
    }

    #endregion

    #region Notifications

    public void AddNotification(Event notification)
    {
        _notifications.Add(notification);
    }

    public void RemoveNotification(Event notification)
    {
        _notifications.Remove(notification);
    }

    public void ClearNotifications()
    {
        _notifications.Clear();
    }

    #endregion

    #region Comparisons

    public override bool Equals(object obj)
    {
        var compareTo = obj as Entity;

        if (ReferenceEquals(this, compareTo)) return true;
        if (compareTo is null) return false;

        return Id.Equals(compareTo.Id);
    }

    public static bool operator ==(Entity a, Entity b)
    {
        if (a is null && b is null)
            return true;

        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(Entity a, Entity b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        return GetType().GetHashCode() * 907 + Id.GetHashCode();
    }

    #endregion
}
