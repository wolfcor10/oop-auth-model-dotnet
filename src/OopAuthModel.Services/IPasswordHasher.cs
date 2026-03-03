namespace OopAuthModel.Services;

public interface IPasswordHasher
{
    string Hash(string plain, string salt);
    bool Verify(string plain, string salt, string expectedHash);
}