using IFCE.AutoGate.Core.Messages;

namespace IFCE.AutoGate.Application.UseCases.DeleteDriver;

public class DeleteDriverCommand : Command<bool>
{
    public DeleteDriverCommand(Guid id)
    {
        Id = id;
        AggregateId = id;
    }

    public Guid Id { get; set; }
}
