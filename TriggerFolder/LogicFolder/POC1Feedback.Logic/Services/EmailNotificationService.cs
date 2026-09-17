using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

public class EmailNotificationService
{
    private readonly ILogger<EmailNotificationService> _logger;
    private readonly IConfiguration _configuration;

    public EmailNotificationService(
        ILogger<EmailNotificationService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public async Task SendEmailAsync(
        string email,
        string subject,
        string body)
    {
        var message = new MimeMessage();

        string User = _configuration["SmtpUser"] ?? throw new InvalidOperationException("Missing SmtpUser");

        message.From.Add(MailboxAddress.Parse(User));

        message.To.Add(MailboxAddress.Parse(email));

        message.Subject = subject;

        message.Body = new TextPart("plain")
        {
            Text = body
        };

        using var smtp = new SmtpClient();

        string host = _configuration["SmtpHost"] ?? throw new InvalidOperationException("Missing SmtpHost");

        int port = int.Parse(_configuration["SmtpPort"] ?? throw new InvalidOperationException("Missing SmtpPort"));

        await smtp.ConnectAsync(host, port, SecureSocketOptions.StartTls);

        string Password = _configuration["SmtpPassword"] ?? throw new InvalidOperationException("Missing SmtpHost");

        await smtp.AuthenticateAsync(User, Password);

        await smtp.SendAsync(message);

        await smtp.DisconnectAsync(true);

        _logger.LogInformation($"Email sent successfully to {email}");
    }
}