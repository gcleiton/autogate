using IFCE.AutoGate.Core.Messages;

namespace IFCE.AutoGate.Application.Commands;

public class RecoverPasswordCommand : Command
{
    public string Email { get; set; }
}
