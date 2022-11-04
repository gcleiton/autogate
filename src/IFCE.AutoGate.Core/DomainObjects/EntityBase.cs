using IFCE.AutoGate.Core.Contracts;
using IFCE.AutoGate.Core.Messages;

namespace IFCE.AutoGate.Core.DomainObjects;

public abstract class EntityBase : IEntity
{
    private readonly List<Event> _events;

    protected EntityBase()
    {
        Id = Guid.NewGuid();
        _events = new List<Event>();
    }

    public IReadOnlyCollection<Event> Events => _events.AsReadOnly();

    public Guid Id { get; protected set; }

    #region Overrides

    public override string ToString()
    {
        return $"{GetType().Name} [Id={Id}]";
    }

    #endregion

    #region Notifications

    public void AddEvent(Event message)
    {
        _events.Add(message);
    }

    public void RemoveEvent(Event message)
    {
        _events.Remove(message);
    }

    public void ClearEvents()
    {
        _events.Clear();
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

    public static bool operator ==(EntityBase a, EntityBase b)
    {
        if (a is null && b is null)
            return true;

        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(EntityBase a, EntityBase b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        return GetType().GetHashCode() * 907 + Id.GetHashCode();
    }

    #endregion
}
