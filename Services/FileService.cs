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

            // FIX #5: Guard file size BEFORE reading into memory
            var info = new FileInfo(dialog.FileName);
            if (info.Length > AppConstants.HardFileSizeLimit)
            {
                MessageBox.Show(
                    $"File is too large to open safely ({info.Length / 1_048_576.0:F1} MB).\n" +
                    $"Maximum is {AppConstants.HardFileSizeLimit / 1_048_576} MB.",
                    "File Too Large", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            CurrentFilePath = dialog.FileName;
            var content = File.ReadAllText(CurrentFilePath, Encoding.UTF8);
            _log.Info($"Opened: {CurrentFilePath} ({content.Length} chars)");
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

        // FIX #3: async void → async Task (exceptions are no longer swallowed silently)
        // FIX #2: Writes to LocalApplicationData, not StartupPath (read-only after install)
        public async Task AutoSaveAsync(string content)
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var dir = Path.Combine(appData, "SoftcurseLiveScriptor");
            Directory.CreateDirectory(dir);
            var path = Path.Combine(dir, AppConstants.LastFileName);
            await File.WriteAllTextAsync(path, content, Encoding.UTF8);
            _log.Info($"Auto-saved to: {path}");
        }

        public bool IsLargeFile(string content)
        {
            return Encoding.UTF8.GetByteCount(content) > AppConstants.LargeFileThreshold;
        }

        public string? OpenWorkspace()
        {
            using var dialog = new FolderBrowserDialog
            {
                Description = "Select Workspace Folder",
                UseDescriptionForTitle = true,
                ShowNewFolderButton = true
            };

            if (dialog.ShowDialog() != DialogResult.OK) return null;

            _log.Info($"Workspace opened: {dialog.SelectedPath}");
            return dialog.SelectedPath;
        }
    }
}