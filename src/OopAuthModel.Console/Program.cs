using System;
using System.Linq;
using OopAuthModel.Domain;
using OopAuthModel.Repositories;
using OopAuthModel.Services;

namespace OopAuthModel.Console;

public static class Program
{
    public static void Main()
    {
        IUserRepository userRepo = new InMemoryUserRepository();
        IPasswordHasher hasher = new SimplePasswordHasher();
        var authService = new AuthenticationService(userRepo, hasher);
        var authorizationService = new AuthorizationService();

        SeedData(userRepo, hasher);

        User? sessionUser = null;

        while (true)
        {
            System.Console.WriteLine("\n=== Identity Console (.NET) ===");
            System.Console.WriteLine("1) Login");
            System.Console.WriteLine("2) Who am I?");
            System.Console.WriteLine("3) Check permission");
            System.Console.WriteLine("4) Logout");
            System.Console.WriteLine("0) Exit");
            System.Console.Write("Select: ");

            var choice = System.Console.ReadLine();

            switch (choice)
            {
                case "1":
                    System.Console.Write("Username or email: ");
                    var u = System.Console.ReadLine() ?? "";
                    System.Console.Write("Password: ");
                    var p = System.Console.ReadLine() ?? "";

                    var logged = authService.Login(u, p);
                    if (logged is not null)
                    {
                        sessionUser = logged;
                        System.Console.WriteLine($"Login OK. Welcome, {sessionUser.Username}");
                    }
                    else
                    {
                        System.Console.WriteLine("Login failed.");
                    }
                    break;

                case "2":
                    if (sessionUser is null)
                    {
                        System.Console.WriteLine("Not logged in.");
                    }
                    else
                    {
                        System.Console.WriteLine($"Logged as: {sessionUser.Username} ({sessionUser.Email})");
                        System.Console.WriteLine($"UserId: {sessionUser.Id}");

                        var roles = sessionUser.Roles.Select(r => r.Name).ToArray();
                        System.Console.WriteLine("Roles: " + (roles.Length == 0 ? "(none)" : string.Join(", ", roles)));
                    }
                    break;

                case "3":
                    if (sessionUser is null)
                    {
                        System.Console.WriteLine("Please login first.");
                        break;
                    }

                    System.Console.Write("Permission code (e.g., CATALOG_UPDATE): ");
                    var code = System.Console.ReadLine() ?? "";
                    var ok = authorizationService.HasPermission(sessionUser, code);
                    System.Console.WriteLine(ok ? "Permission GRANTED" : "Permission DENIED");
                    break;

                case "4":
                    sessionUser = null;
                    System.Console.WriteLine("Logged out.");
                    break;

                case "0":
                    System.Console.WriteLine("Bye!");
                    return;

                default:
                    System.Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }

    private static void SeedData(IUserRepository repo, IPasswordHasher hasher)
    {
        var pUpdateCatalog = new Permission("CATALOG_UPDATE");
        var adminRole = new Role("ADMIN");
        adminRole.AddPermission(pUpdateCatalog);

        var salt = "SALT123"; // demo only
        var hash = hasher.Hash("admin123", salt);
        var cred = new Credential(hash, salt);

        var admin = new User("admin", "admin@mail.com", cred);
        admin.AddRole(adminRole);

        repo.Save(admin);
    }
}