using System.Net;

namespace IFCE.AutoGate.API.Responses;

public class OkResponse : Response
{
    public OkResponse(string message)
    {
        StatusCode = (int)HttpStatusCode.OK;
        Message = message;
    }
}
