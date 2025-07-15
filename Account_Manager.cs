using System;
using System.IO;
using System.Collections.Generic;

public class AccountManager
{
    // File path for storing account data
    private const string _accountFile = "accounts.txt";

    // Properties to store the current logged-in user
    public string _loggedInUserName { get; private set; }
    public string _loggedInEmail { get; private set; }

    // Instance of UserInterface for displaying animations
    UserInterface _accountUI = new UserInterface();

    // Create a new account and append it to the file
    public void CreateAccount()
    {
        // Clear the console
        Console.Clear();

        // Display account creation prompt
        Console.WriteLine("Create a New Account");
        Console.WriteLine("(Enter 0 at any time to return to the main menu)");

        // Load all existing accounts into a list for duplicate checking
        string[] lines = File.Exists(_accountFile) ? File.ReadAllLines(_accountFile) : new string[0];
        HashSet<string> existingEmails = new HashSet<string>();
        HashSet<string> existingUsernames = new HashSet<string>();
        HashSet<string> existingPasswords = new HashSet<string>();


        string tempUsername = "", tempEmail = "", tempPassword = "";

        // Extract all emails and usernames
        foreach (string line in lines)
        {
            // Check if the line contains username
            if (line.StartsWith("Username:"))
            {
                // Add username to the set
                existingUsernames.Add(line.Split(": ")[1].Trim());

            } // End of if

            // Check if the line contains email
            else if (line.StartsWith("Email:"))
            {
                // Add email to the set
                existingEmails.Add(line.Split(": ")[1].Trim());

            } // End of else if

            // Check if the line contains password
            else if (line.StartsWith("Password:"))
            {
                // Add password to the set
                existingPasswords.Add(line.Split(": ")[1].Trim());

            } // End of else if

        } // End of foreach

        // While loop that asks for username
        while (true)
        {
            // Ask for username
            Console.Write("\nEnter a username: ");
            // Read input
            tempUsername = Console.ReadLine();

            // If user enters '0', return to main menu
            if (tempUsername == "0")
            {
                // Clear the console
                Console.Clear();
                // Return to main menu
                return;

            } // End of if


            // Check if the username already exists or is blank
            if (existingUsernames.Contains(tempUsername))
            {
                // If it exists, prompt to try a different one
                Console.WriteLine("That username is already taken. Try a different one.");

            } // End of if

            // If username is blank, prompt to enter a valid one
            else if (string.IsNullOrWhiteSpace(tempUsername))
            {
                // If it is blank, prompt to enter a valid one
                Console.WriteLine("Username cannot be blank.");

            } // End of else if

            // If username is valid, break the loop
            else
            {
                // If it is valid, break the loop
                break;

            } // End of else

        } // End of while loop

        // while loop that asks for email
        while (true)
        {
            // Ask for email
            Console.Write("Enter your email: ");
            // Read input
            tempEmail = Console.ReadLine();

            // If user enters '0', return to main menu
            if (tempEmail == "0")
            {
                // Clear the console
                Console.Clear();
                // Return to main menu
                return;

            } // End of if

            // Check if the email already exists
            if (existingEmails.Contains(tempEmail))
            {
                // If it exists, prompt to try a different one
                Console.WriteLine("That email is already registered. Try a different one.");

            } // End of if

            // iF the email is blank, prompt to enter a valid one
            else if (string.IsNullOrWhiteSpace(tempEmail))
            {
                // If it is blank, prompt to enter a valid one
                Console.WriteLine("Email cannot be blank.");

            } // End of else if

            // If email is valid, break the loop
            else
            {
                // If it is valid, break the loop
                break;

            } // End of else

        } // End of while loop


        // while loop that asks for password
        while (true)
        {
            // Ask for password
            Console.Write("Create a password: ");
            // Read input
            tempPassword = Console.ReadLine();

            // If user enters '0', return to main menu
            if (tempPassword == "0")
            {
                // Clear the console
                Console.Clear();
                // Return to main menu
                return;

            } // End of if

            // Check if the password already exists
            if (existingPasswords.Contains(tempPassword))
            {
                // If it exists, prompt to try a different one
                Console.WriteLine("That password is already used. Try a different one.");

            } // End of if

            // If the password is blank, prompt to enter a valid one
            else if (string.IsNullOrWhiteSpace(tempPassword))
            {
                // If it is blank, prompt to enter a valid one
                Console.WriteLine("Password cannot be blank.");

            } // End of else if

            // If password is valid, break the loop
            else
            {
                // If it is valid, break the loop
                break;

            } // End of else

        } // End of while loop

        // Build the account data string
        string accountData = @$"
Account Data:
Username: {tempUsername}
Email: {tempEmail}
Password: {tempPassword}
---";

        // Append the account data to the file
        File.AppendAllText(_accountFile, accountData + Environment.NewLine);

        // Account creating prompt
        Console.WriteLine("\nCreating a new account...");

        // Show loading animation
        _accountUI.LoadingAnimation();

        // Clear the console
        Console.Clear();

        // Prompt for successful account creation
        Console.WriteLine("\nAccount created successfully! Please log in to continue.");

        // Show loading animation
        _accountUI.LoadingAnimation();

        // Clear the console
        Console.Clear();

    } // End of method CreateAccount 


    // Show the login menu and return user data if successful
    public (string Username, string Email) ShowLoginMenu()
    {
        // While loop for handling the login menu
        while (true)
        {
            // Clear the console
            Console.Clear();

            // Display the login menu
            Console.Write(@"Welcome to the Food Storage Management System!

Please select an option:
1 - Log In
2 - Create New Account
3 - Exit
Please select from the menu (1-3): ");

            // Read user input
            string choice = Console.ReadLine();

            // If user enters '1', start the login process
            if (choice == "1")
            {
                // Call the Login method and check if it returns a valid result
                var loginResult = Login();

                // Clear the console
                Console.Clear();

                // If login is successful, return the user data
                if (loginResult != null) return loginResult.Value; // Login successful, return user data

            } // End of if

            // If user enters '2', create a new account
            else if (choice == "2")
            {
                // Call the CreateAccount method
                CreateAccount();

            } // End of else if

            // If user enters '3', exit the program
            else if (choice == "3")
            {
                // This says a goodbye message
                Console.WriteLine("\nThank you for using the Food Storage Management System!");

                // This tells the user that program is closing
                Console.WriteLine("\nQuiting program...");

                // Displays the quick animation
                _accountUI.LoadingAnimation();

                //clear the console
                Console.Clear();

                // Exits the program
                Environment.Exit(0);

            } // End of else if

            // If user enters anything else, display an error message
            else
            {
                // Displays an error message
                Console.WriteLine("Invalid selection. Press Enter to try again.");
                Console.ReadLine();

            } // End of else

        } // End of while loop

    } // End of method ShowLoginMenu


    // Log in by checking all accounts
    public (string Username, string Email)? Login()
    {
        // Clear the console
        Console.Clear();

        // Display the login prompt
        Console.WriteLine("Log In to Your Account");
        Console.WriteLine("(Enter 0 at any time to return to the main menu)");
        
        // Ask for email and password
        Console.Write("\nEmail: ");
        // Read input
        string inputEmail = Console.ReadLine();

        // If user enters '0', return to main menu
        if (inputEmail == "0")
        {
            // Clear the console
            Console.Clear();
            // Return to main menu
            return null;

        } // End of if

        while (string.IsNullOrWhiteSpace(inputEmail))
        {
            // If email is blank, prompt to enter a valid one
            Console.Write("Email cannot be blank. Please enter your email:");
            inputEmail = Console.ReadLine();

            // If user enters '0', return to main menu
            if (inputEmail == "0")
            {
                // Clear the console
                Console.Clear();
                // Return to main menu
                return null;

            } // End of if

        } // End of while loop

        // Ask for password
        Console.Write("Password: ");
        // Read input
        string inputPassword = Console.ReadLine();

        // If user enters '0', return to main menu
        if (inputPassword == "0")
        {
            // Clear the console
            Console.Clear();
            // Return to main menu
            return null;

        } // End of if

        while (string.IsNullOrWhiteSpace(inputPassword))
        {
            // If password is blank, prompt to enter a valid one
            Console.Write("Password cannot be blank. Please enter your password:");
            inputPassword = Console.ReadLine();

            // If user enters '0', return to main menu
            if (inputPassword == "0")
            {
                // Clear the console
                Console.Clear();
                // Return to main menu
                return null;

            } // End of if

        } // End of while loop

        // Read all lines from the account file
        string[] lines = File.ReadAllLines(_accountFile);

        // Variables to store username, email, and password
        string username = "", email = "", password = "";

        // Loop through each line to find the matching account
        for (int i = 0; i < lines.Length; i++)
        {
            // Check if the line starts with "Username:", "Email:", or "Password:"
            if (lines[i].StartsWith("Username:")) username = lines[i].Split(": ")[1];

            else if (lines[i].StartsWith("Email:")) email = lines[i].Split(": ")[1];

            else if (lines[i].StartsWith("Password:")) password = lines[i].Split(": ")[1];

            // If we reach the end of an account block (indicated by "---"), check credentials
            if (lines[i] == "---")
            {
                // Check if the input email and password match the stored ones
                if (inputEmail == email && inputPassword == password)
                {   
                    // Set the logged-in user properties
                    _loggedInUserName = username;
                    _loggedInEmail = email;

                    // Set the logged-in user properties
                    return (username, email); // Successful login

                } // End of if

                // Reset for next block
                username = email = password = "";

            } // End of if

        } // End of for loop

        // If no matching account was found, display an error message
        Console.WriteLine("\nInvalid email or password. Press Enter to return.");
        Console.ReadLine();

        // Return null to indicate login failure
        return null;

    } // End of method Login
    
} // End of class AccountManager