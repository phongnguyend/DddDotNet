using DddDotNet.CrossCuttingConcerns.ExtensionMethods;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace DddDotNet.Infrastructure.Notification.Email.SendGrid;

public class SendGridHealthCheck : IHealthCheck
{
    private readonly SendGridHealthCheckOptions _options;

    public SendGridHealthCheck(SendGridHealthCheckOptions options)
    {
        _options = options;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            var client = new SendGridClient(_options.ApiKey);

            var tos = _options.Tos.Split(';')
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => new EmailAddress(x))
                .ToList();

            var msg = new SendGridMessage
            {
                From = new EmailAddress(_options.From),
                Subject = _options.Subject,
                HtmlContent = _options.Body
            };

            foreach (var to in _options.Tos.Split(";"))
            {
                if (!string.IsNullOrWhiteSpace(to))
                {
                    msg.AddTo(to.Trim());
                }
            }

            if (!string.IsNullOrWhiteSpace(_options.CCs))
            {
                foreach (var cc in _options.CCs.Split(";").Where(cc => !string.IsNullOrWhiteSpace(cc)))
                {
                    msg.AddCc(cc.Trim());
                }
            }

            if (!string.IsNullOrWhiteSpace(_options.BCCs))
            {
                foreach (var bcc in _options.BCCs.Split(";").Where(bcc => !string.IsNullOrWhiteSpace(bcc)))
                {
                    msg.AddBcc(bcc.Trim());
                }
            }

            if (_options.Attachments != null)
            {
                foreach (var attachment in _options.Attachments)
                {
                    msg.AddAttachment(attachment.FileName, Convert.ToBase64String(attachment.Content.GetBytes()));
                }
            }

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

public class SendGridHealthCheckOptions
{
    public string Host { get; set; }

    public string ApiKey { get; set; }

    public string Subject { get; set; }

    public string Body { get; set; }

    public string From { get; set; }

    public string Tos { get; set; }

    public string CCs { get; set; }

    public string BCCs { get; set; }

    public List<Attachment> Attachments { get; set; }
}
