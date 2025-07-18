using System;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;

// The EmailService class seeks to keep all the attributes and methods needed for the emails that will be sent to the user
public class EmailService
{
    // Attributes
    // This variable will hold the email of the sender
    private string _senderEmail;
    // This variable will hold the password of the email account
    private string _senderPassword;
    // This will hold the type of the email server
    private string _smtpServer;
    // This holds the port of the email server
    private int _smtpPort;

    // Constructors
    public EmailService(string senderEmail, string senderPassword, string smtpServer = "smtp.gmail.com", int smtpPort = 587)
    {
        // This  assigns the values needed to our email service class
        _senderEmail = senderEmail;
        _senderPassword = senderPassword;
        _smtpServer = smtpServer;
        _smtpPort = smtpPort;

    } // End of constructor

    // Behaviors
    // This method helps in sending the email
    public void SendEmail(string subject, string body, string recipientEmail)
    {
        // Trying to create the client, message, and send it
        try
        {
            // This creates a new smtp instance to help access the smtp server and account
            SmtpClient emailAccount = new SmtpClient(_smtpServer)
            {
                // These assing the smpt values
                Port = _smtpPort, // smtp server port
                Credentials = new NetworkCredential(_senderEmail, _senderPassword), // Establishes the credentials of the sender
                EnableSsl = true // This starts the ssl

            }; // End of smtpClient instance

            // This creates message instace to help building the email
            MailMessage emailMessage = new MailMessage
            {
                // These assing the message values
                From = new MailAddress(_senderEmail), // sets the sender address
                Subject = subject, // this sets the email subject
                Body = body // this sets the email body

            }; // End of message instace

            // This adds the email as a recipient
            emailMessage.To.Add(recipientEmail);

            // This sends the email
            emailAccount.Send(emailMessage);

            // This confirms that the email was sent successfully
            Console.WriteLine("Email sent successfully.");

        } // End of try

        // Catch in case something goes wrong
        catch (Exception ex)
        {
            // This tells the user that sending the email failed
            Console.WriteLine($"Failed to send email: {ex.Message}");

        } // End of catch

    } // End of method SendEmail

    // This method helps in generating the body for the email just including the expired items
    public string GenerateEmailBody(List<string> expiringItems, string recipientUsername)
    {
        // If #1: checks if there are any existing expired items
        if (expiringItems == null || expiringItems.Count == 0)
        {
            // This message is sent if there are not expiring items
            string noExpiringItemsEmailBody = @$"Dear {recipientUsername},

There are no items nearing expiration.

Sincerely,

The Food Storage Management System App (FSMS)";

            // This tells the user that there are not expired items
            return noExpiringItemsEmailBody;

        } // End of if# 1

        // This is the first line of the email body
        string expiringItemsEmailBody = @$"Dear {recipientUsername},

The following items are nearing expiration:";

        // Starts the list with number 1
        int listNumber = 1;

        foreach (string item in expiringItems)
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

            // This adds the information of the expiring item in the body of the email
            expiringItemsEmailBody += @$"
__________________________________________________
{listNumber}. Code: {itemCode} |Name: {itemName} |Status: {itemStatus}
Type: {itemType} |Expiration: {itemExpirationDate} |Price: {itemPrice}
Quantity: {itemQuantity} |Location: {itemLocation} |Date Added: {itemDateAdded}
__________________________________________________
";

            // This adds one to the listNumber
            listNumber++; ;

        } // End of foreach

        // This adds the end of the email
        expiringItemsEmailBody += @"
Please take the actions needed.

Sincerely,

The Food Storage Management System App (FSMS)";

        // This returns the built body of the message
        return expiringItemsEmailBody;

    } // End of method GenerateEmailBody

    public static EmailService LoadFromFile(string filePath)
    {
        try
        {
            // Read all lines from the file
            string[] lines = File.ReadAllLines(filePath);

            // Assumes first line is email, second line is password
            string senderEmail = lines[0].Trim();
            string senderPassword = lines[1].Trim();

            // Return an EmailService object using your current constructor
            return new EmailService(senderEmail, senderPassword);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load email credentials: {ex.Message}");
            return null;
        }
    }



} // End of the EmailService class