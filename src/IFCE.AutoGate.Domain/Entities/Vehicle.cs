using IFCE.AutoGate.Core.DomainObjects;

namespace IFCE.AutoGate.Domain.Entities;

public class Vehicle : Entity
{
    // EF Constructor
    private Vehicle()
    {
    }

    public Vehicle(string plate, string model, Guid categoryId)
    {
    }

    public string Plate { get; private set; }
    public string Model { get; private set; }
    public Guid CategoryId { get; private set; }
    public Guid DriverId { get; private set; }

    public VehicleCategory Category { get; private set; }

    public Driver Driver { get; set; }
}
