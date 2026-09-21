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
    private readonly BlobStorageService _blobstorageservice;
    public FeedbackProcessService(ILogger<FeedbackProcessService> logger, EmailNotificationService emailNotificationService , BlobStorageService blobStorageService)
    {
        _logger = logger;
        _emailNotificationService = emailNotificationService;
        _blobstorageservice = blobStorageService;

    }
    public async Task ProcessAsync(string message)
    {
        var feedback = JsonSerializer.Deserialize<Feedback>(message);
        var filename = $"{feedback.CustomerId}.json";
        var content =  JsonSerializer.Serialize(feedback);

        if(feedback == null)
        {
            return;
        }
        if (feedback.Rating == 3)
        {
            _logger.LogInformation("Rating is GOOD");
            _logger.LogInformation("Feedback sent to blob storage");
            await _blobstorageservice.UploadFeedbackAsync(filename, content);
        }
        else if(feedback.Rating == 4)
        {
            _logger.LogInformation("Rating is BETTER");
            _logger.LogInformation("Feedback sent to blob storage");
             await _blobstorageservice.UploadFeedbackAsync(filename, content);
        }
        else if(feedback.Rating == 5)
        {
            _logger.LogInformation("Rating is BESTT ");
            _logger.LogInformation("Feedback sent to blob storage");
             await _blobstorageservice.UploadFeedbackAsync(filename, content);
        }
        else if(feedback.Rating <= 2)
        {
            _logger.LogInformation("Negative feedback is received");

            await _emailNotificationService.SendEmailAsync(feedback.Email, "Negative Feedback", "We received your feedback and will contact to you" );

        }

        await Task.CompletedTask;
        
    }
}