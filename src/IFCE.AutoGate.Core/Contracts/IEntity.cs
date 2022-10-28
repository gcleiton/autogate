using IFCE.AutoGate.Core.Messages;

namespace IFCE.AutoGate.Core.Contracts;

public interface IEntity
{
    public Guid Id { get; }
    public IReadOnlyCollection<Event> Events { get; }

    public void AddEvent(Event message);

    public void RemoveEvent(Event message);

    public void ClearEvents();
}
