namespace LogFilter.Helpers;

internal sealed class LogFileGenerator
{
    public void Generate(string filePath, int entriesCount)
    {
        var random = new Random();
        var lines = new List<string>();

        for (int i = 0; i < entriesCount; i++)
        {
            var ipAddress = new string[] {
            random.Next(0, 5).ToString(),
            random.Next(0, 5).ToString(),
            random.Next(0, 5).ToString(),
            random.Next(0, 5).ToString()
            };

            var date = new DateTime(
                random.Next(2002, 2024),
                random.Next(1, 13),
                random.Next(1, 29),
                random.Next(0, 24),
                random.Next(0, 60),
                random.Next(0, 60)
            , DateTimeKind.Utc);

            lines.Add($"{string.Join(".", ipAddress)} : {date:yyyy-MM-dd HH:mm:ss}");
        }

        File.WriteAllLines(filePath, lines);
    }
}