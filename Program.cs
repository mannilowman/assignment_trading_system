using System.Collections;
using App;


// TODO A user needs to be able to register an account
// TODO A user needs to be able to upload information about the item they with to trade.
// TODO A user needs to be able to browse a list of other users items.
// TODO A user needs to be able to request a trade for other users items.
// TODO A user needs to be able to browse trade requests.
// TODO A user needs to be able to accept a trade request.
// TODO A user needs to be able to deny a trade request.
// TODO A user needs to be able to browse completed requests.

// I have created three lists to store all the data for the users, items and the trades.
// All the data for the different lists is from the different classes (User,Item,Trade).
// I've named them users,items & trades.

List<User> users = new List<User>(); 
//List<Item> items = new List<Item>();
//List<Trade> trades = new List<Trade>();

users.Add(new User("Manni", "1")); // User 1 
users.Add(new User("Test", "1")); // User 2 

User? active_user = null; //default value is null, meaning no user is logged in.
bool running = true;

// Main Loop
while (running)
{
    Console.Clear();

    if (active_user == null)
    {
        //       Starting Menu "Offline Mode"
        Console.WriteLine("\n--- Welcome to my TradingApp ---");
        Console.WriteLine("1) Register");
        Console.WriteLine("2) Login");
        Console.WriteLine("3) Exit");
        Console.Write("Choose an option: ");

        string? choice = Console.ReadLine(); // Save the chosen menu option
        switch (choice)
        {
            case "1": // Register an user
                {
                    RegisterUser(users);

                    break;

                }





        }
    }
}

static void RegisterUser(List<User> users)
{
    Console.Clear();
    Console.Write("New username: ");
    string newUsername = Console.ReadLine() ?? "";
    Console.Write("New password: ");
    string NewPass = Console.ReadLine() ?? "";
    // Quick check to see that the username doesnt exist
    foreach (User user in users)
    {
        if (user.Username == newUsername)
        {
            Console.WriteLine("Username exists");
            Thread.Sleep(3000); // "Pausing program to show the information"
            break;
        }
    }
}