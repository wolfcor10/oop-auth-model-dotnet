using System;

namespace OopAuthModel.Domain;

public sealed class Credential
{
    public string PasswordHash { get; }
    public string Salt { get; }
    public DateTime LastChangedAt { get; } = DateTime.UtcNow;

    public Credential(string passwordHash, string salt)
    {
        PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
        Salt = salt ?? throw new ArgumentNullException(nameof(salt));
    }
}