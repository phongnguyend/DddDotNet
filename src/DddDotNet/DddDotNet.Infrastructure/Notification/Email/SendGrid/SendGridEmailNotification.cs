using DddDotNet.CrossCuttingConcerns.ExtensionMethods;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace DddDotNet.Infrastructure.Notification.Email.SendGrid;

public class SendGridEmailNotification : IEmailNotification
{
    private readonly SendGridOptions _options;

    public SendGridEmailNotification(SendGridOptions options)
    {
        _options = options;
    }

    public async Task SendAsync(IEmailMessage emailMessage, CancellationToken cancellationToken = default)
    {
        var client = new SendGridClient(_options.ApiKey, _options.Host);

        var msg = new SendGridMessage
        {
            From = new EmailAddress(!string.IsNullOrWhiteSpace(_options.OverrideFrom) ? _options.OverrideFrom : emailMessage.From, emailMessage.FromName),
            Subject = emailMessage.Subject,
            HtmlContent = emailMessage.Body
        };

        foreach (var to in emailMessage.Tos.Split(";"))
        {
            if (!string.IsNullOrWhiteSpace(to))
            {
                msg.AddTo(to.Trim());
            }
        }

        if (!string.IsNullOrWhiteSpace(emailMessage.CCs))
        {
            foreach (var cc in emailMessage.CCs.Split(";").Where(cc => !string.IsNullOrWhiteSpace(cc)))
            {
                msg.AddCc(cc.Trim());
            }
        }

        if (!string.IsNullOrWhiteSpace(emailMessage.BCCs))
        {
            foreach (var bcc in emailMessage.BCCs.Split(";").Where(bcc => !string.IsNullOrWhiteSpace(bcc)))
            {
                msg.AddBcc(bcc.Trim());
            }
        }

        if (emailMessage.Attachments != null)
        {
            foreach (var attachment in emailMessage.Attachments)
            {
                msg.AddAttachment(attachment.FileName, Convert.ToBase64String(attachment.Content.GetBytes()));
            }
        }

        var response = await client.SendEmailAsync(msg, cancellationToken);

        if (response.StatusCode != HttpStatusCode.Accepted)
        {
            throw new SendGridException(response.StatusCode, await response.Body.ReadAsStringAsync());
        }
    }
}
