using IFCE.AutoGate.Core.DomainObjects;

namespace IFCE.AutoGate.Domain.Errors;

public class AlreadyExistsError : Error
{
    public AlreadyExistsError(string message) : base("Recurso Existente")
    {
        Message = message;
    }
}
