using System.Net;

namespace IFCE.AutoGate.API.Responses;

public class ConflictResponse : Response
{
    public ConflictResponse(string message)
    {
        StatusCode = (int)HttpStatusCode.Conflict;
        Message = message;
    }
}
