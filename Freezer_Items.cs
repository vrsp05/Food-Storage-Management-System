// The child class FreezerItems will hold the attributes and methods for a freezer stored food item
class FreezerItems : FoodItems
{
    // Attributes: NONE for now

    // Constructor
    public FreezerItems()
    {

    } // End of constructor

    // Behaviors
    // This abstract method helps in creating random codes for the freezer stored items
    public override string UniqueCodeGenerator()
    {
        // This creates a new instance for random
        Random random = new Random();

        // This creates the variable for the code
        string freezerCode;

        // Do while loop that generates a unique code
        do
        {
            // Generate a random 4-digit number
            int randomCode = random.Next(1000, 10000);

            // Build the code for freezer items
            freezerCode = $"F-{randomCode}";
        }
        // Keep looping until a unique code that doesn't exist in _existingUniqueCodes is generated
        while (_existingUniqueCodes.Contains(freezerCode));
        
        // This adds the code
        _existingUniqueCodes.Add(freezerCode);

        // This returns the code
        return freezerCode;

    } // End of method UniqueCodeGenerator
    
} // End of the child class FreezerItems