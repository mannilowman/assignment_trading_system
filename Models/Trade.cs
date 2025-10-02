
using System.ComponentModel.Design;
using System.Data.Common;
using System.Runtime.CompilerServices;

namespace TradingApp;

// 
class Trade
{
    public int Id { get; set; } = 0; // unique trade id

    public int ItemId { get; set; } = 0; // unique item id 

    public string FromUsername { get; set; } = ""; // The requesting trade user

    public string ToUsername { get; set; } = ""; // the user that's recieving the request (owner of the item)

    public string Status { get; set; } = "pending";  // Trade status is goes in order "Pending", "Accepted or Denied", Completed

    public Trade(int id, int itemId, string fromUsername, string toUsername, string status)
    {
        Id = id;   // saving TradeId

        ItemId = itemId;  // saving Item id attached to the specific trade

        FromUsername = fromUsername;  // saving requesting trade user

        ToUsername = toUsername; // saving reciever 

        Status = status; // saving trade status 
    }
}

