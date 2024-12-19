// The class ExpirationChecker will contain the attributes and methods needed to check the expiration status of an item
using System.Globalization;

class ExpirationChecker
{   
    // Attributes
    // This variable holds the expired items for later use
    //protected List<string> _expiredItems = new List<string>();
    // This variable holds the soon to be expired items for later use
    protected List<string> _expiringItems = new List<string>();

    // Constructors
    public ExpirationChecker()
    {

    } // End of constructor

    // Behaviors
    // This method helps with checking the expiration status of each item 
    public void CheckExpirationStatus(List<string> itemList)
    {   
        // Assigns the current date to be used for comparison
        DateTime dateToday =  DateTime.Now;

        // For loop that checks all items in the list
        for (int i = 0; i < itemList.Count(); i++)
        {
            // This assigns the food item into a variable
            string foodItem = itemList[i];

            // This separates the values
            string[] itemParts = foodItem.Split(",");

            // This assigns a name to the value
            string itemExpirationDate = itemParts[3];
            string itemName = itemParts[1];

            // This variable helps in extracting the expiration date
            DateTime expirationDate;

            // If #1: helps checking the expiration status of an item based on its expiration month
            if (DateTime.TryParseExact(itemExpirationDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out expirationDate))
            {

                // This calculates the difference in days
                int dayDifference = (expirationDate - dateToday).Days;

                // If #1.1: Item is expired
                if (dayDifference < 0)
                {   
                    // This sets the value to expired
                    itemParts[4] = "true";

                    // This combines the items
                    itemList[i] = string.Join(",", itemParts);

                    // This adds the expired item
                    //_expiredItems.Add(itemList[i]);

                    // This prints the notification
                    Console.WriteLine($"The item '{itemName}' is expired.");

                } // End of if #1.1

                // If #1.2: Item is soon to expire (within one week)
                else if (dayDifference <= 7)
                {   
                    // If #1.2.1: Checks if the item was already added
                    if (_expiringItems.Contains(itemList[i]))
                    {
                        // If already added it will skip
                        continue;

                    } // End of if #1.2.1

                    // Else #1.2.1: if the items is not added
                    else
                    {
                        // This joins back the items information
                        itemList[i] = string.Join(",", itemParts);

                        // This adds the item into the list
                        _expiringItems.Add(itemList[i]);

                        // This displays the notification
                        Console.WriteLine($"The item '{itemName}' is soon to expire (in {dayDifference} days).");

                    } // End of else #1.2.1

                } // End of if #1.2

            } // End of if #1

            // Else for if #1
            else
            {
                // Displays a error message
                Console.WriteLine($"Error parsing expiration date for item '{itemName}'.");

            } // End of else

        } // End of foor loop

    } // End of method CheckExpirationStatus

    // This method returns the values of the list
    public List<string> GetAllExpiringItems()
    {   
        // This returns the list
        return _expiringItems;

    } // End of method GetAllExpiredItems

} // End of the ExpirationChecker class