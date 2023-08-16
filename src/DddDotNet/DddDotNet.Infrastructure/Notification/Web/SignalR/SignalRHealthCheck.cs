using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DddDotNet.Infrastructure.Notification.Web.SignalR;

public class SignalRHealthCheck : IHealthCheck
{
    private readonly string _endPoint;
    private readonly string _eventName;

    public SignalRHealthCheck(string endPoint, string hubName, string eventName)
    {
        _endPoint = endPoint + "/" + hubName;
        _eventName = eventName;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            await using (var connection = new HubConnectionBuilder()
                .WithUrl(_endPoint)
                .AddMessagePackProtocol()
                .Build())
            {
                await connection.StartAsync(cancellationToken);
                await connection.StopAsync(cancellationToken);
            }

            return HealthCheckResult.Healthy();
        }
        catch (Exception ex)
        {
            return new HealthCheckResult(context.Registration.FailureStatus, exception: ex);
        }
    }
}
