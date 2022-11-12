using IFCE.AutoGate.Core.DomainObjects;

namespace IFCE.AutoGate.Domain.Entities;

public class Transit : EntityNotTracked
{
    // EF Core
    private Transit()
    {
    }

    public Transit(Guid driverId, Guid vehicleId, TransitType transitType)
    {
        DriverId = driverId;
        VehicleId = vehicleId;
        TransitTypeId = transitType.Id;
        TransitDate = DateTime.Now;
    }

    public Guid DriverId { get; }
    public Guid VehicleId { get; }
    public int TransitTypeId { get; }
    public DateTime TransitDate { get; }

    public virtual Driver Driver { get; private set; }
    public virtual Vehicle Vehicle { get; private set; }
    public virtual TransitType TransitType { get; private set; }
}
