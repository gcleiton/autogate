using IFCE.AutoGate.Core.DomainObjects;

namespace IFCE.AutoGate.Domain.Entities;

public class Driver : Entity, IAggregateRoot
{
    private readonly List<Vehicle> _vehicles;

    // EF Constructor
    private Driver()
    {
    }

    public Driver(string name, string email, DateTime bornAt, string phone, string license, string tag,
        List<Vehicle> vehicles)
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
    public string PhotoUrl { get; }
    public string Email { get; }
    public DateTime BornAt { get; }
    public string Phone { get; }
    public string License { get; }
    public string Tag { get; }

    public IReadOnlyCollection<Vehicle> Vehicles => _vehicles;
}
