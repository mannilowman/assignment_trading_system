using System.Diagnostics;
namespace TradingApp;

// User - A created user
class User
{
    public string Username { get; set; } = "";
    private string Password { get; set; } = "";

    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public bool TryLogin(string username, string password)
    {
        return username == Username && password == Password;

    }

    public override string ToString()
    {
        return $"Username: {Username}";
    }
}
