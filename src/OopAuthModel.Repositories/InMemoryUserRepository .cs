using System;
using System.Collections.Generic;
using OopAuthModel.Domain;

namespace OopAuthModel.Repositories;

public sealed class InMemoryUserRepository : IUserRepository
{
    private readonly Dictionary<string, User> _byUsername = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, User> _byEmail = new(StringComparer.OrdinalIgnoreCase);

    public User? FindByUsernameOrEmail(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return null;

        if (_byUsername.TryGetValue(value, out var u)) return u;
        if (_byEmail.TryGetValue(value, out u)) return u;

        return null;
    }

    public void Save(User user)
    {
        _byUsername[user.Username] = user;
        _byEmail[user.Email] = user;
    }
}