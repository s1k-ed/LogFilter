namespace LogFilter.Services;

public sealed class LogFileWriter(string filePath)
{
    private readonly string _filePath = filePath;

    public void Write(IEnumerable<string> entries)
    {
        File.WriteAllLines(_filePath, entries);
    }
}