using System.Net;
using System.Text.Json.Serialization;

namespace IFCE.AutoGate.API.Responses;

public class UnprocessableEntityResponse : Response
{
    public UnprocessableEntityResponse(string message, IEnumerable<string> errors)
    {
        StatusCode = (int)HttpStatusCode.UnprocessableEntity;
        Message = message;
        Errors = errors;
    }

    [JsonPropertyOrder(3)] public IEnumerable<string> Errors { get; set; }
}
