namespace DddDotNet.Infrastructure.Storages.Azure;

public class AzureFileShareOptions
{
    public string ConnectionString { get; set; }

    public string ShareName { get; set; }

    public string Path { get; set; }
}
