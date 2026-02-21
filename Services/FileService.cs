using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using HtmlLiveEditor.Config;

namespace HtmlLiveEditor.Services
{
    public sealed class FileService : IFileService
    {
        private readonly IAppLogger _log;

        public FileService(IAppLogger log)
        {
            _log = log;
        }

        public string? CurrentFilePath { get; private set; }

        public string? Open()
        {
            using var dialog = new OpenFileDialog
            {
                Filter = AppConstants.HtmlFilter,
                Title = "Open HTML File"
            };

            if (dialog.ShowDialog() != DialogResult.OK) return null;

            CurrentFilePath = dialog.FileName;
            var content = File.ReadAllText(CurrentFilePath, Encoding.UTF8);
            _log.Info($"Opened file: {CurrentFilePath} ({content.Length} chars)");
            return content;
        }

        public bool Save(string content)
        {
            if (CurrentFilePath is null)
                return SaveAs(content);

            File.WriteAllText(CurrentFilePath, content, Encoding.UTF8);
            _log.Info($"Saved: {CurrentFilePath}");
            return true;
        }

        public bool SaveAs(string content)
        {
            using var dialog = new SaveFileDialog
            {
                Filter = AppConstants.SaveHtmlFilter,
                Title = "Save HTML File"
            };

            if (dialog.ShowDialog() != DialogResult.OK) return false;

            CurrentFilePath = dialog.FileName;
            File.WriteAllText(CurrentFilePath, content, Encoding.UTF8);
            _log.Info($"Saved As: {CurrentFilePath}");
            return true;
        }

        public async void AutoSave(string content)
        {
            try
            {
                var path = Path.Combine(Application.StartupPath, "App", AppConstants.LastFileName);
                await File.WriteAllTextAsync(path, content, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                _log.Error("Auto-save failed", ex);
            }
        }

        public bool IsLargeFile(string content)
        {
            return Encoding.UTF8.GetByteCount(content) > AppConstants.LargeFileThreshold;
        }
    }
}
