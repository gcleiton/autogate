using IFCE.AutoGate.Core.Contracts;

namespace IFCE.AutoGate.Core.DomainObjects;

public abstract class Entity : IEntity, ITracking
{
    protected Entity()
    {
        Id = new Guid();
    }

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

    public override string ToString()
    {
        return $"{GetType().Name} [Id={Id}]";
    }
}
