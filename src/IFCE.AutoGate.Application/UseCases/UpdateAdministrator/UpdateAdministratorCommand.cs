using IFCE.AutoGate.Core.Messages;

namespace IFCE.AutoGate.Application.UseCases.UpdateAdministrator;

public class UpdateAdministratorCommand : Command<bool>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
