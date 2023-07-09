namespace DddDotNet.Infrastructure.Identity.Azure;

public class AzureAdB2COptions
{
    public string TenantId { get; set; }

    public string TenantDomain { get; set; }

    public string AppId { get; set; }

    public string ClientSecret { get; set; }
}
