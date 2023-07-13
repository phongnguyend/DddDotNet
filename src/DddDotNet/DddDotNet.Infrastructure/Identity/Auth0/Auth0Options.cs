namespace DddDotNet.Infrastructure.Identity.Auth0;

public class Auth0Options
{
    public string TokenUrl { get; set; }

    public string ClientId { get; set; }

    public string ClientSecret { get; set; }

    public string Audience { get; set; }
}
