using System;

namespace HtmlLiveEditor.Services
{
    public interface IFileService
    {
        string? CurrentFilePath { get; }

        /// <summary>Shows open dialog, returns file content or null if cancelled.</summary>
        string? Open();

        /// <summary>Saves to current path. Falls back to SaveAs if no path set.</summary>
        bool Save(string content);

        /// <summary>Shows save dialog, saves content. Returns true if saved.</summary>
        bool SaveAs(string content);

        /// <summary>Writes content to the auto-save location.</summary>
        void AutoSave(string content);

        /// <summary>Checks if the file exceeds the large-file threshold.</summary>
        bool IsLargeFile(string content);
    }
}
