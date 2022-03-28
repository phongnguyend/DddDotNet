using System;

namespace DddDotNet.CrossCuttingConcerns.Locks
{
    public interface IDistributedLockScope : IDisposable
    {
        bool StillHoldingLock();
    }
}
