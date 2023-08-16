using DddDotNet.Infrastructure.Notification.Email;
using DddDotNet.Infrastructure.Notification.Email.Fake;
using DddDotNet.Infrastructure.Notification.Email.SendGrid;
using DddDotNet.Infrastructure.Notification.Email.Smtp;

namespace Microsoft.Extensions.DependencyInjection;

public static class EmailNotificationServiceCollectionExtensions
{
    public static IServiceCollection AddSmtpEmailNotification(this IServiceCollection services, SmtpOptions options)
    {
        services.AddSingleton<IEmailNotification>(new SmtpEmailNotification(options));
        return services;
    }

    public static IServiceCollection AddFakeEmailNotification(this IServiceCollection services)
    {
        services.AddSingleton<IEmailNotification>(new FakeEmailNotification());
        return services;
    }

    public static IServiceCollection AddSendGridEmailNotification(this IServiceCollection services, SendGridOptions options)
    {
        services.AddSingleton<IEmailNotification>(new SendGridEmailNotification(options));
        return services;
    }

    public static IServiceCollection AddEmailNotification(this IServiceCollection services, EmailOptions options)
    {
        if (options.UsedFake())
        {
            services.AddFakeEmailNotification();
        }
        else if (options.UsedSmtpClient())
        {
            services.AddSmtpEmailNotification(options.SmtpClient);
        }
        else if (options.UsedSendGrid())
        {
            services.AddSendGridEmailNotification(options.SendGrid);
        }

        return services;
    }
}
