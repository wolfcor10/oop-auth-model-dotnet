using OopAuthModel.Domain;

namespace OopAuthModel.Repositories;

public interface IUserRepository
{
    User? FindByUsernameOrEmail(string value);
    void Save(User user);
}