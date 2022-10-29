using IFCE.AutoGate.Core.DomainObjects;

namespace IFCE.AutoGate.Domain.Errors;

public class UnexpectedError : Error
{
    public UnexpectedError() : base("Falha Interna no Servidor",
        "Ocorreu um erro inesperado. Caso o problema persista, contate o administrador do sistema.")
    {
    }
}
