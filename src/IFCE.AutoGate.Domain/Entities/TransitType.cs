using IFCE.AutoGate.Core.DomainObjects;

namespace IFCE.AutoGate.Domain.Entities;

public class TransitType : Enumeration
{
    public static TransitType Entrance = new(1, nameof(Entrance));

    public static TransitType Exit = new(2, nameof(Exit));

    public TransitType(int id, string name) : base(id, name)
    {
    }
}