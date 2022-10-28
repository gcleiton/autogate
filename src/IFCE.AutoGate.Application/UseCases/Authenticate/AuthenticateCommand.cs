using IFCE.AutoGate.Core.Messages;

namespace IFCE.AutoGate.Application.UseCases.Authenticate;

public class AuthenticateCommand : Command<AuthenticateCommandResult>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
