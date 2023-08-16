using Microsoft.Extensions.Diagnostics.HealthChecks;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace DddDotNet.Infrastructure.Notification.Email.SendGrid;

public class SendGridHealthCheck : IHealthCheck
{
    private readonly SendGridOptions _options;
    private readonly string _subject;
    private readonly string _body;

    public SendGridHealthCheck(SendGridOptions options, string subject, string body)
    {
        _options = options;
        _subject = subject;
        _body = body;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            var client = new SendGridClient(_options.ApiKey);
            var from = new EmailAddress(_options.OverrideFrom);

            var tos = _options.OverrideTos.Split(';')
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => new EmailAddress(x))
                .ToList();

            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, _subject, string.Empty, _body, showAllRecipients: true);
            var response = await client.SendEmailAsync(msg, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Accepted)
            {
                return HealthCheckResult.Healthy();
            }
            else
            {
                return new HealthCheckResult(context.Registration.FailureStatus, response.StatusCode.ToString());
            }
        }
        catch (Exception exception)
        {
            return new HealthCheckResult(context.Registration.FailureStatus, null, exception);
        }
    }
}
