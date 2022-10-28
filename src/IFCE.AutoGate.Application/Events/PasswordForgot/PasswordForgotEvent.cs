using IFCE.AutoGate.Core.Messages;

namespace IFCE.AutoGate.Application.Events.PasswordForgot;

public class PasswordForgotEvent : Event
{
    public PasswordForgotEvent(string name, string email, Guid recoveryPasswordCode)
    {
        Name = name;
        Email = email;
        if (recoveryPasswordCode == Guid.Empty)
            throw new InvalidOperationException("O código de recuperação da senha é inválido");
        RecoveryPasswordCode = recoveryPasswordCode;
    }

    public string Name { get; }
    public string Email { get; }
    public Guid RecoveryPasswordCode { get; }
}
