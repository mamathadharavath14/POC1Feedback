
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using POC1Feedback.Logic.Services;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

//builder.Services.AddApplicationInsightsTelemetryWorkerService();
//builder.Services.ConfigureFunctionsApplicationInsights();

builder.Services.AddSingleton<FeedbackService>();
builder.Services.AddSingleton<BlobStorageService>();
builder.Services.AddSingleton<FeedbackProcessService>();
builder.Services.AddSingleton<EmailNotificationService>();

builder.Build().Run();