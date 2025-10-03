using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.WebSockets;
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
        Console.WriteLine("3. Request a trade");
        Console.WriteLine("4. Browse trade requests");
        Console.WriteLine("5. Browse completed trades");
        Console.WriteLine("6. Logout");

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

static void RegisterUser(List<User> users)                     // Receives the list with all registered users
{
    Console.Clear();                                           // Clearing the screen to make the form clear
    Console.WriteLine("--- Register ---");                     // Title info

    Console.Write("New username: ");                           // Ask user to enter a username
    string newUsername = Console.ReadLine() ?? "";             // Read username (fallback to empty string if null)

    Console.Write("New password: ");                           // Ask user to enter a password
    string newPassword = Console.ReadLine() ?? "";             // Read password (fallback to empty string if null)

    // Basic validation, username and password must not be empty
    if (newUsername == "" || newPassword == "")                // Check for empty inputs
    {
        Console.WriteLine("Username and password are required."); // Inform user about missing fields
        Console.WriteLine("Press Enter to continue...");       // Wait so the user can read the message
        Console.ReadLine();                                    // Pause until Enter is pressed
        return;                                                // Stop the method without creating a user
    }

    // Check if the username already exists in the list
    bool exists = false;                                       // Flag to remember if we found a duplicate
    foreach (User user in users)                               // Loop through all existing users
    {
        if (user.Username == newUsername)                      // Exact string match (case-sensitive)
        {
            exists = true;                                     // Set the flag if a duplicate is found
            break;                                             // Stop the loop (no need to continue searching)
        }
    }

    if (exists)                                                // If the username is already taken
    {
        Console.WriteLine("Username already exists. Choose another."); // Inform the user
        Console.WriteLine("Press Enter to continue...");       // Wait so the user can read the message
        Console.ReadLine();                                    // Pause until Enter is pressed
        return;                                                // Do nothing more (no new user created)
    }

    // Create and save the new user (only if the username did not exist)
    User newUser = new User(newUsername, newPassword);         // Create a new User object with the provided data
    users.Add(newUser);                                        // Add the new user to the list (our in-memory "database")

    Console.WriteLine("Registration successful!");             // Confirmation message
    Console.WriteLine("Press Enter to continue...");           // Wait so the user can read the message
    Console.ReadLine();                                        // Pause until Enter is pressed
}


void AddItem()
{
    
}