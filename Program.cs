using System;                                     
using System.Collections;                         
using System.Collections.Generic;                 
using TradingApp;                                 // my classes: User, Item, Trade, TradingStatus

// --- LISTS/MEMORY OFF USERS/ITEMS/TRADES
List<User> users = new List<User>();              // all users in memory
List<Item> items = new List<Item>();              // all items in memory
List<Trade> trades = new List<Trade>();           // all trades in memory

// demo users to test quickly
users.Add(new User("Manni", "1"));
users.Add(new User("Test", "1"));

int nextItemId = 1;                               // simple counter for item ids
int nextTradeId = 1;                              // simple counter for trade ids

bool running = true;                              // controls the main loop
User? active_user = null;                         // null means nobody is logged in

// ---- MAIN LOOP ----
while (running)                                   // loop until running = false
{
    Console.Clear();                              // clear screen each iteration

    if (active_user == null)                      // if no one is logged in → offline menu
    {
        // --- OFFLINE MENU ---
        Console.WriteLine("\n--- Welcome to my TradingApp ---"); // title
        Console.WriteLine("1) Register");         // create account
        Console.WriteLine("2) Login");            // log in
        Console.WriteLine("3) Exit");             // quit
        Console.Write("Choose an option: ");      // ask for input

        string? choice = Console.ReadLine();      // read choice
        switch (choice)                           // decide action
        {
            case "1":                             // register
                RegisterUser(users);
                break;

            case "2":                             // login
            {
                Console.Clear();
                Console.Write("Write username: ");                // ask username
                string username = Console.ReadLine() ?? "";       // read username
                Console.Write("Write password: ");                // ask password
                string password = Console.ReadLine() ?? "";       // read password

                active_user = null;                               // reset before search
                foreach (User u in users)                         // loop all users
                {
                    if (u.TryLogin(username, password))           // simple match
                    {
                        active_user = u;                          // logged in
                        break;                                    // stop loop
                    }
                }

                if (active_user == null)                          // wrong login
                {
                    Console.WriteLine("Login failed. Press Enter to continue");
                    Console.ReadLine();                           // pause
                    continue;                                     // go back to top
                }
                break;                                            // go to online menu
            }

            case "3":                             // exit app
                running = false;                  // stop loop
                break;

            default:                              // invalid input
                Console.WriteLine("Invalid choice. Press Enter to continue...");
                Console.ReadLine();               // pause
                break;
        }
    }
    else                  // someone is logged in -> online menu
    {
        // --- ONLINE MENU ---
        Console.WriteLine("--- Welcome dear " + active_user.Username + " ---"); // greet with name
        Console.WriteLine();                                                    // spacing
        Console.WriteLine("1. Upload item");                                    // add item
        Console.WriteLine("2. Browse trough items");                            // see all users + their items
        Console.WriteLine("3. Request a trade from another");                   // send trade request
        Console.WriteLine("4. See your sent trade requests");                   // my outgoing pending
        Console.WriteLine("5. Accept or deny a request");                       // incoming pending to me
        Console.WriteLine("6. Browse trough completed trades");                 // done/denied/accepted
        Console.WriteLine("7. See your sended requests");                       // all my sent (any status)
        Console.WriteLine("8. Logout");                                         // logout
        Console.Write("Choose an option: ");                                     // ask for input

        string userInput = Console.ReadLine() ?? "";                            // read input

        switch (userInput)                                                       // act on choice
        {
            case "1":                                                           // upload item
                nextItemId = AddItem(items, active_user, nextItemId);           // create item and increase counter
                break;

            case "2":                                                           // browse items
                ShowUsersAndItems(users, items);                                 // print users + items
                break;

            case "3":                                                           // request trade
                nextTradeId = RequestTrade(items, trades, active_user, nextTradeId); // create trade and increase counter
                break;

            case "4":                                                           // my sent pending
                ShowMySentPendingTrades(trades, active_user);                    // list pending I sent
                break;

            case "5":                                                           // incoming pending to me
                BrowseTradeRequests(trades, items, active_user);                 // accept/deny and on accept transfer owner
                break;

            case "6":                                                           // completed trades
                BrowseCompletedTrades(trades, active_user);                      // list done/denied/accepted involving me
                break;

            case "7":                                                           // all my sent (any status)
                ShowAllMySentTrades(trades, active_user);                        // list all I sent
                break;

            case "8":                                                           // logout
                active_user = null;                                              // clear session
                break;

            default:                                                            // invalid input
                Console.WriteLine("Invalid choice. Press Enter to continue...");
                Console.ReadLine();                                              // pause
                break;
        }
    }
}



//        prints an item 
static void printitem(Item it)
{
    Console.WriteLine("Id: " + it.Id);
    Console.WriteLine("Name: " + it.Name);
    Console.WriteLine("Description: " + it.Description);
    Console.WriteLine("Owner: " + it.OwnerUsername);
    Console.WriteLine("------");
}

//     prints a trade 
static void printtrade(Trade t)
{
    Console.WriteLine("TradeId: " + t.Id);
    Console.WriteLine("ItemId: " + t.ItemId);
    Console.WriteLine("From: " + t.FromUsername);
    Console.WriteLine("To: " + t.ToUsername);
    Console.WriteLine("Status: " + t.Status);
    Console.WriteLine("------");
}

  // ---- Methods ----

//       register user 
static void RegisterUser(List<User> users)
{
    Console.Clear();
    Console.WriteLine("--- Register ---");

    Console.Write("New username: ");                           // ask username
    string newUsername = Console.ReadLine() ?? "";             // read username

    Console.Write("New password: ");                           // ask password
    string newPassword = Console.ReadLine() ?? "";             // read password

    if (newUsername == "" || newPassword == "")                // both are required
    {
        Console.WriteLine("Username and password are required.");
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
        return;
    }

    bool exists = false;                                       // check duplicates
    foreach (User u in users)
    {
        if (u.Username == newUsername)                         // simple exact match
        {
            exists = true;
            break;
        }
    }

    if (exists)                                                // username already used
    {
        Console.WriteLine("Username already exists. Choose another.");
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
        return;
    }

    users.Add(new User(newUsername, newPassword));             // save new user
    Console.WriteLine("Registration successful!");
    Console.WriteLine("Press Enter to continue...");
    Console.ReadLine();
}

// add an item for the logged-in user
static int AddItem(List<Item> items, User active_user, int nextItemId)
{
    Console.Clear();
    Console.WriteLine("--- Add item ---");

    Console.Write("Item name: ");                              // ask name
    string name = Console.ReadLine() ?? "";                    // read

    if (name == "")                                            // must not be empty
    {
        Console.WriteLine("Invalid input. Please try again.");
        Console.WriteLine("Press Enter to continue.. ");
        Console.ReadLine();
        return nextItemId;                                     // no change
    }

    Console.Write("Description: ");                            // ask description
    string description = Console.ReadLine() ?? "";             // read

    if (description == "")                                     // must not be empty
    {
        Console.WriteLine("Invalid input. Please try again. ");
        Console.WriteLine("Press Enter to continue.. ");
        Console.ReadLine();
        return nextItemId;                                     // no change
    }

    // create and store item
    Item createdItem = new Item(nextItemId, active_user.Username, name, description);
    items.Add(createdItem);
    nextItemId = nextItemId + 1;                               // increase id counter

    Console.WriteLine("Item uploaded.");
    Console.WriteLine("Press Enter to continue...");
    Console.ReadLine();

    return nextItemId;                                         // send back updated counter
}

// show all users and the items they own
static void ShowUsersAndItems(List<User> users, List<Item> items)
{
    Console.Clear();
    Console.WriteLine("--- Users and their items ---");

    foreach (User u in users)                                  // loop users
    {
        Console.WriteLine("User: " + u.Username);              // print username

        bool hasAny = false;                                   // track if this user has items
        foreach (Item it in items)                             // loop items
        {
            if (it.OwnerUsername == u.Username)                // ownership check
            {
                printitem(it);                                 // print item
                hasAny = true;
            }
        }

        if (!hasAny)                                           // if user has no items
        {
            Console.WriteLine("  (no items)");
            Console.WriteLine("------");
        }
    }

    Console.WriteLine("Press Enter to continue... ");
    Console.ReadLine();
}

//      send a trade request 
static int RequestTrade(List<Item> items, List<Trade> trades, User active_user, int nextTradeId)
{
    Console.Clear();
    Console.WriteLine("--- Request a trade ---");

    bool foundAny = false;                                     // check if any candidate exists
    foreach (Item i in items)
    {
        if (i.OwnerUsername != active_user.Username)           // only items from other users
        {
            printitem(i);
            foundAny = true;
        }
    }

    if (!foundAny)                                             // nothing to request
    {
        Console.WriteLine("No items available to request.");
        Console.WriteLine("Press Enter to continue.. ");
        Console.ReadLine();
        return nextTradeId;                                    // unchanged
    }

    Console.Write("Enter the Item Id you want to request: ");  // ask for id
    string input = Console.ReadLine() ?? "";                   // read input

    int chosenId = 0;                                          // number we build
    bool validNumber = true;                                   // only digits allowed

    if (input == "")                                           // empty not allowed
    {
        validNumber = false;
    }
    else
    {
        foreach (char ch in input)                             // check each char
        {
            if (ch < '0' || ch > '9')                          // not a digit → invalid
            {
                validNumber = false;
                break;
            }
            int digit = ch - '0';                              // char -> int digit
            chosenId = (chosenId * 10) + digit;                // build number
        }
    }

    if (!validNumber || chosenId <= 0)                         // validate number
    {
        Console.WriteLine("Invalid item id.");
        Console.WriteLine("Press Enter to continue.. ");
        Console.ReadLine();
        return nextTradeId;                                    // unchanged
    }

    Item? target = null;                                       // find the item by id
    foreach (Item i in items)
    {
        if (i.Id == chosenId)
        {
            target = i;
            break;
        }
    }

    if (target == null)                                        // not found
    {
        Console.WriteLine("Item not found.");
        Console.WriteLine("Press Enter to continue.. ");
        Console.ReadLine();
        return nextTradeId;                                    // unchanged
    }

    if (target.OwnerUsername == active_user.Username)          // cannot request own item
    {
        Console.WriteLine("You cannot request your own item.");
        Console.WriteLine("Press Enter to continue.. ");
        Console.ReadLine();
        return nextTradeId;                                    // unchanged
    }

    // create and store trade
    Trade t = new Trade(nextTradeId, target.Id, active_user.Username, target.OwnerUsername, TradingStatus.Pending);
    trades.Add(t);
    nextTradeId = nextTradeId + 1;                             // increase trade id counter

    Console.WriteLine("Trade request sent (Id: " + t.Id + ").");
    Console.WriteLine("Press Enter to continue.. ");
    Console.ReadLine();

    return nextTradeId;                                        // return updated counter
}

// show my sent trades that are still pending
static void ShowMySentPendingTrades(List<Trade> trades, User active_user)
{
    Console.Clear();
    Console.WriteLine("--- Your sent trade requests (Pending) ---");

    bool anyPrinted = false;                                   // track prints
    foreach (Trade t in trades)
    {
        bool fromMe = false;                                   
        if (t.FromUsername == active_user.Username)
        {
            fromMe = true;
        }

        bool isPending = false;
        if (t.Status == TradingStatus.Pending)
        {
            isPending = true;
        }

        if (fromMe && isPending)                               // both conditions true
        {
            printtrade(t);
            anyPrinted = true;
        }
    }

    if (!anyPrinted)
    {
        Console.WriteLine("You have no pending sent trade requests.");
    }

    Console.WriteLine("Press Enter to continue...");
    Console.ReadLine();
}

// handle incoming pending trades to me (accept/deny)
// when I accept, I transfer the item's owner to the requester
static void BrowseTradeRequests(List<Trade> trades, List<Item> items, User active_user)
{
    Console.Clear();
    Console.WriteLine("--- Incoming trade requests ---");

    bool anyIncoming = false;                                  // track if any pending to me
    foreach (Trade t in trades)
    {
        bool toMe = false;
        if (t.ToUsername == active_user.Username)
        {
            toMe = true;
        }

        bool isPending = false;
        if (t.Status == TradingStatus.Pending)
        {
            isPending = true;
        }

        if (toMe && isPending)
        {
            printtrade(t);
            anyIncoming = true;
        }
    }

    if (!anyIncoming)                                          // nothing to handle
    {
        Console.WriteLine("No pending incoming requests.");
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
        return;
    }

    Console.Write("Enter Trade Id to answer (or empty to exit): "); // select one
    string input = Console.ReadLine() ?? "";
    if (string.IsNullOrWhiteSpace(input))                      // empty → exit
    {
        return;
    }

    int chosenTradeId = 0;                                     // manual number parsing
    bool validNumber = true;

    foreach (char ch in input)
    {
        if (ch < '0' || ch > '9')                              // not a digit
        {
            validNumber = false;
            break;
        }
        int digit = ch - '0';
        chosenTradeId = (chosenTradeId * 10) + digit;
    }

    if (!validNumber || chosenTradeId <= 0)
    {
        Console.WriteLine("Invalid trade id.");
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
        return;
    }

    Trade? target = null;                                      // find that trade
    foreach (Trade tr in trades)
    {
        bool sameId = false;
        if (tr.Id == chosenTradeId) { sameId = true; }

        bool toMe = false;
        if (tr.ToUsername == active_user.Username) { toMe = true; }

        bool isPending = false;
        if (tr.Status == TradingStatus.Pending) { isPending = true; }

        if (sameId && toMe && isPending)
        {
            target = tr;
            break;
        }
    }

    if (target == null)
    {
        Console.WriteLine("Trade not found.");
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
        return;
    }

    Console.WriteLine("1) Accept");
    Console.WriteLine("2) Deny");
    Console.Write("Choose: ");
    string answer = Console.ReadLine() ?? "";

    if (answer == "1")                                         // accept
    {
        Item? tradedItem = null;                               // find the item by id
        foreach (Item it in items)
        {
            if (it.Id == target.ItemId)
            {
                tradedItem = it;
                break;
            }
        }

        if (tradedItem == null)
        {
            Console.WriteLine("Item not found anymore. Trade cannot be completed.");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            return;
        }

        bool iAmOwner = false;                                 // ensure I still own it
        if (tradedItem.OwnerUsername == active_user.Username)
        {
            iAmOwner = true;
        }

        if (!iAmOwner)
        {
            Console.WriteLine("You are not the owner of this item anymore. Trade cannot be completed.");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            return;
        }

        tradedItem.OwnerUsername = target.FromUsername;        // new owner
        target.Status = TradingStatus.Completed;               // mark as completed

        Console.WriteLine("Trade accepted and item transferred.");
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }
    else if (answer == "2")                                    // deny
    {
        target.Status = TradingStatus.Denied;                  // mark denied
        Console.WriteLine("Trade denied.");
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }
    else                                                       // invalid
    {
        Console.WriteLine("Invalid choice.");
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }
}

// show trades involving me that are not pending
static void BrowseCompletedTrades(List<Trade> trades, User active_user)
{
    Console.Clear();
    Console.WriteLine("--- Completed trades ---");

    bool anyPrinted = false;                                   // track prints
    foreach (Trade t in trades)
    {
        
        bool iAmInvolved = false;
        if (t.FromUsername == active_user.Username)
        {
            iAmInvolved = true;
        }
        else
        {
            if (t.ToUsername == active_user.Username)
            {
                iAmInvolved = true;
            }
        }

        bool isCompletedLike = false;
        if (t.Status == TradingStatus.Accepted)
        {
            isCompletedLike = true;
        }
        else
        {
            if (t.Status == TradingStatus.Denied)
            {
                isCompletedLike = true;
            }
            else
            {
                if (t.Status == TradingStatus.Completed)
                {
                    isCompletedLike = true;
                }
            }
        }

        if (iAmInvolved && isCompletedLike)
        {
            printtrade(t);
            anyPrinted = true;
        }
    }

    if (!anyPrinted)
    {
        Console.WriteLine("No completed trades.");
    }

    Console.WriteLine("Press Enter to continue...");
    Console.ReadLine();
}

// show all my sent trades (any status)
static void ShowAllMySentTrades(List<Trade> trades, User active_user)
{
    Console.Clear();
    Console.WriteLine("--- All your sent trade requests ---");

    bool anyPrinted = false;                                   // track prints
    foreach (Trade t in trades)
    {
        bool fromMe = false;
        if (t.FromUsername == active_user.Username)
        {
            fromMe = true;
        }

        if (fromMe)
        {
            printtrade(t);
            anyPrinted = true;
        }
    }

    if (!anyPrinted)
    {
        Console.WriteLine("You have not sent any trade requests.");
    }

    Console.WriteLine("Press Enter to continue...");
    Console.ReadLine();
}
