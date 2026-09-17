using Microsoft.Extensions.Configuration;
using Azure.Messaging.ServiceBus;

namespace POC1Feedback.Logic.Services;

public class FeedbackService
{
    private readonly IConfiguration _configuration;

    public FeedbackService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendMessageToQueue(string message)
    {
        string connectionString = Environment.GetEnvironmentVariable("ServiceBusConnection")?? throw new InvalidOperationException("ServiceBusConnection not found.");

        string queueName = "notificationqueue";

        await using var client =
            new ServiceBusClient(connectionString);

        ServiceBusSender sender =
            client.CreateSender(queueName);

        await sender.SendMessageAsync(
            new ServiceBusMessage(message));
    }
}