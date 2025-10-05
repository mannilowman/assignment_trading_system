namespace TradingApp;

// Trade = a swap request between two users
class Trade
{
    public int Id { get; set; } = 0;                 // Unique trade id
    public int ItemId { get; set; } = 0;             // The item involved
    public string FromUsername { get; set; } = "";   // Who sent the request
    public string ToUsername { get; set; } = "";     // Who receives the request (item owner)
    public TradingStatus Status { get; set; } = TradingStatus.Pending; // Current status

    public Trade(int id, int itemId, string fromUsername, string toUsername, TradingStatus status)
    {
        Id = id;                      // Save trade id
        ItemId = itemId;              // Save item id
        FromUsername = fromUsername;  // Save sender
        ToUsername = toUsername;      // Save receiver
        Status = status;              // Save status (enum)
    }
}

