using IFCE.AutoGate.Core.Contracts;

namespace IFCE.AutoGate.Core.DomainObjects;

public abstract class Entity : EntityBase, ITracking
{
    protected Entity(Guid id = default) : base(id)
    {
    }

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
}
