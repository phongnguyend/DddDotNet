namespace DddDotNet.Infrastructure.Notification.Email.Amazon
{
    public class AmazonSesOptions
    {
        public string AccessKeyID { get; set; }

        public string SecretAccessKey { get; set; }

        public string RegionEndpoint { get; set; }

        public string FromEmail { get; set; }
    }
}
