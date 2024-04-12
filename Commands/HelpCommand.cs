using LogFilter.Interfaces;

namespace LogFilter.Commands;

public sealed class HelpCommand(IEnumerable<ICommand> commands) : ICommand
{
    public string Name => "Help";
    public string Description => "Writing information about all commands";
    public string Help => "> help";

    public void Execute(string[] args)
    {
        Console.WriteLine("Список доступных комманд: ");

        commands.ToList().ForEach(x =>
        {
            Console.WriteLine($"{x.Name}: {x.Description}");
            Console.WriteLine(x.Help);
            Console.WriteLine();
        });
    }
}
