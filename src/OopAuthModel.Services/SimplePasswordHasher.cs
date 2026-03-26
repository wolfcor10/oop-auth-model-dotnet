using System;
using System.Security.Cryptography;
using System.Text;

namespace OopAuthModel.Services;

public sealed class SimplePasswordHasher : IPasswordHasher
{
    public string Hash(string plain, string salt)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(salt + plain));
        return Convert.ToHexString(bytes);
    }

    public bool Verify(string plain, string salt, string expectedHash)
        => string.Equals(Hash(plain, salt), expectedHash, StringComparison.OrdinalIgnoreCase);
}