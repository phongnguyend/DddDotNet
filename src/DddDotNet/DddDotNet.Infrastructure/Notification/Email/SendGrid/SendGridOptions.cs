namespace DddDotNet.Infrastructure.Notification.Email.SendGrid;

public class SendGridOptions
{
    public string Host { get; set; }

    public string ApiKey { get; set; }

    public string OverrideFrom { get; set; }

    public string OverrideTos { get; set; }
}
