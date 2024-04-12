namespace LogFilter.Interfaces;

public interface IArgumentParser
{
    bool CanHandle(string argument);
    void Handle(string argument);
}