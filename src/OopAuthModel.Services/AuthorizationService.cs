using System.Linq;
using OopAuthModel.Domain;

namespace OopAuthModel.Services;

public sealed class AuthorizationService
{
    public bool HasPermission(User user, string permissionCode)
    {
        return user.Roles
            .SelectMany(r => r.Permissions)
            .Any(p => p.Code == permissionCode);
    }
}