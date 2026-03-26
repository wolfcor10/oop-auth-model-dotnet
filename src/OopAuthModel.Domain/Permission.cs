using System;

namespace OopAuthModel.Domain;

public sealed class Permission
{
    public string Code { get; }

    public Permission(string code)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
    }
}