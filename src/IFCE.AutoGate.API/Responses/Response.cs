using System.Text.Json.Serialization;

namespace IFCE.AutoGate.API.Responses;

public abstract class Response
{
    [JsonPropertyOrder(1)] public int StatusCode { get; set; }
    [JsonPropertyOrder(2)] public string Message { get; set; }
}
