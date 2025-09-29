namespace TradingApp;

interface IUser
{
    string Username { get; }
    public bool TryLogin(string username, string password);

    string ToString();
}
