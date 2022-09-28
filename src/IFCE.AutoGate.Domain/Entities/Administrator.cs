using IFCE.AutoGate.Core.DomainObjects;

namespace IFCE.AutoGate.Domain.Entities;

public class Administrator : Entity
{
    public Administrator(string name, string email)
    {
        Name = name;
        Email = email;
    }

    public string Name { get; }
    public string Email { get; }
    public string? Password { get; private set; }
    public Guid? RecoveryPasswordCode { get; private set; }
    public DateTime? RecoveryPasswordExpiresAt { get; private set; }
}
