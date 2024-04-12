namespace LogFilter.Models;

public class FilterParametersConfig
{
    public string? LogFilePath { get; set; }
    public string? OutputFilePath { get; set; }
    public string? AddressStart { get; set; }
    public int? AddressMask { get; set; }
    public string? TimeStart { get; set; }
    public string? TimeEnd { get; set; }
}