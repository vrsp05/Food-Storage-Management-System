using System;

// Main program class
class Program
{   
    // Main function of program
    static void Main(string[] args)
    {   
        // Creating variables
        string menuInput;

        // Creating instances of the classes
        UserInterface ui = new UserInterface();
        InputValidator validator = new InputValidator();
        AccountManager accountManager = new AccountManager();

        // This clears the console
        Console.Clear();

        // Starting system message
        Console.WriteLine();
        Console.WriteLine("Checking for existing account... ");

        // Quick loading animation
        ui.LoadingAnimation();

        // This clears the console
        Console.Clear();

        // This runs the login flow and returns (username, email)
        var userInfo = accountManager.ShowLoginMenu();
        string username = userInfo.Username;
        string email = userInfo.Email;

        // Later we'll load data: $"{username}_foodDATA.txt"
        Console.WriteLine($"\nLogin successful. Welcome, {username}!");

        // Quick loading animation
        ui.LoadingAnimation();

        // This clears the console
        Console.Clear();

        // Starting system message
        Console.WriteLine();
        Console.WriteLine("Loading data... ");

        // Quick loading animation
        ui.LoadingAnimation();

        // String for the food data file
        string foodFile = $"{username}_foodDATA.txt";

        // Check if the food data file exists
        if (File.Exists(foodFile))
        {   
            // This clears the console
            Console.Clear();

            // Confirm the file found
            Console.WriteLine($"Found food data file: {foodFile}");

            // Load the file later here


            // This will load the information from the file
            ui.LoadingSequence("food.txt");

            // Quick loading animation
            ui.LoadingAnimation();

        } // End of if

        // Else if the food data file does not exist
        else
        {   
            // This clears the console
            Console.Clear();
            
            // Confirm the file not found
            Console.WriteLine($"No food data file found. We'll create one when you add items.");

            // Quick loading animation
            ui.LoadingAnimation();

        } // End of else


        // This clears the console
        Console.Clear();

        // Do while loop for handling the menu
        do
        {   
            // This constantly update the status of the items
            ui.CheckingSequence();

            // Call the DisplayMenu method to show the menu
            ui.DisplayMenu();

            // Reads the user input for the menu and assigns the value
            menuInput = ui.MenuInput();

            // This helps validating the input
            menuInput = validator.ValidateMenuInput(menuInput);

            // This calls the MenuAction method to perform the menu actions
            ui.MenuActions(menuInput);

        } while (menuInput == null || menuInput != "5"); // End of do while loop
        
    } // End of main function

} // End of main program class