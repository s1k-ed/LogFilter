using LogFilter.FilterStrategies;
using LogFilter.Interfaces;
using LogFilter.Models;

namespace LogFilter.Services;

public sealed class LogFilterExecutor
{
    private readonly LogFileReader _reader;
    private readonly LogFileWriter _writer;
    private readonly LogFilterService _filterService;
    private readonly LogFormatterService _formatterService;

    public LogFilterExecutor(FilterParameters parameters)
    {
        _reader = new(parameters.LogFilePath!);
        _writer = new(parameters.OutputFilePath!);

        var filters = new List<IFilterStrategy>
        {
            new TimeIntervalStrategy(parameters.TimeStart!.Value, parameters.TimeEnd!.Value)
        };
        if (parameters.AddressStart is not null)
        {
            filters.Add(new IpAddressFilterStrategy(parameters.AddressStart, parameters.AddressMask));
        }

        _filterService = new(filters);
        _formatterService = new();
    }

    public void Execute()
    {
        var entries = _reader.Read();
        var filteredEntries = _filterService.Filter(entries);
        var formattedEntries = _formatterService.Format(filteredEntries);
        _writer.Write(formattedEntries);
    }
}