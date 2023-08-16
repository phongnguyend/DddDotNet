using DddDotNet.Infrastructure.Notification.Email.Smtp;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DddDotNet.IntegrationTests.Notification.Email;

public class SmtpEmailNotificationTests
{
    public SmtpEmailNotificationTests()
    {
    }

    [Fact]
    public async Task SendAsync_Success()
    {
        SmtpEmailNotification smtpClient = new SmtpEmailNotification(new SmtpOptions
        {
            Host = "localhost",
        });

        await smtpClient.SendAsync(new EmailMessage
        {
            From = "phong@gmail.com",
            Tos = "phong@gmail.com",
            Subject = "IntegrationTests 1",
            Body = "IntegrationTests 1",
        });

        await smtpClient.SendAsync(new EmailMessage
        {
            From = "phong@gmail.com",
            FromName = "",
            Tos = "phong@gmail.com",
            Subject = "IntegrationTests 2",
            Body = "IntegrationTests 2",
        });

        await smtpClient.SendAsync(new EmailMessage
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
        var healthCheck = new SmtpHealthCheck(new SmtpHealthCheckOptions
        {
            Host = "localhost",
            From = "phong@gmail.com",
            FromName = "Phong Nguyen",
            Tos = "phong@gmail.com",
            Subject = "HealthCheck",
            Body = "HealthCheck",
        });
        var checkResult = await healthCheck.CheckHealthAsync(new HealthCheckContext { Registration = new HealthCheckRegistration("Test", (x) => null, HealthStatus.Degraded, new string[] { }) });
        Assert.Equal(HealthStatus.Healthy, checkResult.Status);
    }

    [Fact]
    public async Task HealthCheck_Degraded()
    {
        var healthCheck = new SmtpHealthCheck(new SmtpHealthCheckOptions
        {
            Host = Guid.NewGuid().ToString(),
            From = "phong@gmail.com",
            FromName = "Phong Nguyen",
            Tos = "phong@gmail.com",
            Subject = "HealthCheck",
            Body = "HealthCheck",
        });
        var checkResult = await healthCheck.CheckHealthAsync(new HealthCheckContext { Registration = new HealthCheckRegistration("Test", (x) => null, HealthStatus.Degraded, new string[] { }) });
        Assert.Equal(HealthStatus.Degraded, checkResult.Status);
    }
}
