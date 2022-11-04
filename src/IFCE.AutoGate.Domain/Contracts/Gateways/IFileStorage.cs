using Microsoft.AspNetCore.Http;

namespace IFCE.AutoGate.Domain.Contracts.Gateways;

public interface IFileStorage
{
    Task<string> Upload(IFormFile file);
}
