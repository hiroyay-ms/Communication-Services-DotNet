using Azure;
using Azure.Communication.Email;


Console.WriteLine("---");
Console.WriteLine("Azure Communication Services - Send Email");
Console.WriteLine();


string? connectionString = Environment.GetEnvironmentVariable("COMMUNICATION_SERVICES_CONNECTION_STRING");

if (string.IsNullOrEmpty(connectionString))
{
    System.Console.WriteLine("Please set the environment variable: COMMUNICATION_SERVICES_CONNECTION_STRING");
    return;
}

EmailClient emailClient = new EmailClient(connectionString);

var subject = "Hello from Azure Communication Services";
var htmlContent = "<html><body><h1>Send email test</h1><br /><h4>This email message is sent from Azure Communication Service Email.</h4></body></html>";

// Enter sender email address
var sender = "sender email address";

Console.Write("Enter the recipient email address: ");
var recipient = Console.ReadLine();

Console.WriteLine();
Console.WriteLine("Sending email...");

try
{
    EmailSendOperation sendOperation = await emailClient.SendAsync(
        WaitUntil.Completed,
        sender,
        recipient,
        subject,
        htmlContent
    );

    Console.WriteLine();
    Console.WriteLine($"Email Sent. Status = {sendOperation.Value.Status}");

    string operationId = sendOperation.Id;
    Console.WriteLine($"Operation ID: {operationId}");
}
catch (RequestFailedException ex)
{
    Console.WriteLine($"Email send operation failed with error code: {ex.ErrorCode}, message {ex.Message}");
}
