using IFCE.AutoGate.Core.Messages;

namespace IFCE.AutoGate.Application.UseCases.ReactivateDriver;

public class ReactivateDriverCommand : Command<bool>
{
    public ReactivateDriverCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}
