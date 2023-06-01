namespace Common
{
    public class MyLogger
    {
        private static readonly object lockObject = new();

        private static string GetFileName()
        {
            return $"log{DateTime.UtcNow:yyyyMMdd}.txt";
        }

        public static void Debug(string message)
        {
            try
            {
                lock (lockObject)
                {
                    var loggerDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, LoggerConstants.LoggerFolder);
                    if (!Directory.Exists(loggerDirectory))
                    {
                        Directory.CreateDirectory(loggerDirectory);
                    }
                    var filePath = Path.Combine(loggerDirectory, GetFileName());
                    using var writer = new StreamWriter(filePath, true);
                    var prefixTime = DateTime.UtcNow.ToString("HH:mm:ss");
                    writer.WriteLine($"Debug {prefixTime} {message}");
                    writer.Flush();
                    writer.Close();
                }
            }
            catch { }
        }

        public static void Error(string message)
        {
            try
            {
                lock (lockObject)
                {
                    var loggerDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, LoggerConstants.LoggerFolder);
                    if (!Directory.Exists(loggerDirectory))
                    {
                        Directory.CreateDirectory(loggerDirectory);
                    }
                    var filePath = Path.Combine(loggerDirectory, GetFileName());
                    using var writer = new StreamWriter(filePath, true);
                    var prefixTime = DateTime.UtcNow.ToString("HH:mm:ss");
                    writer.WriteLine($"Error {prefixTime} {message}");
                    writer.Flush();
                    writer.Close();
                }
            }
            catch { }
        }

        public static void Info(string message)
        {
            try
            {
                lock (lockObject)
                {
                    var loggerDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, LoggerConstants.LoggerFolder);
                    if (!Directory.Exists(loggerDirectory))
                    {
                        Directory.CreateDirectory(loggerDirectory);
                    }
                    var filePath = Path.Combine(loggerDirectory, GetFileName());
                    using var writer = new StreamWriter(filePath, true);
                    var prefixTime = DateTime.UtcNow.ToString("HH:mm:ss");
                    writer.WriteLine($"Info {prefixTime} {message}");
                    writer.Flush();
                    writer.Close();
                }
            }
            catch { }
        }

        public static void Warn(string message)
        {
            try
            {
                lock (lockObject)
                {
                    var loggerDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, LoggerConstants.LoggerFolder);
                    if (!Directory.Exists(loggerDirectory))
                    {
                        Directory.CreateDirectory(loggerDirectory);
                    }
                    var filePath = Path.Combine(loggerDirectory, GetFileName());
                    using var writer = new StreamWriter(filePath, true);
                    var prefixTime = DateTime.UtcNow.ToString("HH:mm:ss");
                    writer.WriteLine($"Warn {prefixTime} {message}");
                    writer.Flush();
                    writer.Close();
                }
            }
            catch { }
        }
    }
}