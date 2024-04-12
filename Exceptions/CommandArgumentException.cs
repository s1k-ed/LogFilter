namespace LogFilter.Exceptions;

public sealed class UnknownArgumentException(string value) : ArgumentException($"Unknown argument - {value}");
public sealed class NoValueProvidedException(string key) : ArgumentException($"No value provided for the key - {key}");
public sealed class IsRequiredException(string key) : ArgumentException($"{key} is required");