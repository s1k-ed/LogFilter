using System.Net;

using LogFilter.Helpers;
using LogFilter.Interfaces;
using LogFilter.Models;

namespace LogFilter.FilterStrategies;

public sealed class IpAddressFilterStrategy(IPAddress startAddress, int? mask) : IFilterStrategy
{
    private readonly IPAddress _startAddress = startAddress;
    private readonly int? _mask = mask;

    public bool IsMatch(LogEntry entry)
    {
        return entry.IPAddress.IsInSameSubnet(_startAddress, _mask);
    }
}