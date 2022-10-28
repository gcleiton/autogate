using IFCE.AutoGate.Core.Messages;

namespace IFCE.AutoGate.Application.UseCases.ChangePassword;

public class ChangePasswordCommand : Command<bool>
{
    public string Password { get; set; }
    public string PasswordConfirmation { get; set; }
    public Guid RecoveryPasswordCode { get; set; }
}
