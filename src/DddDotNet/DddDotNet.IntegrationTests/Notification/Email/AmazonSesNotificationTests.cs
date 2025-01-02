using DddDotNet.Infrastructure.Notification.Email;
using DddDotNet.Infrastructure.Notification.Email.Amazon;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading.Tasks;
using Xunit;

namespace DddDotNet.IntegrationTests.Notification.Email;

public class AmazonSesNotificationTests
{
    private AmazonSesOptions _options;
    private AmazonSesHealthCheckOptions _healthCheckOptions;

    public AmazonSesNotificationTests()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddUserSecrets("09f024f8-e8d1-4b78-9ddd-da941692e8fa")
            .Build();

        _options = new AmazonSesOptions();

        _healthCheckOptions = new AmazonSesHealthCheckOptions
        {
            Subject = "Health Check",
            Body = "Health Check",
        };

        config.GetSection("Notification:Email:AmazonSES").Bind(_options);
        config.GetSection("Notification:Email:AmazonSES").Bind(_healthCheckOptions);
    }

    [Fact]
    public async Task SendAsync_Success()
    {
        AmazonSesNotification client = new AmazonSesNotification(_options);

        await client.SendAsync(new EmailMessage
        {
            From = _options.FromEmail,
            Tos = _options.FromEmail,
            Subject = "IntegrationTests 1",
            Body = "IntegrationTests 1",
        });

        await client.SendAsync(new EmailMessage
        {
            From = _options.FromEmail,
            FromName = "",
            Tos = _options.FromEmail,
            Subject = "IntegrationTests 2",
            Body = "IntegrationTests 2",
        });

        await client.SendAsync(new EmailMessage
        {
            From = _options.FromEmail,
            FromName = "Phong Nguyen",
            Tos = _options.FromEmail,
            Subject = "IntegrationTests 3",
            Body = "IntegrationTests 3",
        });
    }

    [Fact]
    public async Task HealthCheck_Healthy()
    {
        var healthCheck = new AmazonSesHealthCheck(_healthCheckOptions);
        var checkResult = await healthCheck.CheckHealthAsync(new HealthCheckContext { Registration = new HealthCheckRegistration("Test", (x) => null, HealthStatus.Degraded, new string[] { }) });
        Assert.Equal(HealthStatus.Healthy, checkResult.Status);
    }

    [Fact]
    public async Task HealthCheck_Degraded()
    {
        _healthCheckOptions.FromEmail = "abc@gmail.com";
        var healthCheck = new AmazonSesHealthCheck(_healthCheckOptions);
        var checkResult = await healthCheck.CheckHealthAsync(new HealthCheckContext { Registration = new HealthCheckRegistration("Test", (x) => null, HealthStatus.Degraded, new string[] { }) });
        Assert.Equal(HealthStatus.Degraded, checkResult.Status);
    }
}
