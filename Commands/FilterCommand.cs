using LogFilter.ArgumentParsers;
using LogFilter.Exceptions;
using LogFilter.Helpers;
using LogFilter.Interfaces;
using LogFilter.Services;

namespace LogFilter.Commands;

public sealed class FilterCommand(string configurationFilePath) : ICommand
{
    public string Name => "Filter";
    public string Description => "Filtering the given log file";
    public string Help => "> filter --file-log=log.txt --file-output=out.txt --address-start=1.0.0.0 \n --address-mask=24 --time-start=01.01.2010 --time-end=01.01.2024";

    public void Execute(string[] args)
    {
        var builder = FilterParametersBuilder.CreateFromFile(configurationFilePath);

        var handlers = new List<ArgumentParser>()
        {
            new ("--file-log", (value) => builder.SetLogFilePath(value)),
            new ("--file-output", (value) => builder.SetOutputFilePath(value)),
            new ("--address-start", (value) => builder.SetAddressStart(value)),
            new ("--address-mask", (value) => builder.SetAddressMask(value)),
            new ("--time-start", (value) => builder.SetTimeStart(value)),
            new ("--time-end", (value) => builder.SetTimeEnd(value))
        };

        foreach (var arg in args)
        {
            var handler = handlers.Find(x => x.CanHandle(arg));

            if (handler is null)
                throw new UnknownArgumentException(arg);
            else
                handler.Handle(arg);
        }

        var parameters = builder.Build();

        var executor = new LogFilterExecutor(parameters);

        executor.Execute();
    }
}
