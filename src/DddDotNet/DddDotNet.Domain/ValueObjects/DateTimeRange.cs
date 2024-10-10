using System;

namespace DddDotNet.Domain.ValueObjects;

public record DateTimeRange
{
    public DateTime Start { get; private set; }

    public DateTime End { get; private set; }

    public DateTimeRange(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
    }

    public DateTimeRange(DateTime start, TimeSpan duration)
        : this(start, start.Add(duration))
    {
    }

    public bool Overlaps(DateTimeRange dateTimeRange)
    {
        return Start < dateTimeRange.End && End > dateTimeRange.Start;
    }
}
