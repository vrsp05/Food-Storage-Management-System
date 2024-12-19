using System;
using System.Globalization;
using System.Collections.Generic;

// The class InputValidator seeks to validate the inputs as much as possible
class InputValidator
{
    // Attributes: NONE for now

    // Constructors
    public InputValidator()
    {

    } // End of constructor

    // Behaviors
    // This method helps validating if the input is a non-empty string or numbers between 1 and 5 (only for the menu)
    public string ValidateMenuInput(string input)
    {
        // If #1: checks for blank or whitespace input
        if (string.IsNullOrWhiteSpace(input))
        {   
            // This clears the console
            Console. Clear();

            // This displays the error message
            Console.WriteLine();
            Console.WriteLine("Input cannot be blank. Please try again.");

            // This returns null to indicate a valid input
            return null;

        } // End of if #1

        // If #2: checks if the input is a number between 1 and 5
        if (!int.TryParse(input, out int number) || number < 1 || number > 5)
        {   
            // This clears the console
            Console.Clear();

            // This displays the error message
            Console.WriteLine();
            Console.WriteLine("Invalid input. Please enter a number between 1 and 5.");

            // This returns null to indicate a valid input
            return null;

        } // End of if #2

        // If the input passes both checks, return it
        return input;

    } // End of method ValidateMenuInput

    // This method helps in checking blank spaces
    public string ValidateNonBlankInput(string input)
    {
        // While loop that keeps prompting until a valid input is provided
        while (string.IsNullOrWhiteSpace(input))
        {
            // If the input is blank, show an error message, ask, and read input again
            Console.WriteLine("Input cannot be blank.");
            Console.Write("Please try again, and enter a valid input: ");
            input = Console.ReadLine();

        } // End of while

        // Return the valid input
        return input;

    } // End of method ValidateNonBlankInput

    // This checks if the date given is correct
    public string ValidateDate(string date, string format = "MM/dd/yyyy")
    {   
        // Starts the Datetime
        DateTime parsedDate;

        // This returns if tru or not
        while (!DateTime.TryParseExact(date, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
        {
            // If the input is blank, show an error message and prompt again
            Console.WriteLine("Input is incorrect, date should follow this format: MM/dd/yyyy.");
            Console.Write("Please try again, and enter a valid date: ");
            date = Console.ReadLine();

        } // End of while
        
        // This returns the date when correct
        return date;
    
    } // End of method ValidateDate

    // This method helps in validating if the input is a positive number
    public string ValidateNumberInput(string number)
    {      
        // This creates a float that will be used for testing
        float parsedNumber;

        // While loop that will run to test if it is a real number or a positive
        while (!float.TryParse(number, out parsedNumber) || parsedNumber < 0)
        {
            // If the input is incorrect, show an error message and prompt again
            Console.WriteLine("Input is incorrect. Please try again with a positive integer or float.");
            Console.Write("Enter a valid number: ");
            number = Console.ReadLine();

        } // End of while

        // This returns the number
        return number;

    } // End of method CheckIfPositiveNumber

    // This method helps in validating if the input is freezer or dry
    public string ValidateLocation(string location)
    {      
        // While loop that checks if the location is correct
        while (location != "freezer" && location != "dry")
        {
            // If the input is blank, show an error message, ask, and read input again
            Console.WriteLine("Input incorrect. Identify the correct storage location (freezer or dry).");
            Console.Write("Please try again, and enter a valid input: ");
            location = Console.ReadLine();

        } // End of while

        // This returns the proper value
        return location;

    } // End of method ValidateLocation

} // End of InputValidator class