using Microsoft.AspNetCore.Authorization;

namespace DddDotNet.Infrastructure.Web.Authorization.Policies;

public interface IPolicy
{
    void Configure(AuthorizationPolicyBuilder policy);
}
