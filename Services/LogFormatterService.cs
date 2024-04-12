using LogFilter.Models;

namespace LogFilter.Services;

public sealed class LogFormatterService
{
    public IEnumerable<string> Format(IEnumerable<LogEntry> entries)
    {
        return entries
            .GroupBy(x => x.IPAddress)
            .Select(x => $"{x.Key} - {x.Count()}");
    }
}