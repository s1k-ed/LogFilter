using LogFilter.Interfaces;

namespace LogFilter.Commands;

public sealed class ExitCommand : ICommand
{
    public string Name => "Exit";
    public string Description => "Closing the application";
    public string Help => "> exit";

    public void Execute(string[] args)
    {
        Environment.Exit(0);
    }
}
