using System.Net;
using System.Net.Mail;


Console.WriteLine("---");
Console.WriteLine("Azure Communication Services - Advanced Send Email");
Console.WriteLine();

string? authUser = Environment.GetEnvironmentVariable("SMTP_USER");
string? authPassword = Environment.GetEnvironmentVariable("SMTP_SECRET");

if (string.IsNullOrEmpty(authUser) || string.IsNullOrEmpty(authPassword))
{
    Console.WriteLine("Please set the environment variables: SMTP_USER and SMTP_SECRET");
    return;
}

// Enter sender email address
string sender = "sender email address";

Console.Write("Enter the recipient email address: ");
string? recipient = Console.ReadLine();

if (string.IsNullOrEmpty(recipient))
{
    Console.WriteLine("Recipient email address is required.");
    return;
}

string subject = "Hello from Azure Communication Services";
string body = "This email message is sent from Azure Communication Service Email using SMTP.";


string smtpServer = "smtp.azurecomm.net";

var client = new SmtpClient(smtpServer)
{
    Port = 587,
    Credentials = new NetworkCredential(authUser, authPassword),
    EnableSsl = true,
};

var message = new MailMessage(sender, recipient, subject, body);

try
{
    client.Send(message);
    Console.WriteLine("The email was successfully sent using Smtp.");
}
catch (Exception ex)
{
    Console.WriteLine($"Smtp send failed with the exception: {ex.Message}");
}