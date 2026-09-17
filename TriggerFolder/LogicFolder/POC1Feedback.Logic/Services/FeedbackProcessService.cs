using System.Text.Json;
using Microsoft.Extensions.Logging;
using POC1Feedback.Logic.Models;
using Microsoft.Extensions.Configuration;
using Azure.Messaging.ServiceBus;


namespace POC1Feedback.Logic.Services;
public class FeedbackProcessService
{
    private readonly ILogger<FeedbackProcessService> _logger;
    private readonly EmailNotificationService _emailNotificationService;
    public FeedbackProcessService(ILogger<FeedbackProcessService> logger, EmailNotificationService emailNotificationService)
    {
        _logger = logger;
        _emailNotificationService = emailNotificationService;

    }
    public async Task ProcessAsync(string message)
    {
        var feedback = JsonSerializer.Deserialize<Feedback>(message);

        if(feedback == null)
        {
            return;
        }
        if (feedback.Rating == 3)
        {
            _logger.LogInformation("Rating is GOOD");
        }
        else if(feedback.Rating == 4)
        {
            _logger.LogInformation("Rating is BETTER");
        }
        else if(feedback.Rating == 5)
        {
            _logger.LogInformation("Rating is BESTT");
        }
        else if(feedback.Rating <= 2)
        {
            _logger.LogInformation("Negative feedback is received");

            await _emailNotificationService.SendEmailAsync(feedback.Email, "Negative Feedback", "We received your feedback and will contact to you" );

        }

        await Task.CompletedTask;
        
    }
}