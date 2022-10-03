namespace IFCE.AutoGate.Domain.Contracts.Gateways;

public interface IHasher
{
    public string Hash(string plaintext);
    public bool Validate(string plaintext, string digest);
}
