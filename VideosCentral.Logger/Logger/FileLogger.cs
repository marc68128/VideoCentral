using System;
using System.IO;
using VideosCentral.Logger.Interface;

namespace VideosCentral.Logger
{
    public class FileLogger : ILogger
    {
        private readonly string _logFolderPath;

        public FileLogger(string logFolderPath)
        {
            _logFolderPath = logFolderPath;
            if (!Directory.Exists(_logFolderPath))
                Directory.CreateDirectory(_logFolderPath);
        }

        public void LogError(Exception e, string message = null)
        {
            using (var sw = new StreamWriter(Path.Combine(_logFolderPath, "ErrorLogs.txt"), true))
            {
                sw.WriteLineAsync($"[{DateTime.Now:G}] {message ?? e.Message}");
                sw.WriteLineAsync(e.StackTrace);
                #warning Improve exception logging
                sw.Flush();
            }
        }

        public void LogWarning(string message)
        {
            LogMessage(message, Path.Combine(_logFolderPath, "WarningLogs.txt"));
        }

        public void LogInfo(string message)
        {
            LogMessage(message, Path.Combine(_logFolderPath, "InfoLogs.txt"));
        }

        private void LogMessage(string message, string filePath)
        {
            using (var sw = new StreamWriter(filePath, true))
            {
                sw.WriteLineAsync($"[{DateTime.Now:G}] {message}");
                sw.Flush();
            }
        }
    }
}
