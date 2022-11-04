using IFCE.AutoGate.Core.DomainObjects;

namespace IFCE.AutoGate.Domain.Entities;

public class VehicleCategory : EntityNotTracked
{
    // EF Constructor
    private VehicleCategory()
    {
    }

    public string Name { get; set; }

    public IReadOnlyCollection<Vehicle> Vehicles { get; set; }
}
