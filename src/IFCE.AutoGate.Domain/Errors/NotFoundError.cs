using IFCE.AutoGate.Core.DomainObjects;

namespace IFCE.AutoGate.Domain.Errors;

public class NotFoundError : Error
{
    public NotFoundError(string message) : base("Recurso não Encontrado")
    {
        Message = message;
    }
}
