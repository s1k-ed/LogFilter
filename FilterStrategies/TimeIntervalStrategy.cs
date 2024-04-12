using LogFilter.Interfaces;
using LogFilter.Models;

namespace LogFilter.FilterStrategies;

public sealed class TimeIntervalStrategy(DateOnly start, DateOnly end) : IFilterStrategy
{
    private readonly DateOnly _start = start;
    private readonly DateOnly _end = end;

    public bool IsMatch(LogEntry entry)
    {
        var accessTime = DateOnly.FromDateTime(entry.AccessTime);

        return accessTime.CompareTo(_start) >= 0 &&
            accessTime.CompareTo(_end) <= 0;
    }
}