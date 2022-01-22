using System;

namespace DddDotNet.CrossCuttingConcerns.ExtensionMethods
{
    public static class DecimalExtensions
    {
        public static decimal Limit(this decimal value, decimal min, decimal max)
        {
            return Math.Min(Math.Max(value, min), max);
        }
    }
}
