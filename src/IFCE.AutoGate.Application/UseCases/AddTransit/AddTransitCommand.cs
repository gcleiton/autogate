using IFCE.AutoGate.Core.Messages;

namespace IFCE.AutoGate.Application.UseCases.AddTransit;

public class AddTransitCommand : Command<bool>
{
    public string CardNumber { get; set; }
    public int TransitTypeId { get; set; }
}
