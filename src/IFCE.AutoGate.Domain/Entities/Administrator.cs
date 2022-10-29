using IFCE.AutoGate.Core.DomainObjects;

namespace IFCE.AutoGate.Domain.Entities;

public class Administrator : Entity, IAggregateRoot
{
    // EF Constructor
    private Administrator()
    {
    }

    public Administrator(string name, string email)
    {
        Name = name;
        Email = email;
    }

    public Administrator(Guid id, string name, string email) : this(name, email)
    {
        Id = id;
    }

    public string Name { get; private set; }
    public string Email { get; private set; }
    public string? Password { get; private set; }
    public Guid? RecoveryPasswordCode { get; private set; }
    public DateTime? RecoveryPasswordExpiresAt { get; private set; }

    public void Rename(string name)
    {
        Name = name;
    }

    public void ChangeEmail(string email)
    {
        Email = email;
    }

    public void ForgetPassword()
    {
        RecoveryPasswordCode = Guid.NewGuid();
        var expiresAt = DateTime.Now.AddHours(2);
        RecoveryPasswordExpiresAt = expiresAt;
    }

    public void ChangePassword(string password)
    {
        Password = password;
        RecoveryPasswordCode = null;
        RecoveryPasswordExpiresAt = null;
    }

    public bool IsRecoveryPasswordValid()
    {
        if (RecoveryPasswordCode is null) return false;

        return RecoveryPasswordExpiresAt > DateTime.Now;
    }
}
