using DddDotNet.Infrastructure.Notification.Sms.Twilio;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading.Tasks;
using Xunit;

namespace DddDotNet.IntegrationTests.Notification.Sms;

public class TwilioSmsNotificationTests
{
    private TwilioOptions _options;
    private TwilioHealthCheckOptions _healthCheckOptions;
    private IConfiguration _configuration;

    public TwilioSmsNotificationTests()
    {
        _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddUserSecrets("09f024f8-e8d1-4b78-9ddd-da941692e8fa")
            .Build();

        _options = new TwilioOptions();
        _healthCheckOptions = new TwilioHealthCheckOptions();

        _configuration.GetSection("Notification:Sms:Twilio").Bind(_options);
        _configuration.GetSection("Notification:Sms:Twilio").Bind(_healthCheckOptions);

        _healthCheckOptions.Message = "Health Check";
    }

    [Fact]
    public async Task SendAsync_Success()
    {
        TwilioSmsNotification twilio = new TwilioSmsNotification(_options);

        await twilio.SendAsync(new SmsMessage
        {
            Message = "DddDotNet IntegrationTests",
            PhoneNumber = _healthCheckOptions.ToNumber,
        });
    }

    [Fact]
    public async Task HealthCheck_Healthy()
    {
        var healthCheck = new TwilioHealthCheck(_healthCheckOptions);
        var checkResult = await healthCheck.CheckHealthAsync(new HealthCheckContext { Registration = new HealthCheckRegistration("Test", (x) => null, HealthStatus.Degraded, new string[] { }) });
        Assert.Equal(HealthStatus.Healthy, checkResult.Status);
    }

    [Fact]
    public async Task HealthCheck_Degraded()
    {
        _healthCheckOptions.AuthToken = "xxx";
        var healthCheck = new TwilioHealthCheck(_healthCheckOptions);
        var checkResult = await healthCheck.CheckHealthAsync(new HealthCheckContext { Registration = new HealthCheckRegistration("Test", (x) => null, HealthStatus.Degraded, new string[] { }) });
        Assert.Equal(HealthStatus.Degraded, checkResult.Status);
    }
}
