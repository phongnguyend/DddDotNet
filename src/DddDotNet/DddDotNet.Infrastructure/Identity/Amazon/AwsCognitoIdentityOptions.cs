namespace DddDotNet.Infrastructure.Identity.GoogleCloud;

public class AwsCognitoIdentityOptions
{
    public string AccessKeyID { get; set; }

    public string SecretAccessKey { get; set; }

    public string UserPoolID { get; set; }

    public string RegionEndpoint { get; set; }
}
