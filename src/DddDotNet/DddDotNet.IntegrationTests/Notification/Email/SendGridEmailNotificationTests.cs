using DddDotNet.Infrastructure.Notification.Email;
using DddDotNet.Infrastructure.Notification.Email.SendGrid;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DddDotNet.IntegrationTests.Notification.Email;

public class SendGridEmailNotificationTests
{
    private SendGridOptions _options;

    public SendGridEmailNotificationTests()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddUserSecrets("09f024f8-e8d1-4b78-9ddd-da941692e8fa")
            .Build();

        _options = new SendGridOptions();

        config.GetSection("Notification:Email:SendGrid").Bind(_options);
    }

    [Fact]
    public async Task SendAsync_Success()
    {
        SendGridEmailNotification sendGrid = new SendGridEmailNotification(_options);

        await sendGrid.SendAsync(new EmailMessage
        {
            From = "phong@gmail.com",
            Tos = "phong@gmail.com",
            Subject = "IntegrationTests 1",
            Body = "IntegrationTests 1",
            Attachments = [new Attachment { FileName = "test.txt", Content = new MemoryStream(Encoding.UTF8.GetBytes("This is a text file.")) }]
        });

        await sendGrid.SendAsync(new EmailMessage
        {
            From = "phong@gmail.com",
            FromName = "",
            Tos = "phong@gmail.com",
            Subject = "IntegrationTests 2",
            Body = "IntegrationTests 2",
        });

        await sendGrid.SendAsync(new EmailMessage
        {
            From = "phong@gmail.com",
            FromName = "Phong Nguyen",
            Tos = "phong@gmail.com",
            Subject = "IntegrationTests 3",
            Body = "IntegrationTests 3",
        });
    }

    [Fact]
    public async Task HealthCheck_Healthy()
    {
        var healthCheck = new SendGridHealthCheck(_options, "Health Check", "Health Check");
        var checkResult = await healthCheck.CheckHealthAsync(new HealthCheckContext { Registration = new HealthCheckRegistration("Test", (x) => null, HealthStatus.Degraded, new string[] { }) });
        Assert.Equal(HealthStatus.Healthy, checkResult.Status);
    }

    [Fact]
    public async Task HealthCheck_Degraded()
    {
        _options.ApiKey = Guid.NewGuid().ToString();
        var healthCheck = new SendGridHealthCheck(_options, "Health Check", "Health Check");
        var checkResult = await healthCheck.CheckHealthAsync(new HealthCheckContext { Registration = new HealthCheckRegistration("Test", (x) => null, HealthStatus.Degraded, new string[] { }) });
        Assert.Equal(HealthStatus.Degraded, checkResult.Status);
    }
}
