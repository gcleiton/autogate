using System.Net;

namespace IFCE.AutoGate.API.Responses;

public class UnauthorizedResponse : Response
{
    public UnauthorizedResponse(string message)
    {
        StatusCode = (int)HttpStatusCode.Unauthorized;
        Message = message;
    }
}
