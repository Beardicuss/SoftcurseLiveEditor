using System;
using System.IO;
using System.Text.Json;
using HtmlLiveEditor.Models;

namespace HtmlLiveEditor.Services
{
    public sealed class SettingsManager : ISettingsManager
    {
        private readonly IAppLogger _log;
        private readonly string _settingsFilePath;

        public SettingsManager(IAppLogger log)
        {
            _log = log;
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var dir = Path.Combine(appData, "SoftcurseLiveScriptor");
            Directory.CreateDirectory(dir);
            _settingsFilePath = Path.Combine(dir, "settings.json");
        }

        public UserSettings Load()
        {
            if (!File.Exists(_settingsFilePath))
            {
                _log.Info("Settings file not found, creating new defaults.");
                return new UserSettings();
            }

            try
            {
                var json = File.ReadAllText(_settingsFilePath);
                var settings = JsonSerializer.Deserialize<UserSettings>(json);
                _log.Info("Settings loaded successfully.");
                return settings ?? new UserSettings();
            }
            catch (Exception ex)
            {
                _log.Error("Failed to load settings file, using defaults.", ex);
                return new UserSettings();
            }
        }

        public void Save(UserSettings settings)
        {
            try
            {
                var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_settingsFilePath, json);
                _log.Info("Settings saved successfully.");
            }
            catch (Exception ex)
            {
                _log.Error("Failed to save settings.", ex);
            }
        }
    }
}
