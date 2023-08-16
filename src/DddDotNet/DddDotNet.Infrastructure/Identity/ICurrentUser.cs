using System;

namespace DddDotNet.Infrastructure.Identity;

public interface ICurrentUser
{
    bool IsAuthenticated { get; }

    Guid UserId { get; }
}
