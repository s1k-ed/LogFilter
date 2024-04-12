using LogFilter.Commands;
using LogFilter.Exceptions;
using LogFilter.Interfaces;

var configurationPath = "settings.json";

var commands = new Dictionary<string, ICommand>()
{
    {"filter", new FilterCommand(configurationPath)},
    {"generate", new GenerateDataCommand()},
    {"exit", new ExitCommand()},
};

commands.Add("help", new HelpCommand(commands.Values));

while (true)
{
    Console.Clear();
    Console.WriteLine("Список доступных комманд:");
    foreach (var commandKey in commands.Keys)
    {
        Console.Write($"{commandKey} - ");
    }
    Console.WriteLine("\n\nВведите команду:");
    var command = (Console.ReadLine() ?? "").Split(' ');
    var key = command[0];
    var arguments = command.Skip(1).ToArray();

    try
    {
        if (!commands.ContainsKey(key))
            throw new KeyNotFoundException(key);

        commands[key].Execute(arguments);

    }
    catch (Exception exception)
    {
        HandleException(exception);
    }
    finally
    {
        Console.WriteLine();
        Console.WriteLine("Нажмите любую клавишу, чтобы продолжить");
        Console.ReadKey();
    }
}

static void HandleException(Exception exception)
{
    var message = exception switch
    {
        KeyNotFoundException => "Комманда не найдена.",
        ParseException => "Ошибка чтения значения",
        UnknownArgumentException => "Неизвестный аргумент",
        NoValueProvidedException => "Отсутствует значение",
        IsRequiredException => "Отсутствует обязательный ключ",
        ArgumentException => "Ошибка чтения параметров",
        _ => "Необработанная ошибка"
    };

    Console.WriteLine($"Ошибка: {message}");
    Console.WriteLine(exception.Message);
}