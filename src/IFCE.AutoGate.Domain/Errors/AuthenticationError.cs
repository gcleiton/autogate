using IFCE.AutoGate.Core.DomainObjects;

namespace IFCE.AutoGate.Domain.Errors;

public class AuthenticationError : Error
{
    public AuthenticationError() : base("Erro de Autenticação", "Email e/ou senha inválidos")
    {
    }
}
