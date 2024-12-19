// This class will handle the most of the actions performed by the user in the menu
using System.ComponentModel.Design;
using System.Net;
using System.Reflection.Metadata.Ecma335;

class UserInterface
{
    // Attributes
    // This variable helps in checking if the info was already loaded
    private bool _isLoaded;
    // This creates an instance for the FoodStorageManager class
    private FoodStorageManager _fsms = new FoodStorageManager();
    // This creates an instances for the DryItems and FreezerItems classes from the FoodItem class
    private FoodItems _FoodItems = new DryItems();
    // This creates an instance for the CheckExpiration class
    private ExpirationChecker _statusChecker = new ExpirationChecker();
    // This creates an instance for the EmailService class
    private EmailService _emailService = new EmailService("vrsppalilo@gmail.com", "babz jdgk yihw ejxx");
    // this creates an instance for the InputValidator class
    private InputValidator _validator = new InputValidator();

    // Constructors
    public UserInterface()
    {

    } // End of constructor

    // Behaviors
    
    // This method displays the menu to the user
    public void DisplayMenu()
    {   
        // This prints the menu
        Console.Write(@"
Welcome to the Food Storage Management System (FSMS)

MAIN MENU
Here are your FSMS options:
 1- Add a food item
 2- Remove a food item
 3- View my items
 4- Save my list
 5- Quit program
Please select from the menu (1-5): ");

    } // End of DisplayMenu method

    // This method will handle the user input for the menu
    public string MenuInput()
    {
        // This reads the user input
        string menuInput = Console.ReadLine();

        // This returns the input
        return menuInput;
        
    } // End of method MenuInput

    // This method will handle the decisions of the user
    public void MenuActions(string input)
    {   
        // If #1: If the user selects to add an item
        if (input == "1")
        {   
            // This creates the variables that will be used
            string name;
            string type;
            string expirationDate;
            float price;
            int quantity;
            string location;

            // This starts the questionnaire to help create the new item
            Console.WriteLine();
            Console.WriteLine("Please answer the following questions:");

            // Bool varialbe that will assist in validating all inputs
            bool isValid = false;

            // While loop that runs until all questions have been answered properly
            while (isValid == false)
            {   
                // Try to help catching other errors
                try
                {
                    // This is the questionnaire to build the new item, conversion is done in the respective lines

                    // This asks the name of the item, reads and assigns the input into a variable
                    Console.Write("What is the name of the item? ");
                    name = _validator.ValidateNonBlankInput(MenuInput()); // Input validation happens here

                    // This asks the type of the item, reads and assigns the input into a variable
                    Console.Write("What is the type of the item (meat, vegetable, bread, rice, etc.)? ");
                    type = _validator.ValidateNonBlankInput(MenuInput()); // Input validation happens here
                    
                    // This asks the expiration date of the item, reads and assigns the input into a variable
                    Console.Write("What is the expiration date of the item (MM/dd/yyyy)? ");
                    expirationDate = _validator.ValidateDate(MenuInput()); // Input validation happens here

                    // This asks the price of the item, reads and assigns the input into a variable
                    Console.Write("What is the price of the item (decimal, whole number)? ");
                    price = float.Parse(_validator.ValidateNumberInput(MenuInput())); // Conversion from string to float and Input validation happens here

                    // This asks the quantity of items added, reads and assigns the input into a variable
                    Console.Write("How many items are you adding to storage (integer)? ");
                    quantity = int.Parse(_validator.ValidateNumberInput(MenuInput())); // Conversion from string to int and Input validation happens here

                    // This asks the storage location of the item, reads and assigns the input into a variable
                    Console.Write("What is the storage location for the item ('freezer' or 'dry')? ");
                    location = _validator.ValidateNonBlankInput(MenuInput()); // Input validation happens here
                    location = _validator.ValidateLocation(location); // Input validation happens here

                    // This helps in deciding which type of code is needed and generates the code
                    string code = _fsms.CodeTypeDecider(location);

                    // This builds the item's information
                    string foodItem = _FoodItems.BuildFoodItem(name, type, expirationDate, price, quantity, location, code);

                    // This is a adding message
                    Console.WriteLine("Adding... ");

                    // Quick animation to close this adding session
                    LoadingAnimation();

                    // This adds the item into the list
                    _fsms.AddFoodItem(foodItem);

                    // This tells the user the item was added
                    Console.WriteLine();
                    Console.WriteLine($"The food item '{name}' was added successfully.");
                    Console.WriteLine();

                    // This is a adding message
                    Console.WriteLine("Closing... ");

                    // Quick animation to close this adding session
                    LoadingAnimation();

                    // Marking isValid as true to exit the loop
                    isValid = true;

                    // This clears the console
                    Console.Clear();

                } // End of try

                // Catch that handles other exceptions
                catch (Exception ex)
                {
                    // If there is an error, print the message and restart the questionnaire
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Please answer the questions again.");

                } // End of catch

            } // End of while

        } // End of if #1

        // If #2: If the user selects to delete an item
        else if (input == "2")
        {
            // This asks the user what is the code of the food item and reads the input
            Console.WriteLine();
            Console.Write("What is the code of the food item? ");
            string code = _validator.ValidateNonBlankInput(MenuInput()); // Test the input

            // This calls the remove item method
            code = _fsms.RemoveFoodItem(code);

            // Simple animation and message that item is being removed
            Console.WriteLine("Removing...");
            LoadingAnimation();

            // This tells the user that the item was removed correctly
            Console.WriteLine();
            Console.WriteLine($"Food item with code '{code}' has been removed.");
            Console.WriteLine();

            // This is a clossing message
            Console.WriteLine("Closing... ");

            // Quick animation to close the removing session
            LoadingAnimation();

            // This clears the console
            Console.Clear();

        } // End of if #2

        // If #3: if the user wanrs to view all the items
        else if (input == "3")
        {   
            // This is the loading message
            Console.WriteLine();
            Console.WriteLine("Displaying items...");

            // Displays the quick animation
            LoadingAnimation();

            // This clears the console
            Console.Clear();

            // This presents the items
            Console.WriteLine();
            Console.WriteLine("These are your current stored items:");

            // This displays all the items
            _fsms.DisplayAllItems();

        } // End of if #3

        // If #4: if the user wants to save the list
        else if (input == "4")
        {   
            // This asks the user the name of the file and reads the input
            Console.WriteLine();
            Console.Write("What is the name of the file? ");
            string fileName = _validator.ValidateNonBlankInput(MenuInput());

            // This is a adding message
            Console.WriteLine("Saving... ");

            // Displays the quick animation
            LoadingAnimation();

            // This calls the method that saves the file
            _fsms.SaveFoodItems(fileName);
           
            // This is a adding message
            Console.WriteLine("Closing... ");

            // Quick animation to close the saving session
            LoadingAnimation();

            // This clears the console
            Console.Clear();

        } // End of if #4

        // If #5: if the user wants to quit the program
        else if (input == "5")
        {   
            // This gives a nice goodbye message
            Console.WriteLine(@"
Goodbye, and have a nice day!
");
        // This tells the user that program is closing
        Console.WriteLine("Quiting program...");

        // Displays the quick animation
        LoadingAnimation();

        // This builds the body of the email
        string emailBody = _emailService.GenerateEmailBody(_statusChecker.GetAllExpiringItems());

        // This sends an email with the expired items
        _emailService.SendEmail("Expiring Items Notification", emailBody);

        // This terminates the program
        //Environment.Exit(0);

        } // End of if #5

    } // End of MenuActions

    // This is the loading animation method
    public void LoadingAnimation()
    {
        // Time delay in milliseconds between each step of the animation
        int delay = 250;

        // Repeat the sequence a few times
        for (int i = 0; i < 2; i++)
        {
            // Step 1: Show one asterisk
            Console.Write("*");
            System.Threading.Thread.Sleep(delay);

            // Step 2: Show two asterisks
            Console.Write("*");
            System.Threading.Thread.Sleep(delay);

            // Step 3: Show three asterisks
            Console.Write("*");
            System.Threading.Thread.Sleep(delay);

            // Step 4: Show four asterisks
            Console.Write("*");
            System.Threading.Thread.Sleep(delay);

            // Step 5: Show five asterisks
            Console.Write("*");
            System.Threading.Thread.Sleep(delay);

            // Step 4: Remove asterisks one by one
            Console.Write("\b \b"); // Remove the last asterisk
            System.Threading.Thread.Sleep(delay);

            Console.Write("\b \b"); // Remove the fourth asterisk
            System.Threading.Thread.Sleep(delay);

            Console.Write("\b \b"); // Remove the third asterisk
            System.Threading.Thread.Sleep(delay);

            Console.Write("\b \b"); // Remove the second asterisk
            System.Threading.Thread.Sleep(delay);

            Console.Write("\b \b"); // Remove the first asterisk
            System.Threading.Thread.Sleep(delay);

        } // End of for loop

    } // End of LoadingAnimation method

    // This method will help in loading the information into the file
    public void LoadingSequence(string fileName)
    {      
        // If# 1: Checks if the file was already loaded
        if (_isLoaded == false)
        {   
            // This calls the method that loads all date
            _fsms.LoadFoodItems(fileName);

            // Marks the file as loaded to avoid loading repeatedly
            _isLoaded = true;

        } // End of if #1

        // If #2: Checks and tells the the user that the data was loadedn
        else if (_isLoaded == true)
        {   
            // Debuggin message that helps marking that the file was uploaded
            Console.WriteLine("File is already loaded");

        } // End of if #2

    } // End of method LoadingSequence

    // This method helps in checking the values once
    public void CheckingSequence()
    {   
        // This calls the checking method
        _statusChecker.CheckExpirationStatus(_fsms.GetAllFoodItems());

    } // End of method CheckingSequence

} // End of class UserInterface