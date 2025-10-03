namespace TradingApp;

class Item
{
    public int Id { get; set; } = 0;  // unique ID for each item

    public string OwnerUsername { get; set; } = ""; // Owners username

    public string Name { get; set; } = ""; // name of the item ex "Audi SQ7"
 
    public string Description { get; set; } = ""; // Description of the item ex "Red" 

    public Item(int id, string ownerUsername, string name, string description)
    {
        Id = id; // save id 

        OwnerUsername = ownerUsername; // save owner

        Name = name; // save name of item

        Description = description; // save description 
    }

}