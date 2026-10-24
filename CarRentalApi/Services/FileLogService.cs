namespace CarRentalApi.Services;

// File handling requirement: writes API activity/errors to a text file.
public class FileLogService : IFileLogService
{
    private readonly string _logFilePath;

    public FileLogService(IWebHostEnvironment environment)
    {
        var folder = Path.Combine(environment.ContentRootPath, "Logs");
        Directory.CreateDirectory(folder);
        _logFilePath = Path.Combine(folder, "car-rental-log.txt");
    }

    public async Task WriteAsync(string message)
    {
        var line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}{Environment.NewLine}";
        await File.AppendAllTextAsync(_logFilePath, line);
    }
}
