using IFCE.AutoGate.Core.DomainObjects;

namespace IFCE.AutoGate.Core.Contracts;

public interface IResult
{
    public bool IsSuccess { get; }
    public bool IsFailure { get; }
    public Error Error { get; }
}
