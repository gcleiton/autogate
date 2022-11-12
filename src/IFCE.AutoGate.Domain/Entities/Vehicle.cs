using IFCE.AutoGate.Core.DomainObjects;

namespace IFCE.AutoGate.Domain.Entities;

public class Vehicle : Entity
{
    // EF Constructor
    private Vehicle()
    {
    }

    public Vehicle(string plate, string model, string tag, Guid categoryId, Guid id = default) : base(id)
    {
        Plate = plate;
        Model = model;
        Tag = tag;
        CategoryId = categoryId;
    }

    public string Plate { get; }
    public string Model { get; }
    public string Tag { get; }
    public Guid CategoryId { get; }
    public Guid DriverId { get; }

    public VehicleCategory Category { get; }

    public Driver Driver { get; set; }
}
