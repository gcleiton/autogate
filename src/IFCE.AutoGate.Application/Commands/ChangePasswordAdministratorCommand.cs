using IFCE.AutoGate.Core.Messages;

namespace IFCE.AutoGate.Application.Commands;

public class ChangePasswordAdministratorCommand : Command
{
    public string Password { get; set; }
    public string PasswordConfirmation { get; set; }
    public Guid RecoveryPasswordCode { get; set; }
}
