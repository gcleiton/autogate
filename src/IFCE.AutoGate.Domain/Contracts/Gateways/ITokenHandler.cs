using IFCE.AutoGate.Domain.Entities;

namespace IFCE.AutoGate.Domain.Contracts.Gateways;

public interface ITokenHandler
{
    Task<string> Generate(Administrator administrator);
}
