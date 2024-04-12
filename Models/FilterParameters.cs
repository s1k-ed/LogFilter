using System.Net;

namespace LogFilter.Models;

public sealed class FilterParameters
{
    public string? LogFilePath { get; set; }
    public string? OutputFilePath { get; set; }
    public IPAddress? AddressStart { get; set; }
    public int? AddressMask { get; set; }
    public DateOnly? TimeStart { get; set; }
    public DateOnly? TimeEnd { get; set; }

    public bool IsValid() => !
        (string.IsNullOrWhiteSpace(LogFilePath) ||
        string.IsNullOrWhiteSpace(OutputFilePath) ||
        TimeStart is null ||
        TimeEnd is null ||
        (AddressMask is not null && AddressStart is null));
}