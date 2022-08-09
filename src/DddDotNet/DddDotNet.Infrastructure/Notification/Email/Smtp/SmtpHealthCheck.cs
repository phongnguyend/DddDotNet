using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace DddDotNet.Infrastructure.Notification.Email.Smtp
{
    public class SmtpHealthCheck : IHealthCheck
    {
        private readonly SmtpHealthCheckOptions _options;

        public SmtpHealthCheck(SmtpHealthCheckOptions options)
        {
            _options = options;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var mail = new MailMessage
                {
                    From = new MailAddress(_options.From, _options.FromName),
                };

                _options.Tos?.Split(';')
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .ToList()
                    .ForEach(x => mail.To.Add(x));

                _options.CCs?.Split(';')
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .ToList()
                    .ForEach(x => mail.CC.Add(x));

                _options.BCCs?.Split(';')
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .ToList()
                    .ForEach(x => mail.Bcc.Add(x));

                mail.Subject = _options.Subject;

                mail.Body = _options.Body;

                mail.IsBodyHtml = true;

                var smtpClient = new System.Net.Mail.SmtpClient(_options.Host);

                if (_options.Port.HasValue)
                {
                    smtpClient.Port = _options.Port.Value;
                }

                if (!string.IsNullOrWhiteSpace(_options.UserName) && !string.IsNullOrWhiteSpace(_options.Password))
                {
                    smtpClient.Credentials = new System.Net.NetworkCredential(_options.UserName, _options.Password);
                }

                if (_options.EnableSsl.HasValue)
                {
                    smtpClient.EnableSsl = _options.EnableSsl.Value;
                }

                await smtpClient.SendMailAsync(mail, cancellationToken);

                return HealthCheckResult.Healthy();
            }
            catch (Exception exception)
            {
                return new HealthCheckResult(context.Registration.FailureStatus, null, exception);
            }
        }
    }

    public class SmtpHealthCheckOptions : SmtpOptions
    {
        public string From { get; set; }

        public string FromName { get; set; }

        public string Tos { get; set; }

        public string CCs { get; set; }

        public string BCCs { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}
