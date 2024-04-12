using LogFilter.Exceptions;
using LogFilter.Helpers;
using LogFilter.Interfaces;

namespace LogFilter.Commands;

public sealed class GenerateDataCommand : ICommand
{
    public string Name => "Generate";
    public string Description => "Generating the log file";
    public string Help => "> generate here.txt 1000";

    public void Execute(string[] args)
    {
        if (args.Length != 2)
            throw new ArgumentException($"Expected 2 arguments, was {args.Length}");

        var path = args[0];

        if (!int.TryParse(args[1], out var count))
            throw new ParseException(args[1]);

        var generator = new LogFileGenerator();
        generator.Generate(path, count);
    }
}
