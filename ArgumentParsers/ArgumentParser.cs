using LogFilter.Exceptions;
using LogFilter.Interfaces;

namespace LogFilter.ArgumentParsers;

public sealed class ArgumentParser(string prefix, Action<string> handler) : IArgumentParser
{
    private readonly Action<string> _handler = handler;
    private readonly string _prefix = prefix;

    public bool CanHandle(string argument) => argument.StartsWith(_prefix);
    public void Handle(string argument)
    {
        var parts = argument.Split('=');

        var value = parts.Length != 2 || string.IsNullOrWhiteSpace(parts[1])
            ? throw new NoValueProvidedException(_prefix)
            : parts[1];

        _handler(value);
    }
}