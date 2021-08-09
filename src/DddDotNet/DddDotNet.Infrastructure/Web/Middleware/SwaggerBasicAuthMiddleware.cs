using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DddDotNet.Infrastructure.Web.Middleware
{
    public class SwaggerBasicAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _cache;

        public SwaggerBasicAuthMiddleware(RequestDelegate next, IMemoryCache cache)
        {
            _next = next;
            _cache = cache;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/swagger"))
            {
                string authHeader = context.Request.Headers["Authorization"];
                if (authHeader != null && authHeader.StartsWith("Basic "))
                {
                    // Get the encoded username and password
                    var encodedUsernamePassword = authHeader.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]?.Trim();

                    // Decode from Base64 to string
                    var decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));

                    // Split username and password
                    var username = decodedUsernamePassword.Split(':', 2)[0];
                    var password = decodedUsernamePassword.Split(':', 2)[1];

                    // Check if login is correct
                    if (IsAuthorized(username, password))
                    {
                        await _next.Invoke(context);
                        return;
                    }
                }

                // Return authentication type (causes browser to show login dialog)
                context.Response.Headers["WWW-Authenticate"] = "Basic";

                // Return unauthorized
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            else
            {
                await _next.Invoke(context);
            }
        }

        public bool IsAuthorized(string clientId, string clientSecret)
        {
            var isAuthorized = clientId.Equals("TestUser", StringComparison.InvariantCultureIgnoreCase) && clientSecret.Equals("TestPassword");

            if (!isAuthorized)
            {
                return isAuthorized;
            }

            var cacheKey = $"{clientId}-{clientSecret}";
            if (_cache.TryGetValue(cacheKey, out DateTime cacheExpiredAt))
            {
                if (cacheExpiredAt < DateTime.Now)
                {
                    _cache.Remove(cacheKey);
                    return false;
                }
            }
            else
            {
                var expiredAt = DateTime.Now.AddMinutes(5);
                _cache.Set(cacheKey, expiredAt);
            }

            return isAuthorized;
        }
    }
}
