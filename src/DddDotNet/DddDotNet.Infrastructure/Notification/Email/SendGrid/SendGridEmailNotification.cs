using DddDotNet.CrossCuttingConcerns.ExtensionMethods;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
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

        var uniqueEmails = new HashSet<string>();

        foreach (var to in emailMessage.Tos.ToLowerInvariant().Split(";").Select(x => x.Trim()))
        {
            if (!string.IsNullOrWhiteSpace(to) && uniqueEmails.Add(to))
            {
                msg.AddTo(to);
            }
        }

        if (!string.IsNullOrWhiteSpace(emailMessage.CCs))
        {
            foreach (var cc in emailMessage.CCs.ToLowerInvariant().Split(";").Select(x => x.Trim()).Where(cc => !string.IsNullOrWhiteSpace(cc) && uniqueEmails.Add(cc)))
            {
                msg.AddCc(cc);
            }
        }

        if (!string.IsNullOrWhiteSpace(emailMessage.BCCs))
        {
            foreach (var bcc in emailMessage.BCCs.ToLowerInvariant().Split(";").Select(x => x.Trim()).Where(bcc => !string.IsNullOrWhiteSpace(bcc) && uniqueEmails.Add(bcc)))
            {
                msg.AddBcc(bcc);
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
