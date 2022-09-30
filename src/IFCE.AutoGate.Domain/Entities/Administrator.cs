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

    public void ForgetPassword()
    {
        RecoveryPasswordCode = new Guid();
        var expiresAt = DateTime.Now.AddHours(2);
        RecoveryPasswordExpiresAt = expiresAt;
    }
}
