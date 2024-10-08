using System;

namespace DddDotNet.CrossCuttingConcerns.ExtensionMethods;

public static class IntExtensions
{
    public static int Limit(this int value, int min, int max)
    {
        return Math.Min(Math.Max(value, min), max);
    }
}
