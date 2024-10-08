using System;
using System.Collections.Generic;

namespace DddDotNet.Domain.ValueObjects;

public class DateTimeOffsetRange : ValueObject
{
    public DateTimeOffset Start { get; private set; }

    public DateTimeOffset End { get; private set; }

    public DateTimeOffsetRange(DateTimeOffset start, DateTimeOffset end)
    {
        Start = start;
        End = end;
    }

    public DateTimeOffsetRange(DateTimeOffset start, TimeSpan duration)
        : this(start, start.Add(duration))
    {
    }

    public bool Overlaps(DateTimeOffsetRange dateTimeOffsetRange)
    {
        return Start < dateTimeOffsetRange.End && End > dateTimeOffsetRange.Start;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Start;
        yield return End;
    }
}
