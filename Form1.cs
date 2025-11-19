using Microsoft.Web.WebView2.Core;
using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Windows.Forms;

namespace HtmlLiveEditor
{
    public partial class Form1 : Form
    {
        private string? currentFilePath = null;
        private string lastHtml = string.Empty;
        private bool isEditorReady = false;
        private string? pendingEditorContent = null;

        public Form1()
        {
            InitializeComponent();

            this.Load += Form1_Load;
            this.openToolStripMenuItem.Click += OpenToolStripMenuItem_Click;
            this.saveToolStripMenuItem.Click += SaveToolStripMenuItem_Click;
            this.saveAsToolStripMenuItem.Click += SaveAsToolStripMenuItem_Click;
        }

        private async void Form1_Load(object? sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("=== Form1_Load started ===");

            await webViewEditor.EnsureCoreWebView2Async();
            await webViewPreview.EnsureCoreWebView2Async();

            System.Diagnostics.Debug.WriteLine("WebView2 initialized");

            string appPath = Path.Combine(Application.StartupPath, "App");
            System.Diagnostics.Debug.WriteLine($"App path: {appPath}");

            if (!Directory.Exists(appPath))
            {
                MessageBox.Show(
                    $"Critical Error: 'App' folder not found!\n\nPath: {appPath}\n\n" +
                    "Make sure the App folder exists and all files are set to 'Copy if newer'.",
                    "Missing Resources", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            var options = new CoreWebView2EnvironmentOptions();
            var env = await CoreWebView2Environment.CreateAsync(null, null, options);

            webViewEditor.CoreWebView2!.SetVirtualHostNameToFolderMapping(
                "appassets.local", appPath, CoreWebView2HostResourceAccessKind.Allow);
            webViewPreview.CoreWebView2!.SetVirtualHostNameToFolderMapping(
                "appassets.local", appPath, CoreWebView2HostResourceAccessKind.Allow);

            System.Diagnostics.Debug.WriteLine("Virtual host mapping set");

            // Attach event handlers BEFORE navigating
            webViewEditor.CoreWebView2.WebMessageReceived += EditorMessageReceived;
            webViewPreview.CoreWebView2.WebMessageReceived += PreviewMessageReceived;

            System.Diagnostics.Debug.WriteLine("Event handlers attached");

            webViewEditor.Source = new Uri("https://appassets.local/editor.html");
            webViewPreview.Source = new Uri("https://appassets.local/preview.html");

            System.Diagnostics.Debug.WriteLine("=== Form1_Load completed ===");
        }

        // ---------- Editor → Preview ----------
        private async void EditorMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            try
            {
                string json = e.WebMessageAsJson;
                if (string.IsNullOrWhiteSpace(json)) return;

                using JsonDocument doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                if (root.TryGetProperty("type", out var typeProp))
                {
                    string? type = typeProp.GetString();

                    if (type == "editorReady")
                    {
                        isEditorReady = true;
                        System.Diagnostics.Debug.WriteLine("Editor is now ready!");

                        if (pendingEditorContent != null)
                        {
                            System.Diagnostics.Debug.WriteLine("Sending pending content to editor...");
                            SetEditorContent(pendingEditorContent);
                            pendingEditorContent = null;
                        }
                        return;
                    }

                    if (type == "codeChanged")
                    {
                        string? html = root.GetProperty("text").GetString();
                        lastHtml = html ?? string.Empty;
                        webViewPreview.CoreWebView2?.NavigateToString(lastHtml);

                        string lastPath = Path.Combine(Application.StartupPath, "App", "last.html");
                        await File.WriteAllTextAsync(lastPath, lastHtml);
                        return;
                    }

                    if (type == "hoverLine")
                    {
                        string? snippet = root.GetProperty("snippet").GetString();
                        if (!string.IsNullOrWhiteSpace(snippet))
                        {
                            var msg = JsonSerializer.Serialize(new { type = "editorHover", htmlSnippet = snippet });
                            webViewPreview.CoreWebView2?.PostWebMessageAsString(msg);
                        }
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Editor error: " + ex.Message);
            }
        }

        // ---------- Preview → Editor (Hover / Highlight) ----------
        private void PreviewMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            try
            {
                string json = e.WebMessageAsJson;
                if (string.IsNullOrWhiteSpace(json)) return;

                using JsonDocument doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                if (!root.TryGetProperty("type", out var typeProp)) return;
                string? msgType = typeProp.GetString();

                string? snippet = root.TryGetProperty("htmlSnippet", out var prop)
                    ? prop.GetString()
                    : null;

                if (string.IsNullOrEmpty(snippet)) return;

                // Escape for JS
                string escaped = snippet
                    .Replace("\\", "\\\\")
                    .Replace("`", "\\`")
                    .Replace("$", "\\$");

                string js = $@"
            (function() {{
                if (!window.editor) return;
                const text = window.editor.getValue();
                const lines = text.split('\n');
                let targetLine = -1;
                const token = `{escaped}`.trim().split(' ')[0];

                for (let i = 0; i < lines.length; i++) {{
                    if (lines[i].includes(token)) {{
                        targetLine = i + 1;
                        break;
                    }}
                }}

                if (targetLine <= 0) return;

                if ('{msgType}' === 'hover') {{
                    if (window._hoverDeco) window.editor.deltaDecorations(window._hoverDeco, []);
                    window._hoverDeco = window.editor.deltaDecorations([], [{{
                        range: new monaco.Range(targetLine, 1, targetLine, 1),
                        options: {{ isWholeLine: true, className: 'hover-line' }}
                    }}]);
                }} else if ('{msgType}' === 'unhover') {{
                    if (window._hoverDeco) window.editor.deltaDecorations(window._hoverDeco, []);
                }} else if ('{msgType}' === 'highlight') {{
                    if (window._lastDeco) window.editor.deltaDecorations(window._lastDeco, []);
                    window._lastDeco = window.editor.deltaDecorations([], [{{
                        range: new monaco.Range(targetLine, 1, targetLine, 1),
                        options: {{ isWholeLine: true, className: 'highlighted-line' }}
                    }}]);
                    window.editor.revealLineInCenter(targetLine);
                }}
            }})();";

                webViewEditor.CoreWebView2?.ExecuteScriptAsync(js);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Preview→Editor error: " + ex.Message);
            }
        }

        // ---------- File Operations ----------
        private void OpenToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog
            {
                Filter = "HTML Files|*.html;*.htm|All Files|*.*",
                Title = "Open HTML File"
            };

            if (dialog.ShowDialog() != DialogResult.OK) return;

            currentFilePath = dialog.FileName;
            string content = File.ReadAllText(currentFilePath);

            lastHtml = content;
            webViewPreview.CoreWebView2?.NavigateToString(content);

            // Send to editor ONLY when it's ready
            if (isEditorReady)
            {
                SetEditorContent(content);
            }
            else
            {
                pendingEditorContent = content; // will be sent when ready
            }
        }

        private void SaveToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lastHtml)) return;

            if (currentFilePath is null)
            {
                SaveAsToolStripMenuItem_Click(sender, e);
                return;
            }

            File.WriteAllText(currentFilePath, lastHtml);
        }

        private void SaveAsToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            using var dialog = new SaveFileDialog
            {
                Filter = "HTML Files|*.html|All Files|*.*",
                Title = "Save HTML File"
            };

            if (dialog.ShowDialog() != DialogResult.OK) return;

            currentFilePath = dialog.FileName;
            File.WriteAllText(currentFilePath, lastHtml);
        }

        // ---------- Robust Editor Update ----------
        private void SetEditorContent(string html)
        {
            if (webViewEditor.CoreWebView2 == null)
            {
                System.Diagnostics.Debug.WriteLine("ERROR: webViewEditor.CoreWebView2 is null!");
                return;
            }

            System.Diagnostics.Debug.WriteLine($"SetEditorContent called with {html.Length} characters");

            // Convert to Base64 to avoid all escaping issues
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(html);
            string base64 = Convert.ToBase64String(bytes);

            string js = $@"
                (function() {{
                    console.log('SetEditorContent script executing...');
                    let attempts = 0;
                    const maxAttempts = 100;
                    
                    function trySet() {{
                        attempts++;
                        console.log('Attempt ' + attempts + ' to set editor content');
                        
                        if (window.editor && typeof window.editor.setValue === 'function') {{
                            try {{
                                // Decode Base64
                                const base64 = '{base64}';
                                const decoded = atob(base64);
                                const content = decodeURIComponent(escape(decoded));
                                
                                window.editor.setValue(content);
                                console.log('✓✓✓ SUCCESS: Editor content set! Length: ' + content.length);
                                return true;
                            }} catch(e) {{
                                console.error('ERROR setting editor value:', e);
                                return false;
                            }}
                        }} else {{
                            console.warn('Editor not ready. window.editor exists: ' + !!window.editor);
                        }}
                        
                        if (attempts < maxAttempts) {{
                            setTimeout(trySet, 100);
                        }} else {{
                            console.error('FAILED: Could not set editor content after ' + maxAttempts + ' attempts');
                        }}
                        return false;
                    }}
                    
                    trySet();
                }})();";

            System.Diagnostics.Debug.WriteLine("Executing JavaScript to set editor content...");
            webViewEditor.CoreWebView2.ExecuteScriptAsync(js);
        }
    }
}