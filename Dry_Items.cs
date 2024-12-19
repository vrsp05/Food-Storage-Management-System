// The child class DryItems will hold the attributes and methods for a dry stored food item
class DryItems : FoodItems
{
    // Attributes: NONE for now

    // Constructor
    public DryItems()
    {

    } // End of constructor

    // Behaviors
    // This abstract method helps in creating random codes for the items
    public override string UniqueCodeGenerator()
    {
        // This creates a new instance for random
        Random random = new Random();

        // This creates the variable for the code
        string dryCode;

        // Do while loop that generates a unique code
        do
        {
            // Generate a random 4-digit number
            int randomCode = random.Next(1000, 10000);

            // Build the code for dry stored food item
            dryCode = $"D-{randomCode}";
        }
        // Keep looping until we generate a unique code that doesn't exist in _existingUniqueCodes
        while (_existingUniqueCodes.Contains(dryCode));

        // This adds the code into the list
        _existingUniqueCodes.Add(dryCode);

        // This returns the code
        return dryCode;

    } // End of method UniqueCodeGenerator
    
} // End of the child class DryItems 