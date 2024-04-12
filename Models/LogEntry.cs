using System.Globalization;
using System.Net;

namespace LogFilter.Models;

public sealed record LogEntry(IPAddress IPAddress, DateTime AccessTime)
{
    public static LogEntry Parse(string value)
    {
        var parts = value.Split(" : ");
        if (!IPAddress.TryParse(parts[0], out var address))
            throw new ArgumentException($"Cant parse ip - {parts[0]}");

        if (!DateTime.TryParse(parts[1], CultureInfo.InvariantCulture, out var accessTime))
            throw new ArgumentException($"Cant parse date - {parts[1]}");

        return new(address, accessTime);
    }
}