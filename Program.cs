using LogFilter.ArgumentParsers;
using LogFilter.Helpers;
using LogFilter.Services;


// var generator = new LogFileGenerator();
// generator.Generate("GeneratedLog.txt", 1000);

Run(args);

static void Run(string[] args)
{
    var builder = new FilterParametersBuilder();

    var handlers = new List<ArgumentParser>()
    {
        new ("--file-log", (value) => builder.SetLogFilePath(value)),
        new ("--file-output", (value) => builder.SetOutputFilePath(value)),
        new ("--address-start", (value) => builder.SetAddressStart(value)),
        new ("--address-mask", (value) => builder.SetAddressMask(value)),
        new ("--time-start", (value) => builder.SetTimeStart(value)),
        new ("--time-end", (value) => builder.SetTimeEnd(value))
    };

    try
    {
        foreach (var arg in args)
        {
            var handler = handlers.Find(x => x.CanHandle(arg));

            if (handler is null)
            {
                throw new ArgumentException($"Cant handle the {arg} argument");
            }
            else
            {
                handler.Handle(arg);
            }
        }

        var parameters = builder.Build();

        var executor = new LogFilterExecutor(parameters);

        executor.Execute();

        Console.WriteLine("Успешно");
    }
    catch (Exception exception)
    {
        Console.WriteLine($"Ошибка: {exception.Message}");
        Console.WriteLine("Запустите программу с правильными аргументами.");
    }
}
