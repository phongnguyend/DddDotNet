using System;

namespace DddDotNet.Infrastructure.Identity
{
    public class AnonymousUser : ICurrentUser
    {
        public bool IsAuthenticated => false;

        public Guid UserId => Guid.Empty;
    }
}
