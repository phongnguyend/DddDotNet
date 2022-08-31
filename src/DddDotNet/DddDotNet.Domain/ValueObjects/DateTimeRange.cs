using System;
using System.Collections.Generic;

namespace DddDotNet.Domain.ValueObjects
{
    public class DateTimeRange : ValueObject
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

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Start;
            yield return End;
        }
    }
}
