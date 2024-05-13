using Azure;
using Azure.Communication.Email;


Console.WriteLine("---");
Console.WriteLine("Azure Communication Services - Advanced Send Email");
Console.WriteLine();


string? connectionString = Environment.GetEnvironmentVariable("COMMUNICATION_SERVICES_CONNECTION_STRING");

if (string.IsNullOrEmpty(connectionString))
{
    System.Console.WriteLine("Please set the environment variable: COMMUNICATION_SERVICES_CONNECTION_STRING");
    return;
}

EmailClient emailClient = new EmailClient(connectionString);

var subject = "Hello from Azure Communication Services";
var htmlContent = "<html><body><h1>Advanced send email test</h1><br /><h4>This email message is sent from Azure Communication Service Email.</h4></body></html>";
var sender = "donotreply@hiroyay-lab.net";

var emailContent = new EmailContent(subject)
{
    Html = htmlContent
};

Console.Write("Enter the recipient email address: ");
var recipients = Console.ReadLine();

var toRecipients = new List<EmailAddress>();

if (!string.IsNullOrEmpty(recipients))
{
    foreach (var recipient in recipients.Split(','))
    {
        toRecipients.Add(new EmailAddress(recipient.Trim()));
    }
}
else
{
    Console.WriteLine("Recipient email address is required.");
    return;
}

EmailRecipients emailRecipients = new EmailRecipients(toRecipients);

Console.Write("Enter the cc email address: ");
var ccRecipients = Console.ReadLine();

if (!string.IsNullOrEmpty(ccRecipients))
{
    foreach (var recipient in ccRecipients.Split(','))
    {
        emailRecipients.CC.Add(new EmailAddress(recipient.Trim()));
    }
}

Console.Write("Enter the bcc email address: ");
var bccRecipients = Console.ReadLine();

if (!string.IsNullOrEmpty(bccRecipients))
{
    foreach (var recipient in bccRecipients.Split(','))
    {
        emailRecipients.BCC.Add(new EmailAddress(recipient.Trim()));
    }
}

var emailMessage = new EmailMessage(sender, emailRecipients, emailContent);

Console.WriteLine();
Console.WriteLine("Sending email...");

try
{
    EmailSendOperation sendOperation = emailClient.Send(WaitUntil.Completed, emailMessage);

    Console.WriteLine();
    Console.WriteLine($"Email Sent. Status = {sendOperation.Value.Status}");

    string operationId = sendOperation.Id;
    Console.WriteLine($"Operation ID: {operationId}");
}
catch (RequestFailedException ex)
{
    Console.WriteLine($"Email send operation failed with error code: {ex.ErrorCode}, message {ex.Message}");
}