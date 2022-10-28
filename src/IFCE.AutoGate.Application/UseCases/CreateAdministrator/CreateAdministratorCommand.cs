using IFCE.AutoGate.Core.Messages;

namespace IFCE.AutoGate.Application.UseCases.CreateAdministrator;

public class CreateAdministratorCommand : Command<bool>
{
    public string Name { get; set; }
    public string Email { get; set; }
}
