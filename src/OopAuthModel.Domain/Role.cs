using System;
using System.Collections.Generic;
using System.Security;

namespace OopAuthModel.Domain;

public sealed class Role
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; }

    private readonly HashSet<Permission> _permissions = new();
    public IReadOnlyCollection<Permission> Permissions => _permissions;

    public Role(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    public void AddPermission(Permission permission)
    {
        if (permission is null) throw new ArgumentNullException(nameof(permission));
        _permissions.Add(permission);
    }
}