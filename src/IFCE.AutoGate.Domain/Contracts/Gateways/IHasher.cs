namespace IFCE.AutoGate.Domain.Contracts.Gateways;

public interface IHasher
{
    public string Hash(string plaintext);
}
