using OopAuthModel.Domain;
using OopAuthModel.Repositories;

namespace OopAuthModel.Services;

public sealed class AuthenticationService
{
    private readonly IUserRepository _users;
    private readonly IPasswordHasher _hasher;

    public AuthenticationService(IUserRepository users, IPasswordHasher hasher)
    {
        _users = users;
        _hasher = hasher;
    }

    public User? Login(string usernameOrEmail, string password)
    {
        var user = _users.FindByUsernameOrEmail(usernameOrEmail);
        if (user is null) return null;
        if (!user.IsActive) return null;

        var ok = _hasher.Verify(password, user.Credential.Salt, user.Credential.PasswordHash);
        return ok ? user : null;
    }
}