using HtmlLiveEditor.Models;

namespace HtmlLiveEditor.Services
{
    public interface ISettingsManager
    {
        UserSettings Load();
        void Save(UserSettings settings);
    }
}
