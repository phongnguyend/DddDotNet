namespace DddDotNet.Infrastructure.Storages.Smb
{
    public class SmbFileShareOptions
    {
        public string HostName { get; set; }

        public string DomainName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string ShareName { get; set; }

        public string Path { get; set; }
    }
}
