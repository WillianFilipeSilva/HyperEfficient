namespace HyperEfficient.Infrastructure.Logger
{
    public static class Logger
    {
        private static readonly string LogDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "Log");

        public static async Task LogError(string message)
        {
            await WriteLog($"LogError-{DateTime.Now:yyyy-MM-dd}.txt", message);
        }

        public static async Task LogInfo(string message)
        {
            await WriteLog($"LogInfo-{DateTime.Now:yyyy-MM-dd}.txt", message);
        }

        private static async Task WriteLog(string fileName, string message)
        {
            try
            {
                Directory.CreateDirectory(LogDirectory);
                string filePath = Path.Combine(LogDirectory, fileName);
                string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}{Environment.NewLine}";
                await File.AppendAllTextAsync(filePath, logEntry);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao registrar log: {ex.Message}");
            }
        }
    }
} 