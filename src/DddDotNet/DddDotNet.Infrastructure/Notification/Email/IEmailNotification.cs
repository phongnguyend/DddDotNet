using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace DddDotNet.Infrastructure.Notification.Email;

public interface IEmailNotification
{
    Task SendAsync(IEmailMessage emailMessage, CancellationToken cancellationToken = default);
}

public interface IEmailMessage
{
    public string From { get; set; }

    public string FromName { get; set; }

    public string Tos { get; set; }

    public string CCs { get; set; }

    public string BCCs { get; set; }

    public string Subject { get; set; }

    public string Body { get; set; }

    public List<Attachment> Attachments { get; set; }
}

public class Attachment
{
    public string FileName { get; set; }

    public Stream Content { get; set; }
}
