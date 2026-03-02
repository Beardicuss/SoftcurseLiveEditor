using System;
using System.Threading.Tasks;

namespace HtmlLiveEditor.Services
{
    public interface IFileService
    {
        string? CurrentFilePath { get; }
        string? Open();
        bool Save(string content);
        bool SaveAs(string content);
        Task AutoSaveAsync(string content);   // was: void AutoSave
        bool IsLargeFile(string content);
        string? OpenWorkspace();
    }
}