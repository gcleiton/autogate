using IFCE.AutoGate.Core.Contracts;
using IFCE.AutoGate.Core.DomainObjects;

namespace IFCE.AutoGate.Domain.Entities;

public class Driver : Entity, ISoftDelete, IAggregateRoot
{
    private readonly IList<Transit> _transits = new List<Transit>();
    private readonly IList<Vehicle> _vehicles = new List<Vehicle>();


    // EF Constructor
    private Driver()
    {
    }

    public Driver(string name, string email, DateOnly bornAt, string phone, string license,
        IEnumerable<Vehicle> vehicles, Guid id = default) : base(id)
    {
        Name = name;
        Email = email;
        BornAt = bornAt;
        Phone = phone;
        License = license;
        _vehicles = vehicles.ToList();
    }

    public string Name { get; }
    public string Photo { get; private set; }
    public string Email { get; }
    public DateOnly BornAt { get; }
    public string Phone { get; }
    public string License { get; }

    public IReadOnlyCollection<Vehicle> Vehicles => _vehicles.ToList();
    public IReadOnlyCollection<Transit> Transits => _transits.ToList();

    public DateTime? DisabledAt { get; private set; }

    public void Disable()
    {
        DisabledAt = DateTime.Now;
    }

    public void Enable()
    {
        DisabledAt = null;
    }

    public void ChangePhoto(string photo)
    {
        Photo = photo;
    }
}
