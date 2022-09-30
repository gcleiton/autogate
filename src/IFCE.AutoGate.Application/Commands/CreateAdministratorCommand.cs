using IFCE.AutoGate.Core.Messages;

namespace IFCE.AutoGate.Application.Commands;

public class CreateAdministratorCommand : Command
{
    public string Name { get; set; }
    public string Email { get; set; }
}
