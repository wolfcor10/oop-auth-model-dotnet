using System;
using System.Collections.Generic;
using System.Data;
using System.Net;

namespace OopAuthModel.Domain;

public sealed class User
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Username { get; }
    public string Email { get; }
    public bool IsActive { get; private set; } = true;
    public DateTime CreatedAt { get; } = DateTime.UtcNow;

    // Composition: User owns Credential
    public Credential Credential { get; }

    private readonly HashSet<Role> _roles = new();
    public IReadOnlyCollection<Role> Roles => _roles;

    public User(string username, string email, Credential credential)
    {
        Username = username ?? throw new ArgumentNullException(nameof(username));
        Email = email ?? throw new ArgumentNullException(nameof(email));
        Credential = credential ?? throw new ArgumentNullException(nameof(credential));
    }

    public void AddRole(Role role)
    {
        if (role is null) throw new ArgumentNullException(nameof(role));
        _roles.Add(role);
    }

    public void Deactivate() => IsActive = false;
    public void Activate() => IsActive = true;
}