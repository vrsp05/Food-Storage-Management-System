// The FoodItems class is a parent class that handles most of the food item's needs
public abstract class FoodItems
{
    // Attributes
    // This variable holds the food item's name
    private string _itemName;
    // This variable holds the type of the item (for example: meat or vegetable or rice or pasta)
    private string _itemType;
    // This variable holds the food item's expiration date
    private string _expirationDate;
    // This variable holds the food item's price
    private float _itemPrice;
    // This variable holds the quantity of items
    private int _itemQuantity;
    // This variable holds the food item's storage location (for example: freezer or dry storage)
    private string _storageLocation;
    // This variable holds the food item's unique code
    private string _itemUniqueCode;
    // This variable holds the date when the item was added
    private string _dateAdded;
    // This variable holds the food item's expiration status
    private bool _isExpired;
    // This is a list of the unique items to make sure items are not repeated
    protected List<string> _existingUniqueCodes = new List<string>();

    // Constructors
    public FoodItems()
    {

    } // End of constructor

    // Behaviors
    // This abstract method lets both freezed and dry objects create their unique code
    public abstract string UniqueCodeGenerator();

    // This method formats all the information of the item
    public string BuildFoodItem(string name, string type, string expiration, float price, int quantity, string location, string code)
    {   
        // This sets all the variables or attributes
        _itemName = name;
        _itemType = type;
        _expirationDate = expiration;
        _itemPrice = price;
        _itemQuantity = quantity;
        _storageLocation = location;
        _itemUniqueCode = code;
        _isExpired = false;

        // This assigns the date of today
        _dateAdded = DateTime.Now.ToString("MM/dd/yyyy");

        // This combines all values into one string
        string foodItem = $"{_itemUniqueCode},{_itemName},{_itemType},{_expirationDate},{_isExpired},{_itemPrice},{_itemQuantity},{_storageLocation},{_dateAdded}";

        // This returns the formatted value
        return foodItem;

    } // End of method BuildFoodItem

    // // This method helps in returning the list
    // public List<string> GetExistingCodes()
    // {   
    //     // This returns the list
    //     return _existingUniqueCodes;

    // } // End of the method GetExistingCodes

    // This method helps in adding the existing codes into the list from a file
    public void AddExistingUniqueCodes(string code)
    {   
        // Adds the code
        _existingUniqueCodes.Add(code);

    } // End of method AddExistingUniqueCodes

} // End of the parent class FoodItems