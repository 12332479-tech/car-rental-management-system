namespace CarRentalWinForms.Utilities;

// File handling requirement: saves simple local activity logs for the Windows Forms application.
public static class LocalFileService
{
    private static readonly string LogFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
    private static readonly string LogFile = Path.Combine(LogFolder, "winforms-log.txt");

    public static void WriteLog(string message)
    {
        try
        {
            Directory.CreateDirectory(LogFolder);
            File.AppendAllText(LogFile, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}{Environment.NewLine}");
        }
        catch
        {
            // Do not stop the GUI if the log file cannot be written.
        }
    }
}
