using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TradingApp;

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

List<User> users = new List<User>(); // Creating List with all the registered users
List<Item> items = new List<Item>(); // Creating List with all the registered items
List<Trade> trades = new List<Trade>(); // Creating List with all the trades

// Two created Demo users for testing the program.
users.Add(new User("Manni", "1")); // User 1 
users.Add(new User("Test", "1")); // User 2 



int nextItemId = 1; // Counter for items
int nextTradeId = 1; // Counter for trades


bool running = true; // Controlling the Main Loop, keeps the program going.
User? active_user = null; //default value is null, meaning no user is logged in.


// Main Loop
while (running)
{
    Console.Clear(); // Clearing the screen to make the menu clear.
    //       Starting Menu "Offline Mode"
    if (active_user == null) // Controlling if someones logged in.
    {

        Console.WriteLine("\n--- Welcome to my TradingApp ---"); // Title info.
        Console.WriteLine("1) Register"); // Menu choice 1.
        Console.WriteLine("2) Login"); // Menu choice 2.
        Console.WriteLine("3) Exit"); // Menu choice 3.
        Console.Write("Choose an option: "); // Type your menu choice.

        string? choice = Console.ReadLine(); // Reading the chosen menu option.
        switch (choice) // Choosing behavior based on input
        {
            case "1": // Register an user

                RegisterUser(users); // Calling the method(sending the list with users)

                break; // stopping the case, back to menu.


            case "2": // Log in user 

                Console.Clear();
                Console.Write("Write username: ");

                String username = Console.ReadLine();

                Console.Write("Write password: ");
                String password = Console.ReadLine();

                foreach (User user in users)
                {
                    if (user.TryLogin(username, password))
                    {
                        active_user = user;
                        break;
                    }
                }

                if (!users.Contains(active_user))
                {
                    System.Console.WriteLine("Login failed. Press enter to continue");
                    Console.ReadLine();
                    continue;
                }
                break;

            case "3": // Exit
                running = false; // Exit program

                break;

            default:
                Console.WriteLine("Invalid choice. Press Enter to continue...");
                Console.ReadLine();  // waiting for user to press Enter

                break;
        }
    }
    else
    {
        Console.WriteLine($"Welcome {active_user}");

        Console.WriteLine("Trading Menu");
        Console.WriteLine("----    ----");
        Console.WriteLine("1. Add an item");
        Console.WriteLine("2. Avaible trading items");
        Console.WriteLine("Request a trade");
        Console.WriteLine("Browse trade requests");
        Console.WriteLine("Browse completed trades");
        Console.WriteLine("Logout");

        string? choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                AddItem(); // Adding an item

                break;

            case "2":
                /*                 SeeAvaibleItems(); // List of avaible items
                 */
                break;

            case "3":
                /*                 RequestTrade(); // Requesting a trade
                 */
                break;

            case "4":
                /*                 BrowseTradeRequests(); // Browse list of trade  requests
                 */
                break;

            case "5":
                /*                 BrowseCompletedTrades(); // Browse list of completed trades
                 */
                break;

            case "6":
                active_user = null; // Log out

                break;

        }
    }
}

static void RegisterUser(List<User> users)
{
    Console.Clear();
    Console.Write("New username: ");
    string newUsername = Console.ReadLine() ?? "";
    Console.Write("New password: ");
    string newPassword = Console.ReadLine() ?? "";


    // CREATE ERROR HANDLING FOR EXISTING USERNAME
    // IF USER EXISTS, DO NOTHING
    // IF USER DOESNT EXIST, CREATE NEW USER
    users.Add(new User(newUsername, newPassword));
}

void AddItem()
{
    
}