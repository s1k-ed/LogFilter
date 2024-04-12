using LogFilter.Models;

namespace LogFilter.Interfaces;

public interface IFilterStrategy
{
    bool IsMatch(LogEntry entry);
}