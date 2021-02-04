using Microsoft.AspNetCore.Authorization;
using System;

namespace DddDotNet.Infrastructure.Web.Authorization.Policies
{
    public class AuthorizePolicyAttribute : AuthorizeAttribute
    {
        public AuthorizePolicyAttribute(Type policy)
            : base(policy.FullName)
        {
        }
    }
}
