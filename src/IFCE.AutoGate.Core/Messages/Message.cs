using System.Text.Json.Serialization;

namespace IFCE.AutoGate.Core.Messages;

public abstract class Message
{
    [JsonIgnore] public Guid AggregateId { get; set; }
}
