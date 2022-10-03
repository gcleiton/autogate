using IFCE.AutoGate.Application.Dtos;
using IFCE.AutoGate.Core.Messages;

namespace IFCE.AutoGate.Application.Requests;

public class LoginRequest : Request<AccessTokenDto>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
