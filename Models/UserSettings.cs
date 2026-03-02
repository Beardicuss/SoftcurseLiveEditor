namespace HtmlLiveEditor.Models
{
    public class UserSettings
    {
        public string Theme { get; set; } = "vs-dark";
        public string Language { get; set; } = "html";
        public string LastWorkspacePath { get; set; } = string.Empty;
        public int WindowX { get; set; } = -1;
        public int WindowY { get; set; } = -1;
        public int WindowWidth { get; set; } = 1600;
        public int WindowHeight { get; set; } = 900;
        public int SplitterDistance { get; set; } = 800;
        public bool IsMaximized { get; set; } = false;
        public string LastOpenedFile { get; set; } = string.Empty;
    }
}
