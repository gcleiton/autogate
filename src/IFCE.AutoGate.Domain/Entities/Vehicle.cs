using IFCE.AutoGate.Core.DomainObjects;

namespace IFCE.AutoGate.Domain.Entities;

public class Vehicle : Entity
{
    private readonly IList<Transit> _transits = new List<Transit>();

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
    public Guid DriverId { get; private set; }

    public VehicleCategory Category { get; private set; }

    public Driver Driver { get; private set; }

    public IReadOnlyCollection<Transit> Transits => _transits.ToList();
}
