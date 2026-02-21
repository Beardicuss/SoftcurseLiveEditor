using System;
using System.Windows.Forms;
using HtmlLiveEditor.Services;

namespace HtmlLiveEditor
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Lightweight DI — create services and inject into form
            var log = new AppLogger();
            var fileService = new FileService(log);
            var editorBridge = new EditorBridge(log);
            var previewBridge = new PreviewBridge(log);

            Application.Run(new Form1(log, fileService, editorBridge, previewBridge));
        }
    }
}