namespace LogFilter.Exceptions;

public sealed class ParseException(string value) : ArgumentException($"Cant parse value - {value}");