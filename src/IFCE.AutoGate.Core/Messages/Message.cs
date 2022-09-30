using System.Text.Json.Serialization;

namespace IFCE.AutoGate.Core.Messages;

public abstract class Message
{
    protected Message()
    {
        MessageType = GetType().Name;
        Timestamp = DateTime.Now;
    }

    public string MessageType { get; }
    [JsonIgnore] public Guid AggregateId { get; set; }
    public DateTime Timestamp { get; }
}
