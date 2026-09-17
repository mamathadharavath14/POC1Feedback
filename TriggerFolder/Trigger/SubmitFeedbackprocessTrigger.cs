using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using POC1Feedback.Logic.Services;
using POC1Feedback.Logic.Models;

namespace Company.Function;

public class SubmitFeedbackprocessTrigger
{
    private readonly ILogger<SubmitFeedbackprocessTrigger> _logger;
    private readonly FeedbackProcessService _feedbackProcessService;

    public SubmitFeedbackprocessTrigger(ILogger<SubmitFeedbackprocessTrigger> logger, FeedbackProcessService feedbackProcessService)
    {
        _logger = logger;
        _feedbackProcessService = feedbackProcessService;
    }
    

    [Function("SubmitFeedbackprocess")]
    public async Task Run([ServiceBusTrigger("notificationqueue", Connection ="ServiceBusConnection")] string message)
    {
      
       _logger.LogInformation("Service bus messages procceesed and given rating");
        await _feedbackProcessService.ProcessAsync(message);
    }
    }
