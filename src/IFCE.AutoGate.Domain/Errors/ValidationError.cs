using IFCE.AutoGate.Core.DomainObjects;

namespace IFCE.AutoGate.Domain.Errors;

public class ValidationError : Error
{
    public ValidationError(IEnumerable<string> errors) : base("Erro de Validação",
        "Ocorreu um ou mais erros de validação")
    {
        Errors = errors;
    }

    public IEnumerable<string> Errors { get; }
}
