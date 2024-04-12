using LogFilter.Interfaces;
using LogFilter.Models;

namespace LogFilter.Services;

public sealed class LogFilterService(IReadOnlyList<IFilterStrategy> filters)
{
    private readonly IReadOnlyList<IFilterStrategy> _filters = filters;

    public IEnumerable<LogEntry> Filter(IEnumerable<LogEntry> entries)
    {
        return entries
            .Where(x => _filters.All(y => y.IsMatch(x)));
    }
}