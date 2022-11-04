using IFCE.AutoGate.Core.DomainObjects;

namespace IFCE.AutoGate.Domain.Entities;

public class Driver : Entity, IAggregateRoot
{
    private readonly IList<Vehicle> _vehicles;

    // EF Constructor
    private Driver()
    {
    }

    public Driver(string name, string email, DateOnly bornAt, string phone, string license, string tag,
        IList<Vehicle> vehicles)
    {
        Name = name;
        Email = email;
        BornAt = bornAt;
        Phone = phone;
        License = license;
        Tag = tag;
        _vehicles = vehicles;
    }

    public string Name { get; }
    public string Photo { get; private set; }
    public string Email { get; }
    public DateOnly BornAt { get; }
    public string Phone { get; }
    public string License { get; }
    public string Tag { get; }

    public IReadOnlyCollection<Vehicle> Vehicles => _vehicles.ToList();

    public void ChangePhoto(string photo)
    {
        Photo = photo;
    }
}
