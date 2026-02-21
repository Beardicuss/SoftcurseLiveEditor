using System;
using System.Text.Json;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;

namespace HtmlLiveEditor.Services
{
    public sealed class EditorBridge : IEditorBridge
    {
        private readonly IAppLogger _log;
        private WebView2? _webView;
        private string? _pendingContent;

        public EditorBridge(IAppLogger log)
        {
            _log = log;
        }

        public bool IsReady { get; private set; }

        public event Action? EditorReady;
        public event Action<string>? CodeChanged;
        public event Action<string>? HoverLine;

        public void Attach(WebView2 webView)
        {
            _webView = webView;
            _webView.CoreWebView2.WebMessageReceived += OnMessageReceived;
            _log.Info("EditorBridge attached");
        }

        public void SetContent(string html)
        {
            if (_webView?.CoreWebView2 == null)
            {
                _log.Error("EditorBridge.SetContent: CoreWebView2 is null");
                return;
            }

            if (!IsReady)
            {
                _log.Info($"Editor not ready yet, queuing {html.Length} chars");
                _pendingContent = html;
                return;
            }

            _log.Info($"Sending setCode to editor ({html.Length} chars)");
            var message = JsonSerializer.Serialize(new { type = "setCode", text = html });
            _webView.CoreWebView2.PostWebMessageAsJson(message);
        }

        public void SetTheme(string theme)
        {
            if (_webView?.CoreWebView2 == null) return;
            var message = JsonSerializer.Serialize(new { type = "setTheme", theme });
            _webView.CoreWebView2.PostWebMessageAsJson(message);
            _log.Info($"Theme set to: {theme}");
        }

        public void SetLanguage(string language)
        {
            if (_webView?.CoreWebView2 == null) return;
            var message = JsonSerializer.Serialize(new { type = "setLanguage", language });
            _webView.CoreWebView2.PostWebMessageAsJson(message);
            _log.Info($"Language set to: {language}");
        }

        public void InsertSnippet(string snippet)
        {
            if (_webView?.CoreWebView2 == null) return;
            var message = JsonSerializer.Serialize(new { type = "insertSnippet", text = snippet });
            _webView.CoreWebView2.PostWebMessageAsJson(message);
            _log.Info("Snippet inserted");
        }

        public void HighlightLine(int lineNumber, string cssClass)
        {
            if (_webView?.CoreWebView2 == null || !IsReady) return;

            string decoVar = cssClass == "hover-line" ? "_hoverDeco" : "_lastDeco";
            string js = $@"
                (function() {{
                    if (!window.editor) return;
                    if (window.{decoVar}) window.editor.deltaDecorations(window.{decoVar}, []);
                    window.{decoVar} = window.editor.deltaDecorations([], [{{
                        range: new monaco.Range({lineNumber}, 1, {lineNumber}, 1),
                        options: {{ isWholeLine: true, className: '{cssClass}' }}
                    }}]);
                    {(cssClass == "highlighted-line" ? $"window.editor.revealLineInCenter({lineNumber});" : "")}
                }})();";

            _webView.CoreWebView2.ExecuteScriptAsync(js);
        }

        public void ClearDecorations(string decoId)
        {
            if (_webView?.CoreWebView2 == null || !IsReady) return;
            string js = $@"(function() {{
                if (window.editor && window.{decoId})
                    window.editor.deltaDecorations(window.{decoId}, []);
            }})();";
            _webView.CoreWebView2.ExecuteScriptAsync(js);
        }

        // ── Message handler ──
        private void OnMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            try
            {
                string raw = e.WebMessageAsJson;
                if (string.IsNullOrWhiteSpace(raw)) return;

                using var doc = JsonDocument.Parse(raw);
                var root = doc.RootElement;

                if (!root.TryGetProperty("type", out var typeProp)) return;
                string? type = typeProp.GetString();

                switch (type)
                {
                    case "editorReady":
                        IsReady = true;
                        _log.Info("Monaco editor is ready");
                        EditorReady?.Invoke();

                        if (_pendingContent != null)
                        {
                            _log.Info("Flushing pending content to editor");
                            SetContent(_pendingContent);
                            _pendingContent = null;
                        }
                        break;

                    case "codeChanged":
                        var text = root.GetProperty("text").GetString() ?? string.Empty;
                        CodeChanged?.Invoke(text);
                        break;

                    case "hoverLine":
                        var snippet = root.GetProperty("snippet").GetString() ?? string.Empty;
                        HoverLine?.Invoke(snippet);
                        break;
                }
            }
            catch (Exception ex)
            {
                _log.Error("EditorBridge message error", ex);
            }
        }

        /// <summary>Find the 1-based line number of a snippet in the editor content.</summary>
        public void FindAndHighlight(string snippet, string cssClass)
        {
            if (_webView?.CoreWebView2 == null || !IsReady || string.IsNullOrWhiteSpace(snippet))
                return;

            // Escape for JS template literal
            string escaped = snippet
                .Replace("\\", "\\\\")
                .Replace("`", "\\`")
                .Replace("$", "\\$");

            string decoVar = cssClass == "hover-line" ? "_hoverDeco" : "_lastDeco";
            string js = $@"
                (function() {{
                    if (!window.editor) return;
                    const text = window.editor.getValue();
                    const lines = text.split('\n');
                    let targetLine = -1;
                    const searchStr = `{escaped}`.trim();
                    const token = searchStr.split(/[\s>]/)[0].replace(/^</, '');

                    if (!token) return;

                    for (let i = 0; i < lines.length; i++) {{
                        if (lines[i].includes(token)) {{
                            targetLine = i + 1;
                            break;
                        }}
                    }}

                    if (targetLine <= 0) return;

                    if (window.{decoVar}) window.editor.deltaDecorations(window.{decoVar}, []);
                    window.{decoVar} = window.editor.deltaDecorations([], [{{
                        range: new monaco.Range(targetLine, 1, targetLine, 1),
                        options: {{ isWholeLine: true, className: '{cssClass}' }}
                    }}]);
                    {(cssClass == "highlighted-line" ? "window.editor.revealLineInCenter(targetLine);" : "")}
                }})();";

            _webView.CoreWebView2.ExecuteScriptAsync(js);
        }
    }
}
