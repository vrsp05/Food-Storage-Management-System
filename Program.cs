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

        // Starting system message
        Console.WriteLine();
        Console.WriteLine("Loading data... ");

        // Quick loading animation
        ui.LoadingAnimation();

        // This clears the console
        Console.Clear();

        // This will load the information from the file
        ui.LoadingSequence("food.txt");

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