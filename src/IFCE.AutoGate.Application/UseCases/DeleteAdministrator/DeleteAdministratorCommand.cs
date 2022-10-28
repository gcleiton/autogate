using IFCE.AutoGate.Core.Messages;

namespace IFCE.AutoGate.Application.UseCases.DeleteAdministrator;

public class DeleteAdministratorCommand : Command<bool>
{
    public DeleteAdministratorCommand(Guid id)
    {
        Id = id;
        AggregateId = id;
    }

    public Guid Id { get; }
}
