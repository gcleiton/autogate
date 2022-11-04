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
        Plate = plate;
        Model = model;
        CategoryId = categoryId;
    }

    public string Plate { get; }
    public string Model { get; }
    public Guid CategoryId { get; }
    public Guid DriverId { get; private set; }

    public VehicleCategory Category { get; private set; }

    public Driver Driver { get; set; }
}
