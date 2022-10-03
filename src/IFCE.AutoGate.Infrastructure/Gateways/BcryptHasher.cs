using IFCE.AutoGate.Domain.Contracts.Gateways;

namespace IFCE.AutoGate.Infrastructure.Gateways;

public class BcryptHasher : IHasher
{
    private readonly int _salt;

    public BcryptHasher(int salt)
    {
        _salt = salt;
    }

    public string Hash(string plaintext)
    {
        return BCrypt.Net.BCrypt.HashPassword(plaintext, _salt);
    }

    public bool Validate(string plaintext, string digest)
    {
        return BCrypt.Net.BCrypt.Verify(plaintext, digest);
    }
}
