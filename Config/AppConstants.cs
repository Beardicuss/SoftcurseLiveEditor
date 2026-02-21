namespace HtmlLiveEditor.Config
{
    public static class AppConstants
    {
        // Timing
        public const int EditorDebounceMs = 300;
        public const int HoverDebounceMs = 80;
        public const int AutoSaveIntervalMs = 30_000;

        // WebView2 virtual host
        public const string VirtualHost = "appassets.local";
        public const string EditorUrl = "https://appassets.local/editor.html";
        public const string PreviewUrl = "https://appassets.local/preview.html";

        // File defaults
        public const string HtmlFilter = "HTML Files|*.html;*.htm|All Files|*.*";
        public const string SaveHtmlFilter = "HTML Files|*.html|All Files|*.*";
        public const string AutoSaveSuffix = ".autosave.html";
        public const string LastFileName = "last.html";

        // Large file threshold (bytes)
        public const long LargeFileThreshold = 1_048_576; // 1 MB
        public const int LargeFileDebounceMs = 1000;
    }
}
