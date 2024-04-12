using System.Globalization;
using System.Net;

using LogFilter.Exceptions;

namespace LogFilter.Models;

public sealed record LogEntry(IPAddress IPAddress, DateTime AccessTime)
{
    public static LogEntry Parse(string value)
    {
        var parts = value.Split(" : ");
        if (!IPAddress.TryParse(parts[0], out var address))
            throw new ParseException(parts[0]);

        if (!DateTime.TryParse(parts[1], CultureInfo.InvariantCulture, out var accessTime))
            throw new ParseException(parts[1]);

        return new(address, accessTime);
    }
}