using System.Globalization;
using System.Net;
using System.Text.Json;

using LogFilter.Exceptions;
using LogFilter.Models;

namespace LogFilter.Helpers;

public class FilterParametersBuilder
{
    private readonly FilterParameters _filterParameters = new();

    public FilterParametersBuilder SetLogFilePath(string fileLog)
    {
        _filterParameters.LogFilePath = fileLog;
        return this;
    }

    public FilterParametersBuilder SetOutputFilePath(string fileOutput)
    {
        _filterParameters.OutputFilePath = fileOutput;
        return this;
    }

    public FilterParametersBuilder SetAddressStart(string addressStart)
    {
        if (!IPAddress.TryParse(addressStart, out var address))
        {
            throw new ParseException(addressStart);
        }

        _filterParameters.AddressStart = address;
        return this;
    }

    public FilterParametersBuilder SetAddressMask(string addressMask)
    {
        if (!int.TryParse(addressMask, out var mask) || mask < 0)
        {
            throw new ParseException(addressMask);
        }

        _filterParameters.AddressMask = mask;
        return this;
    }

    public FilterParametersBuilder SetTimeStart(string timeStart)
    {
        if (!DateOnly.TryParse(timeStart, CultureInfo.InvariantCulture, out var start))
        {
            throw new ParseException(timeStart);
        }
        _filterParameters.TimeStart = start;
        return this;
    }

    public FilterParametersBuilder SetTimeEnd(string timeEnd)
    {
        if (!DateOnly.TryParse(timeEnd, CultureInfo.InvariantCulture, out var end))
        {
            throw new ParseException(timeEnd);
        }

        _filterParameters.TimeEnd = end;
        return this;
    }

    public FilterParameters Build()
    {
        if (string.IsNullOrWhiteSpace(_filterParameters.LogFilePath))
            throw new IsRequiredException(nameof(_filterParameters.LogFilePath));
        if (string.IsNullOrWhiteSpace(_filterParameters.OutputFilePath))
            throw new IsRequiredException(nameof(_filterParameters.OutputFilePath));
        if (_filterParameters.TimeStart is null)
            throw new IsRequiredException(nameof(_filterParameters.TimeStart));
        if (_filterParameters.TimeEnd is null)
            throw new IsRequiredException(nameof(_filterParameters.TimeEnd));
        if (_filterParameters.AddressMask is not null &&
            _filterParameters.AddressStart is null)
            throw new IsRequiredException(nameof(_filterParameters.AddressStart));

        return _filterParameters;
    }

    public static FilterParametersBuilder CreateFromFile(string path)
    {
        if (!File.Exists(path)) return new();

        var json = File.ReadAllText(path);
        var config = JsonSerializer.Deserialize<FilterParametersConfig>(json);

        if (config is null) return new();

        var builder = new FilterParametersBuilder();

        if (!string.IsNullOrEmpty(config.LogFilePath))
        {
            builder.SetLogFilePath(config.LogFilePath);
        }
        if (!string.IsNullOrEmpty(config.OutputFilePath))
        {
            builder.SetOutputFilePath(config.OutputFilePath);
        }
        if (!string.IsNullOrEmpty(config.AddressStart))
        {
            builder.SetAddressStart(config.AddressStart);
        }
        if (config.AddressMask.HasValue)
        {
            builder.SetAddressMask(config.AddressMask.Value.ToString());
        }
        if (!string.IsNullOrEmpty(config.TimeStart))
        {
            builder.SetTimeStart(config.TimeStart);
        }
        if (!string.IsNullOrEmpty(config.TimeEnd))
        {
            builder.SetTimeEnd(config.TimeEnd);
        }

        return builder;
    }
}