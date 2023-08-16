namespace DddDotNet.Infrastructure.Storages.Google;

public class GoogleCloudStorageOptions
{
    public string CredentialFilePath { get; set; }

    public string BucketName { get; set; }

    public string Path { get; set; }
}
