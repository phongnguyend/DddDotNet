using System;
using System.Net;

namespace DddDotNet.Infrastructure.Notification.Email.SendGrid;

public class SendGridException : Exception
{
    public SendGridException(HttpStatusCode statusCode, string message)
        : base($"StatusCode: {statusCode}, Message: {message}")
    {
    }
}
