using System;
using System.Diagnostics;

namespace HtmlLiveEditor.Services
{
    public sealed class AppLogger : IAppLogger
    {
        public void Info(string message)
        {
            var formatted = $"[INFO]  {DateTime.Now:HH:mm:ss.fff} | {message}";
            Debug.WriteLine(formatted);
        }

        public void Warn(string message)
        {
            var formatted = $"[WARN]  {DateTime.Now:HH:mm:ss.fff} | {message}";
            Debug.WriteLine(formatted);
        }

        public void Error(string message, Exception? ex = null)
        {
            var formatted = $"[ERROR] {DateTime.Now:HH:mm:ss.fff} | {message}";
            if (ex != null)
                formatted += $"\n  → {ex.GetType().Name}: {ex.Message}";
            Debug.WriteLine(formatted);
        }
    }
}
