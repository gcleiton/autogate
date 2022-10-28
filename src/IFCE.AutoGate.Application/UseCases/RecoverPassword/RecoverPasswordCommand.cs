using IFCE.AutoGate.Core.Messages;

namespace IFCE.AutoGate.Application.UseCases.RecoverPassword;

public class RecoverPasswordCommand : Command<bool>
{
    public string Email { get; set; }
}
