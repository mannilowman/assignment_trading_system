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

            case "3": 
                running = false; // Exit program

                break;

            default:
                Console.WriteLine("Invalid choice. Press Enter to continue...");
                Console.ReadLine();  // waiting for user to press Enter

                break;
        }
    }
    else
    Console.WriteLine("--- Welcome dear " + active_user.Username + " ---"); // Greeting with actual username
    Console.WriteLine();                                                    // Empty line for spacing
    Console.WriteLine("1. Upload item");                                    // Option 1
    Console.WriteLine("2. Browse trough items");                            // Option 2
    Console.WriteLine("3. Request a trade from another");                   // Option 3
    Console.WriteLine("4. See your sent trade requests");                   // Option 4 (only Pending sent by logged in user)
    Console.WriteLine("5. Accept or deny a request");                       // Option 5 (incoming requests to logged in user)
    Console.WriteLine("6. Browse trough completed trades");                 // Option 6 (Accepted/Denied/Completed)
    Console.WriteLine("7. See your sended requests");                       // Option 7 (all your sent, any status)
    Console.WriteLine("8. Logout");                                         // Option 8
    Console.Write("Choose an option: ");                                     //  input option 

    string userInput = Console.ReadLine() ?? "";                              // Read the user's choice (empty if null)

    switch (userInput)                                                         // Handle the selected option
    {
        case "1": //    
            nextItemId = AddItem(items, active_user, nextItemId);             // Create item and update counter via return value
            break;

       case "2":  
            ShowUsersAndItems(users, items);                                   // Print every user and items they own
            break;

        case "3": 
            nextTradeId = RequestTrade(items, trades, active_user, nextTradeId); // Create trade and update counter via return value
            break;

        case "4":  
            ShowMySentPendingTrades(trades, active_user);                      // Show outgoing trades that are still Pending
            break;

        case "5":  
            BrowseTradeRequests(trades, active_user);                          // Accept / Deny incoming Pending trades
            break;

        case "6":  
            BrowseCompletedTrades(trades, active_user);                        // Show non-Pending trades where you are involved
            break;

        case "7":  
            ShowAllMySentTrades(trades, active_user);                          // Show all outgoing trades (any status)
            break;

        case "8":  
            active_user = null;                                                // Clear the session and return to offline menu
        break;

        default: 
            Console.WriteLine("Invalid choice. Press Enter to continue...");   // Inform the user
            Console.ReadLine();                                                // Pause so the user can read
            break;
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

    
    if (newUsername == "" || newPassword == "")                // Check for empty inputs
    {
        Console.WriteLine("Username and password are required."); // Inform user about missing fields
        Console.WriteLine("Press Enter to continue...");       // Wait so the user can read the message
        Console.ReadLine();                                    // Pause until Enter is pressed
        return;                                                // Stop the method without creating a user
    }

    
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


    static int AddItem(List<Item> items, User active_user, int nextItemId) // Create item (id, owner, name, description)
    {
    Console.Clear();  // Clear screen
    Console.Write("--- Add item---"); // Item title

    Console.Write("Item name: "); // Ask for name
    String name = Console.ReadLine() ?? ""; // read name

    if (name == "") // validate name
    {
        Console.WriteLine("Invalid input. Please try again.");
        Console.WriteLine("Press Enter to continue.. ");
        Console.ReadLine();
        return nextItemId;  // no change
    }

    Console.Write("Description: "); //  ask for description 
    string description = Console.ReadLine() ?? ""; // read description 

    if (description == "") // validate description 
    {
        Console.WriteLine("Invalid input. Please try again. ");
        Console.WriteLine("Press Enter to continue.. ");
        Console.ReadLine();
        return nextItemId; // no change 
    }

    Item createdItem = new Item(nextItemId, active_user.Username, name, description); // create item, id, owner, name, description 

    items.Add(createdItem); // save item

    nextItemId = nextItemId + 1; // increase counter 

    Console.WriteLine("Item uploaded."); // confirmation 
    Console.ReadLine();

    return nextItemId; // return updated counter 

    }

    // Show every user and the items they own

    static void ShowUsersAndItems(List<User> users, List<Item> items)
    {
        Console.Clear();
        Console.WriteLine("--- Users and theiir items ---");
    
        foreach (User u in users)
    }
        Console.WriteLine("User: " + u.Username);

        bool userHasItems = false;
        foreach (Item it in items)
        {
            if (it.OwnerUsername == u.Username)
            {
            Console.WriteLine(" - Id; " + it.Id + " | Name: " + it.Name + " | Description: " + it.Description); // Item row
            userHasItems = true; // atleast one found
            }
        }

        if (!userHasItems) // if none
        {
        Console.WriteLine(" (no items)");
        }

        Console.WriteLine("------"); // making spacing/separating for beter design
    }
    
    Console.WriteLine("Press Enter to continue... ");
    Console.ReadLine();
}

// Create a trade request and return updated nextradeId
static int RequestTrade(List<Item> items, List<Trade> trades, User active_user, int nextTradeId)
{
    Console.Clear();  // clear screen 
    Console.WriteLine("--- Request a trade ---"); // Title

    bool foundAny = false; // track candidates
    foreach (Item i in items) // loop items
    {
        if (i.OwnerUsername != active_user.Username)
        {
            Console.WriteLine("Id: " + i.Id + " | Name: " + i.Name 0 " | Owner: " + i.OwnerUsername); // show avaible trade candidate
            foundAny = true;
        }
    }

    if (!foundAny)
    {
        Console.WriteLine("No items avaible to request. "); // Feedback
        Console.WriteLine("Press Enter to continue.. "); // Pause
        Console.ReadLine(); // Waiting for user to press Enter
        return nextTradeId; // Return unchanged counter
    }

    Console.Write("Enter the Item Id you want to request: "); // asking for an id 
    string input = Console.ReadLine() ?? "";
    int chosenId = 0;

    


}

