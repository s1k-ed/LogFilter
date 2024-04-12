namespace LogFilter.Interfaces;

public interface ICommand
{
    string Name { get; }
    string Description { get; }
    string Help { get; }

    void Execute(string[] args);
}