using System.Globalization;
using System.Net;

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
            throw new ArgumentException("Cant parse address start");
        }

        _filterParameters.AddressStart = address;
        return this;
    }

    public FilterParametersBuilder SetAddressMask(string addressMask)
    {
        if (!int.TryParse(addressMask, out var mask))
        {
            throw new ArgumentException("Cant parse address mask");
        }

        _filterParameters.AddressMask = mask;
        return this;
    }

    public FilterParametersBuilder SetTimeStart(string timeStart)
    {
        if (!DateOnly.TryParse(timeStart, CultureInfo.InvariantCulture, out var start))
        {
            throw new ArgumentException("Cant parse time start");
        }
        _filterParameters.TimeStart = start;
        return this;
    }

    public FilterParametersBuilder SetTimeEnd(string timeEnd)
    {
        if (!DateOnly.TryParse(timeEnd, CultureInfo.InvariantCulture, out var end))
        {
            throw new ArgumentException("Cant parse time end");
        }

        _filterParameters.TimeEnd = end;
        return this;
    }

    public FilterParameters Build()
    {
        if (_filterParameters.IsValid())
            return _filterParameters;

        throw new ArgumentException("Cant build filter parameters.");
    }
}