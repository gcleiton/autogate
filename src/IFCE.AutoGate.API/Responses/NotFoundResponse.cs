using System.Net;

namespace IFCE.AutoGate.API.Responses;

public class NotFoundResponse : Response
{
    public NotFoundResponse(string message)
    {
        StatusCode = (int)HttpStatusCode.NotFound;
        Message = message;
    }
}
