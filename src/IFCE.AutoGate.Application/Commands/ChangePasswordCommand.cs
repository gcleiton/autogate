using IFCE.AutoGate.Core.Messages;

namespace IFCE.AutoGate.Application.Commands;

public class ChangePasswordCommand : Command
{
    public string Password { get; set; }
    public string PasswordConfirmation { get; set; }
    public Guid RecoveryPasswordCode { get; set; }
}
