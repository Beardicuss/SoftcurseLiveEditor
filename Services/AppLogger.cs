using System;
using System.Diagnostics;

namespace HtmlLiveEditor.Services
{
    public sealed class AppLogger : IAppLogger
    {
        private readonly string _logFilePath;

        public AppLogger()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var dir = System.IO.Path.Combine(appData, "SoftcurseLiveScriptor");
            System.IO.Directory.CreateDirectory(dir);
            _logFilePath = System.IO.Path.Combine(dir, "app.log");
        }

        private void WriteLog(string formattedMessage)
        {
            Debug.WriteLine(formattedMessage);
            try
            {
                System.IO.File.AppendAllText(_logFilePath, formattedMessage + Environment.NewLine);
            }
            catch
            {
                // Suppress if file locked
            }
        }

        public void Info(string message)
        {
            var formatted = $"[INFO]  {{DateTime.Now:HH:mm:ss.fff}} | {message}";
            WriteLog(formatted);
        }

        public void Warn(string message)
        {
            var formatted = $"[WARN]  {{DateTime.Now:HH:mm:ss.fff}} | {message}";
            WriteLog(formatted);
        }

        public void Error(string message, Exception? ex = null)
        {
            var formatted = $"[ERROR] {{DateTime.Now:HH:mm:ss.fff}} | {message}";
            if (ex != null)
                formatted += $"\n  → {{ex.GetType().Name}}: {{ex.Message}}";
            WriteLog(formatted);
        }
    }
}
