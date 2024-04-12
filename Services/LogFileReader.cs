using LogFilter.Models;

namespace LogFilter.Services;

public sealed class LogFileReader(string filePath)
{
    private readonly string _filePath = filePath;

    public IEnumerable<LogEntry> Read()
    {
        var lines = File.ReadAllLines(_filePath);

        return lines.Where(x => !string.IsNullOrWhiteSpace(x)).Select(LogEntry.Parse);
    }
}