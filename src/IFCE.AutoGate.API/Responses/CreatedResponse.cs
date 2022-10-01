using System.Net;

namespace IFCE.AutoGate.API.Responses;

public class CreatedResponse : Response
{
    public CreatedResponse(string message)
    {
        StatusCode = (int)HttpStatusCode.Created;
        Message = message;
    }
}
