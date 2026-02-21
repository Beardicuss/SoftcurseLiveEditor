using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using HtmlLiveEditor.Config;
using HtmlLiveEditor.Services;

namespace HtmlLiveEditor
{
    public partial class Form1 : Form
    {
        private readonly IAppLogger _log;
        private readonly IFileService _fileService;
        private readonly EditorBridge _editor;
        private readonly PreviewBridge _preview;

        private string _lastHtml = string.Empty;
        private System.Windows.Forms.Timer? _autoSaveTimer;
        private string _currentLanguage = "html";

        public Form1(IAppLogger log, IFileService fileService, EditorBridge editor, PreviewBridge preview)
        {
            _log = log;
            _fileService = fileService;
            _editor = editor;
            _preview = preview;

            InitializeComponent();

            // ── File menu ──
            this.Load += Form1_Load;
            this.openToolStripMenuItem.Click += OnOpen;
            this.saveToolStripMenuItem.Click += OnSave;
            this.saveAsToolStripMenuItem.Click += OnSaveAs;

            // ── Theme menu ──
            this.lightThemeToolStripMenuItem.Click += (_, _) => _editor.SetTheme("vs");
            this.darkThemeToolStripMenuItem.Click += (_, _) => _editor.SetTheme("vs-dark");

            // ── Language menu ──
            this.langHtml.Click += (_, _) => SetLanguage("html");
            this.langCss.Click += (_, _) => SetLanguage("css");
            this.langJs.Click += (_, _) => SetLanguage("javascript");

            // ── Refresh preview ──
            this.refreshPreviewMenuItem.Click += (_, _) => RefreshPreview();

            // ── Snippets ──
            this.snippetBoilerplate.Click += (_, _) => _editor.InsertSnippet(Snippets.Boilerplate);
            this.snippetFlexbox.Click += (_, _) => _editor.InsertSnippet(Snippets.Flexbox);
            this.snippetGrid.Click += (_, _) => _editor.InsertSnippet(Snippets.Grid);
            this.snippetForm.Click += (_, _) => _editor.InsertSnippet(Snippets.Form);
            this.snippetTable.Click += (_, _) => _editor.InsertSnippet(Snippets.Table);
        }

        // ═══════════════════════════════════════════
        //  Initialization
        // ═══════════════════════════════════════════

        private async void Form1_Load(object? sender, EventArgs e)
        {
            _log.Info("=== Form1_Load started ===");
            SetStatus("Initializing...");

            await webViewEditor.EnsureCoreWebView2Async();
            await webViewPreview.EnsureCoreWebView2Async();

            _log.Info("WebView2 initialized");

            string appPath = Path.Combine(Application.StartupPath, "App");
            if (!Directory.Exists(appPath))
            {
                MessageBox.Show(
                    $"Critical Error: 'App' folder not found!\n\nPath: {appPath}\n\n" +
                    "Make sure the App folder exists and all files are set to 'Copy if newer'.",
                    "Missing Resources", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            webViewEditor.CoreWebView2!.SetVirtualHostNameToFolderMapping(
                AppConstants.VirtualHost, appPath, CoreWebView2HostResourceAccessKind.Allow);
            webViewPreview.CoreWebView2!.SetVirtualHostNameToFolderMapping(
                AppConstants.VirtualHost, appPath, CoreWebView2HostResourceAccessKind.Allow);

            // Attach bridges
            _editor.Attach(webViewEditor);
            _preview.Attach(webViewPreview);

            // Wire events
            _editor.EditorReady += OnEditorReady;
            _editor.CodeChanged += OnCodeChanged;
            _editor.HoverLine += OnEditorHover;
            _preview.ElementHovered += OnPreviewHover;
            _preview.ElementUnhovered += OnPreviewUnhover;
            _preview.ElementClicked += OnPreviewClick;

            // Navigate
            webViewEditor.Source = new Uri(AppConstants.EditorUrl);
            webViewPreview.Source = new Uri(AppConstants.PreviewUrl);

            // Start auto-save timer
            _autoSaveTimer = new System.Windows.Forms.Timer { Interval = AppConstants.AutoSaveIntervalMs };
            _autoSaveTimer.Tick += (_, _) =>
            {
                if (!string.IsNullOrEmpty(_lastHtml))
                {
                    _fileService.AutoSave(_lastHtml);
                    SetStatus("Auto-saved");
                }
            };
            _autoSaveTimer.Start();

            _log.Info("=== Form1_Load completed ===");
        }

        private void OnEditorReady()
        {
            SetStatus("Editor ready — LINK: STABLE");
        }

        // ═══════════════════════════════════════════
        //  Editor → Preview
        // ═══════════════════════════════════════════

        private void OnCodeChanged(string html)
        {
            _lastHtml = html;

            if (_currentLanguage == "html")
                _preview.UpdateContent(html);

            SetStatus($"Editing · {html.Length} chars");
        }

        private void OnEditorHover(string snippet)
        {
            if (!string.IsNullOrWhiteSpace(snippet))
                _preview.SendEditorHover(snippet);
        }

        // ═══════════════════════════════════════════
        //  Preview → Editor
        // ═══════════════════════════════════════════

        private void OnPreviewHover(string snippet)
        {
            _editor.FindAndHighlight(snippet, "hover-line");
        }

        private void OnPreviewUnhover()
        {
            _editor.ClearDecorations("_hoverDeco");
        }

        private void OnPreviewClick(string snippet)
        {
            _editor.FindAndHighlight(snippet, "highlighted-line");
            SetStatus("Element highlighted in editor");
        }

        // ═══════════════════════════════════════════
        //  File Operations
        // ═══════════════════════════════════════════

        private void OnOpen(object? sender, EventArgs e)
        {
            var content = _fileService.Open();
            if (content == null) return;

            _lastHtml = content;
            _preview.UpdateContent(content);
            _editor.SetContent(content);

            this.Text = $"Softcurse LiveScriptor — {Path.GetFileName(_fileService.CurrentFilePath)}";
            SetStatus($"Opened: {Path.GetFileName(_fileService.CurrentFilePath)}");

            if (_fileService.IsLargeFile(content))
            {
                SetStatus("⚠ Large file — preview may be slow");
                _log.Warn($"Large file opened: {content.Length} chars");
            }
        }

        private void OnSave(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_lastHtml)) return;

            if (_fileService.Save(_lastHtml))
                SetStatus($"Saved: {Path.GetFileName(_fileService.CurrentFilePath)}");
        }

        private void OnSaveAs(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_lastHtml)) return;

            if (_fileService.SaveAs(_lastHtml))
            {
                this.Text = $"Softcurse LiveScriptor — {Path.GetFileName(_fileService.CurrentFilePath)}";
                SetStatus($"Saved As: {Path.GetFileName(_fileService.CurrentFilePath)}");
            }
        }

        // ═══════════════════════════════════════════
        //  Language / Theme / Refresh
        // ═══════════════════════════════════════════

        private void SetLanguage(string language)
        {
            _currentLanguage = language;
            _editor.SetLanguage(language);
            SetStatus($"Language: {language.ToUpperInvariant()}");
        }

        private void RefreshPreview()
        {
            if (_currentLanguage == "html" && !string.IsNullOrEmpty(_lastHtml))
            {
                _preview.UpdateContent(_lastHtml);
                SetStatus("Preview refreshed");
            }
        }

        // ═══════════════════════════════════════════
        //  Status bar
        // ═══════════════════════════════════════════

        private void SetStatus(string text)
        {
            if (statusLabel != null)
                statusLabel.Text = text;
        }
    }

    // ═══════════════════════════════════════════
    //  Snippet Templates
    // ═══════════════════════════════════════════

    internal static class Snippets
    {
        public const string Boilerplate = @"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Document</title>
    <style>
        body { font-family: system-ui, sans-serif; margin: 2rem; }
    </style>
</head>
<body>
    <h1>Hello World</h1>
    <p>Start building here...</p>
</body>
</html>";

        public const string Flexbox = @"<div style=""display: flex; gap: 1rem; align-items: center; justify-content: center; padding: 2rem;"">
    <div style=""flex: 1; padding: 1rem; background: #f0f0f0; border-radius: 8px;"">Column 1</div>
    <div style=""flex: 1; padding: 1rem; background: #e0e0e0; border-radius: 8px;"">Column 2</div>
    <div style=""flex: 1; padding: 1rem; background: #d0d0d0; border-radius: 8px;"">Column 3</div>
</div>";

        public const string Grid = @"<div style=""display: grid; grid-template-columns: repeat(3, 1fr); gap: 1rem; padding: 2rem;"">
    <div style=""padding: 1rem; background: #f0f0f0; border-radius: 8px;"">Cell 1</div>
    <div style=""padding: 1rem; background: #e0e0e0; border-radius: 8px;"">Cell 2</div>
    <div style=""padding: 1rem; background: #d0d0d0; border-radius: 8px;"">Cell 3</div>
    <div style=""padding: 1rem; background: #c0c0c0; border-radius: 8px;"">Cell 4</div>
    <div style=""padding: 1rem; background: #b0b0b0; border-radius: 8px;"">Cell 5</div>
    <div style=""padding: 1rem; background: #a0a0a0; border-radius: 8px;"">Cell 6</div>
</div>";

        public const string Form = @"<form style=""max-width: 400px; margin: 2rem auto; font-family: system-ui;"">
    <div style=""margin-bottom: 1rem;"">
        <label for=""name"" style=""display: block; margin-bottom: 0.25rem; font-weight: 600;"">Name</label>
        <input type=""text"" id=""name"" placeholder=""Enter your name"" style=""width: 100%; padding: 0.5rem; border: 1px solid #ccc; border-radius: 4px;"" />
    </div>
    <div style=""margin-bottom: 1rem;"">
        <label for=""email"" style=""display: block; margin-bottom: 0.25rem; font-weight: 600;"">Email</label>
        <input type=""email"" id=""email"" placeholder=""Enter your email"" style=""width: 100%; padding: 0.5rem; border: 1px solid #ccc; border-radius: 4px;"" />
    </div>
    <div style=""margin-bottom: 1rem;"">
        <label for=""message"" style=""display: block; margin-bottom: 0.25rem; font-weight: 600;"">Message</label>
        <textarea id=""message"" rows=""4"" placeholder=""Your message..."" style=""width: 100%; padding: 0.5rem; border: 1px solid #ccc; border-radius: 4px;""></textarea>
    </div>
    <button type=""submit"" style=""padding: 0.5rem 1.5rem; background: #007bff; color: white; border: none; border-radius: 4px; cursor: pointer;"">Submit</button>
</form>";

        public const string Table = @"<table style=""border-collapse: collapse; width: 100%; font-family: system-ui;"">
    <thead>
        <tr style=""background: #f8f9fa;"">
            <th style=""padding: 0.75rem; border: 1px solid #dee2e6; text-align: left;"">Name</th>
            <th style=""padding: 0.75rem; border: 1px solid #dee2e6; text-align: left;"">Email</th>
            <th style=""padding: 0.75rem; border: 1px solid #dee2e6; text-align: left;"">Role</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td style=""padding: 0.75rem; border: 1px solid #dee2e6;"">Alice</td>
            <td style=""padding: 0.75rem; border: 1px solid #dee2e6;"">alice@example.com</td>
            <td style=""padding: 0.75rem; border: 1px solid #dee2e6;"">Admin</td>
        </tr>
        <tr style=""background: #f8f9fa;"">
            <td style=""padding: 0.75rem; border: 1px solid #dee2e6;"">Bob</td>
            <td style=""padding: 0.75rem; border: 1px solid #dee2e6;"">bob@example.com</td>
            <td style=""padding: 0.75rem; border: 1px solid #dee2e6;"">User</td>
        </tr>
    </tbody>
</table>";
    }
}