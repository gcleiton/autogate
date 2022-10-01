using IFCE.AutoGate.Core.Messages;

namespace IFCE.AutoGate.Core.Contracts;

public interface IEntity
{
    public Guid Id { get; }
    public IReadOnlyCollection<Event> Notifications { get; }

    public void AddNotification(Event notification);

    public void RemoveNotification(Event notification);

    public void ClearNotifications();
}
