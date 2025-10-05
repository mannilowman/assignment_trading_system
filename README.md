                                       TRADING APP


This is my little console project where you can make an account, log in, add items, and try to trade them with other users. Each item has an id, a name, a description, and an owner. If you send a trade request and the other person accepts, the item switches owner, and you can see it directly when you browse the items list.

The whole thing is built on just a few classes: User, Item, and Trade. I also made a small enum called TradingStatus to keep track if a trade is Pending, Accepted, Denied, or Completed. All the data just lives in memory in simple lists, so nothing is saved when you quit, but that also keeps it really simple and clear.

I didn’t use inheritance here because it didn’t really make sense like, a trade isn’t a user or an item. So I just connected them with composition a trade has an item id and usernames. For loops and searches I kept it basic with foreach and if, no LINQ, since I wanted the logic to be super clear to follow.

Basically, it’s a small but working trading system where I tried to keep things simple and understandable while still showing the core ideas of OOP.

I know the assignment also mentions save and load, like writing everything to files and then restoring it when you start again. That could have been done with the File.WriteAllText and File.ReadAllLines, but honestly i chose to not add it here because i wanted to focus on my logic around trades, users and items.
And i really didnt feel like adding more fuel to this fire atm.
Hope you enjoy my little project.


How to use it

1. Start the program - You will see the first menu (Offline-Menu)
2. Register a new user or log in with one of the demo users (Manni/Test)
3. Once logged in you can : Uppload an item & give it a description, 
  browse items and see what other users own, 
  send trade requests for items that aren't yours,
  check your pending trades or respond to incoming requests,
  accepting a trade will move the item to its new owner.
4. You can also see all completed trades, or log out and switch user.

Nothing is saved to files or databases, everything just exists while the program is running. I kept it simple just for texting and learning.