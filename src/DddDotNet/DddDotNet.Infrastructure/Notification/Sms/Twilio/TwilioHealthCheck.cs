using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Threading;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace DddDotNet.Infrastructure.Notification.Sms.Twilio
{
    public class TwilioHealthCheck : IHealthCheck
    {
        private readonly TwilioHealthCheckOptions _options;

        public TwilioHealthCheck(TwilioHealthCheckOptions options)
        {
            _options = options;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                TwilioClient.Init(_options.AccountSId, _options.AuthToken);

                var message = await MessageResource.CreateAsync(
                    body: _options.Message,
                    from: new PhoneNumber(_options.FromNumber),
                    to: new PhoneNumber(_options.ToNumber));

                if (!string.IsNullOrWhiteSpace(message?.Sid))
                {
                    return HealthCheckResult.Healthy();
                }
                else
                {
                    return new HealthCheckResult(context.Registration.FailureStatus, $"ErrorCode: {message?.ErrorCode}, ErrorMessage: {message?.ErrorMessage}");
                }
            }
            catch (Exception exception)
            {
                return new HealthCheckResult(context.Registration.FailureStatus, null, exception);
            }
        }
    }

    public class TwilioHealthCheckOptions : TwilioOptions
    {
        public string Message { get; set; }

        public string ToNumber { get; set; }
    }
}
