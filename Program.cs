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
        EmailService emailService = EmailService.LoadFromFile("email_config.txt");

        // This clears the console
        Console.Clear();

        // Starting system message
        Console.WriteLine();
        Console.WriteLine("Checking for email service... ");
        
        // Quick loading animation
        ui.LoadingAnimation();

        // This checks if the email service was loaded successfully
        // If the email service is null, it means it could not be loaded
        if (emailService == null)
        {
            // This tells the user that the email service could not be loaded
            Console.WriteLine(@"
Error: Could not load email service. Please contact support.

The program will now exit.");

            // Quick loading animation
            ui.LoadingAnimation();

            // This terminates the program
            return;

        } // End of if

        // This clears the console
        Console.Clear();

        // This runs the login flow and returns (username, email)
        accountManager.ShowLoginMenu();
        string LoggedInUsername = accountManager._loggedInUserName;

        // This sets the logged in user in the UserInterface class
        ui.SetLoggedInAccount(accountManager);

        // Later we'll load data: $"{username}_foodDATA.txt"
        Console.WriteLine($"\nLogin successful. Welcome, {LoggedInUsername}!");

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
        string foodFile = $"{LoggedInUsername}_foodDATA.txt";

        // Check if the food data file exists
        if (File.Exists(foodFile))
        {   
            // This clears the console
            Console.Clear();

            // Confirm the file found
            Console.WriteLine($"Found food data file: {foodFile}");

            // This will load the information from the file
            ui.LoadingSequence(foodFile);

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
            ui.MenuActions(menuInput, emailService);

        } while (menuInput == null || menuInput != "5"); // End of do while loop
        
    } // End of main function

} // End of main program class