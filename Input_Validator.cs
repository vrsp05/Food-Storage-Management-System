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
            Console.Clear();

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

    // This method helps in validating if the input is a valid email address
    public bool IsValidEmail(string email)
    {
        // Try to check if the email is valid
        try
        {
            // This checks if the email is in a valid format
            var addr = new System.Net.Mail.MailAddress(email);

            // If the email is valid, return true
            return addr.Address == email;

        } // End of try

        // If an exception occurs, return false
        catch
        {
            // This returns false if the email is not valid
            Console.WriteLine("Invalid email format. Please enter a valid email address.");
            return false;

        } // End of catch

    } // End of method IsValidEmail

    // This method helps in validating the password based on specific rules
    // It checks for length, character types, and confirms the password
    public string GetValidPassword(string username, string email)
    {
        // While loop that will keep asking for a valid password
        while (true)
        {
            // Prompt the user to create a password
            Console.Write(@"
Your password must:
- Be at least 8 characters long
- Include at least one uppercase letter
- Include at least one lowercase letter
- Include at least one digit
- Include at least one special character (e.g. !, @, #, %, $, etc.)
- Not contain spaces
- Not contain your username or email
- Cannot include double quotes ("") or single quotes (')

Create your password: ");
            string password = Console.ReadLine();

            // If user enters '0', return to main menu
            if (password == "0")
            {
                // Clear the console
                Console.Clear();
                // Return to main menu
                return "0";

            } // End of if

            // Rule 1: Check minimum length
            if (password.Length < 8)
            {
                // If the password is too short, display an error message
                Console.WriteLine("Password must be at least 8 characters long.");
                continue;
            }

            // Rule 2: Check for at least one uppercase letter
            if (!password.Any(char.IsUpper))
            {
                // If the password does not contain an uppercase letter, display an error message
                Console.WriteLine("Password must contain at least one uppercase letter.");
                continue;
            }

            // Rule 3: Check for at least one lowercase
            if (!password.Any(char.IsLower))
            {
                Console.WriteLine("Password must contain at least one lowercase letter.");
                continue;
            }

            // Rule 4: Check for at least one digit
            if (!password.Any(char.IsDigit))
            {
                Console.WriteLine("Password must contain at least one digit.");
                continue;
            }

            // Rule 5: Check for at least one special character
            if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                Console.WriteLine("Password must contain at least one special character.");
                continue;
            }

            // Rule 6: Check for spaces
            if (password.Contains(" "))
            {
                Console.WriteLine("Password cannot contain spaces.");
                continue;
            }

            // Rule 7: Cannot contain username or email
            if (password.Contains(username, StringComparison.OrdinalIgnoreCase) ||
                password.Contains(email, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Password cannot contain your username or email.");
                continue;
            }

            // Rule 8: Cannot contain quotes
            if (password.Contains("\"") || password.Contains("'"))
            {
                Console.WriteLine("Password cannot contain double quotes (\") or single quotes (').");
                continue;
            }

            // Rule 9: If the password is blank, prompt to enter a valid one
            if (string.IsNullOrWhiteSpace(password))
            {
                // If it is blank, prompt to enter a valid one
                Console.WriteLine("Password cannot be blank.");

            } // End of else if

            // Rule 10: Confirm password
            Console.Write("Confirm your password: ");
            string confirmPassword = Console.ReadLine();

            // If user enters '0', return to main menu
            if (confirmPassword == "0")
            {
                // Clear the console
                Console.Clear();
                // Return to main menu
                return "0";

            } // End of if

            // If passwords do not match, display an error message
            if (password != confirmPassword)
            {
                // Error message for mismatched passwords
                Console.WriteLine("Passwords do not match.");
                continue;
            }

            // All checks passed
            return password;

        } // End of while loop

    } // End of method GetValidPassword

} // End of InputValidator class