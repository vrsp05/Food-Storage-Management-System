// The FoodStorageManager is the class that will handle most of the actions related to food
using System;
using System.Text.RegularExpressions;

class FoodStorageManager
{
    // Attributes
    // This list will hold the information of each item
    protected List<string> _allFoodItems = new List<string>();
    // This creates an instance of the DryItems class
    private DryItems _dryItems = new DryItems();
    // This creates an instance of the FreezerItems class
    private FreezerItems _freezerItems = new FreezerItems();

    // Constructors
    public FoodStorageManager()
    {

    } // End of constructor

    // Behaviors
    // This method helps with adding items to our list
    public void AddFoodItem(string foodItem)
    {   
        // This adds the food item
        _allFoodItems.Add(foodItem);

    } // End of AddFoodItem method

    // This method helps in deciding which type of code will be generated
    public string CodeTypeDecider(string location)
    {
        // If #1: if the location is dry
        if (location == "dry")
        {
            // This returns the generated code for the dry item
            return _dryItems.UniqueCodeGenerator();

        } // End of if #1

        // If #2: if he location is the freezer
        else if (location == "freezer")
        {
            // This returns the generated code for the dry item
            return _freezerItems.UniqueCodeGenerator();

        } // End of if #2

        // This handles un expected inputs
        throw new ArgumentException("Invalid location specified. Please use 'dry' or 'freezer'.");

    } // End of method CodeTypeDecider

    // This method displays the list with all the items
    public void DisplayAllItems()
    {   
        // Starts the list with number 1
        int listNumber = 1;

        // For loop that displays all items
        foreach (string item in _allFoodItems)
        {   
            // This breaks down the parts of each food item
            string[] foodParts = item.Split(",");

            // This assigns names to each value and changes value type when necessary
            string itemCode = foodParts[0];
            string itemName = foodParts[1];
            string itemType = foodParts[2];
            string itemExpirationDate = foodParts[3];
            string itemStatus = bool.Parse(foodParts[4]) ? "Expired" : "Good"; // Value type changed
            float itemPrice = float.Parse(foodParts[5]); // Value type changed
            int itemQuantity = int.Parse(foodParts[6]); // Value type changed
            string itemLocation = foodParts[7];
            string itemDateAdded = foodParts[8];

            // This displays the item
            Console.WriteLine(@$"
__________________________________________________
{listNumber}. Code: {itemCode} |Name: {itemName} |Status: {itemStatus}
Type: {itemType} |Expiration: {itemExpirationDate} |Price: {itemPrice}
Quantity: {itemQuantity} |Location: {itemLocation} |Date Added: {itemDateAdded}
__________________________________________________");

            // This adds one to the listNumber
            listNumber++;

        } // End of foreach loop

    } // End of method DisplayAllItems

    // This method helps removing an item based on the code
    public string RemoveFoodItem(string code)
    {   
        // This defines the pattern for valid codes, for example: "F-0000" or "D-0000".
        string codePattern = @"^[FD]-\d{4}$";

        // Loop until the code matches the expected format
        while (!Regex.IsMatch(code, codePattern))
        {
            // Inform the user if the code is invalid
            Console.WriteLine("Invalid code format. Please enter a valid code in the format 'F-0000' or 'D-0000'.");
            Console.Write("Enter a valid code: ");
            code = Console.ReadLine();

        } // End of while loop

        // Check if the storage list contains the item
        var itemToRemove =_allFoodItems.Find(item => item.Contains(code));;

        // do while loop that checks if the item does not exist
        while (itemToRemove == null)
        {   
            // This informs the user of the incorrect input, asks to try again, and reads the input
            Console.WriteLine($"No food item with code '{code}' was found.");
            Console.Write("Please enter a existing code: ");
            code = Console.ReadLine();

            // Check again if the storage list contains the item
            itemToRemove = _allFoodItems.Find(item => item.Contains(code));

        } // End of do while loop

        // Remove the item
        _allFoodItems.Remove(itemToRemove);

        // This returns the code for display
        return code;

    } // End of RemoveFoodItem method

    // This method helps in saving the information of the food items in a text file
    public void SaveFoodItems(string fileName)
    {
        // This starts writing the file
        using (StreamWriter writer = new StreamWriter(fileName))
        {   
            // This variable is used to enumerate the items
            int listNumber = 1;

            // This writes a header for the file
            writer.WriteLine("Here are your current saved items:");

            // For loop that displays all items
            foreach (string item in _allFoodItems)
            {   
                // This breaks down the parts of each food item
                string[] foodParts = item.Split(",");

                // This assigns names to each value
                string itemCode = foodParts[0];
                string itemName = foodParts[1];
                string itemType = foodParts[2];
                string itemExpirationDate = foodParts[3];
                string itemStatus = bool.Parse(foodParts[4]) ? "Expired" : "Good";
                string itemPrice = foodParts[5];
                string itemQuantity = foodParts[6];
                string itemLocation = foodParts[7];
                string itemDateAdded = foodParts[8];

                // This displays the item
                writer.WriteLine(@$"
    __________________________________________________
    {listNumber}. Code: {itemCode} |Name: {itemName} |Status: {itemStatus}
    Type: {itemType} |Expiration: {itemExpirationDate} |Price: {itemPrice}
    Quantity: {itemQuantity} |Location: {itemLocation} |Date Added: {itemDateAdded}
    __________________________________________________");

                // This adds one to the listNumber
                listNumber++;

            } // End of foreach loop

            // This is a end message
            writer.WriteLine();
            writer.WriteLine("End of the file.");

        } // End of using
  
        // This tells the user the file was saved
        Console.WriteLine();
        Console.WriteLine($"The file '{fileName}' was saved successfully.");
        Console.WriteLine();

    } // End of method SaveFoodItems

    // Method to load food items from a file
    public void LoadFoodItems(string filePath)
    {
        // If #1: Checks if the file exists
        if (!File.Exists(filePath))
        {   
            // Tells the user that the file with the saved data does not exists
            Console.WriteLine("There is no saved data.");

            // Returns back into the program
            return;

        } // End of if #1

        // Try #1: Tries to read a compliacated file
        try
        {   
            // Reads the contents of the specified file
            using (StreamReader reader = new StreamReader(filePath))
            {   
                // List and variable that helps reading the lines of the file and extract the needed values
                List<string> usefulLines = new List<string>();
                string line;

                // While loop that runs to skip unnecessary lines
                while ((line = reader.ReadLine()) != null)
                {
                    // WHILE If #1: Checks if the line contains unnecessary value to skip them
                    if (line.Contains("Here are your current saved items:") || line.Contains("End of the file") || line.Contains("__________________________________________________") || string.IsNullOrWhiteSpace(line))
                    {   
                        // This skips the line
                        continue;

                    } // End of WHILE If #1

                    // WHILE If #2: Checks the start of useful lines
                    if (line.Contains(". Code:")) // Start of a new food item
                    {   
                        // This clears the list's previous item's lines to save a new item
                        usefulLines.Clear();

                        // This adds the useful line with no extra spaces
                        usefulLines.Add(line.Trim());
                        
                        // For loop that reads the next 2 following useful lines
                        for (int i = 0; i < 2; i++)
                        {   
                            // Reads the line and saves it in a variable
                            string nextLine = reader.ReadLine();

                            // FOR LOOP if #1: Checks if the line is not an empty line to add it
                            if (!string.IsNullOrWhiteSpace(nextLine))
                            {   
                                // This adds the line
                                usefulLines.Add(nextLine.Trim());

                            } // End of FOR LOOP if #1

                        } // End of for loop

                        // Try #2: Tries to separate and clean the useful values from the useful lines
                        try
                        {
                            // These lines clean and extract the necessary values of the first useful line
                            string[] parts1 = usefulLines[0].Split('|'); // Separates by |
                            string code = parts1[0].Split(':')[1].Trim(); // Separates by : and cleans extra spaces
                            string name = parts1[1].Split(':')[1].Trim(); // Separates by : and cleans extra spaces
                            string stringStatus = parts1[2].Split(':')[1].Trim(); // Separates by : and cleans extra spaces

                            // This adds the code into a list for later confirmation
                            _dryItems.AddExistingUniqueCodes(code);

                            // Variable that helps converting back into bool values
                            bool status;

                            // TRY #2 if #1: Checks if the string value for status is good
                            if (stringStatus == "Good")
                            {   
                                // Sets the status to false signifiying that the item is not expired
                                status = false;

                            } // End of TRY #2 if #1

                            // Else that sets the status to expired
                            else
                            {   
                                // This sets the status to true, signifying that the item is expired
                                status = true;

                            } // End of else

                            // These lines clean and extract the necessary values of the second useful line
                            string[] parts2 = usefulLines[1].Split('|'); // Separates by |
                            string type = parts2[0].Split(':')[1].Trim(); // Separates by : and cleans extra spaces
                            string expiration = parts2[1].Split(':')[1].Trim(); // Separates by : and cleans extra spaces
                            float price = float.Parse(parts2[2].Split(':')[1].Trim()); // Separates by : and cleans extra spaces

                            // These lines clean and extract the necessary values of the thrid useful line
                            string[] parts3 = usefulLines[2].Split('|'); // Separates by |
                            int quantity = int.Parse(parts3[0].Split(':')[1].Trim()); // Separates by : and cleans extra spaces
                            string location = parts3[1].Split(':')[1].Trim(); // Separates by : and cleans extra spaces
                            string dateAdded = parts3[2].Split(':')[1].Trim(); // Separates by : and cleans extra spaces

                            // Combine all data into a single formatted string
                            string formattedItem = $"{code},{name},{type},{expiration},{status},{price},{quantity},{location},{dateAdded}";

                            // This adds the formatted info
                            _allFoodItems.Add(formattedItem);
                        
                        } // End of try #2

                        // Catch of try #2: Helps with debuggin
                        catch (Exception ex)
                        {   
                            // This informs which item was not processed correctly
                            Console.WriteLine($"Error processing item: {ex.Message}");

                        } // End of Catch of try #2

                    } // WHILE If #2

                } // End of  While loop
            
            } // End of Using

            // This helps by telling that the file was added with no issues (debuggin purposes)
            Console.WriteLine("Food data loaded successfully.");

        } // End of Try #1

        // Try #1 catch: Tells the user that the file was not loaded correctly
        catch (Exception ex)
        {   
            // These the user that the file was not loaded correctly
            Console.WriteLine($"An error occurred while loading the file: {ex.Message}");

        } // End of Try #1 catch

    } // End of method LoadFoodItems

    // This method helps in returning the list
    public List<string> GetAllFoodItems()
    {   
        // This returns the list
        return _allFoodItems;

    } // End of method GetAllFoodItems

} // End of FoodStorageManager classes