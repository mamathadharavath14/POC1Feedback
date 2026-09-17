using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using POC1Feedback.Logic.Services;
using POC1Feedback.Logic.Models;

namespace Company.Function;

public class SubmitFeedback
{
    private readonly ILogger<SubmitFeedback> _logger;
    private readonly FeedbackService _feedbackService;

    public SubmitFeedback(ILogger<SubmitFeedback> logger, FeedbackService feedbackService)
    {
        _logger = logger;
        _feedbackService = feedbackService;
    }
    

    [Function("SubmitFeedback")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
    {
        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var feedback = JsonSerializer.Deserialize<Feedback>(requestBody);

        if (feedback == null)
        {
            return new BadRequestObjectResult("Invalid Request");
        }

        await _feedbackService.SendMessageToQueue(requestBody);

        return new OkObjectResult("Feedback Sent to servicebus queue");
    }
    }
