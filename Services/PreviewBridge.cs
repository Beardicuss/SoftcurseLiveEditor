using System;
using System.Text.Json;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;

namespace HtmlLiveEditor.Services
{
    public sealed class PreviewBridge : IPreviewBridge
    {
        private readonly IAppLogger _log;
        private WebView2? _webView;
        private string _lastRenderedHtml = string.Empty;

        public PreviewBridge(IAppLogger log)
        {
            _log = log;
        }

        public event Action<string>? ElementHovered;
        public event Action? ElementUnhovered;
        public event Action<string>? ElementClicked;

        public void Attach(WebView2 webView)
        {
            _webView = webView;
            _webView.CoreWebView2.WebMessageReceived += OnMessageReceived;
            _log.Info("PreviewBridge attached");
        }

        /// <summary>
        /// Inject HTML into the preview's #content div via JavaScript.
        /// This preserves the preview shell and all its event listeners.
        /// </summary>
        public void UpdateContent(string html)
        {
            if (_webView?.CoreWebView2 == null)
            {
                _log.Error("PreviewBridge.UpdateContent: CoreWebView2 is null");
                return;
            }

            // Skip if content is unchanged to avoid unnecessary DOM updates
            if (html == _lastRenderedHtml) return;
            _lastRenderedHtml = html;

            // Encode as JSON string to safely embed in JS (handles all escaping)
            string jsonSafe = JsonSerializer.Serialize(html);

            string js = $@"
                (function() {{
                    var el = document.getElementById('content');
                    if (!el) return;
                    
                    var newHtml = {jsonSafe};
                    
                    function getStyles(html) {{
                        var temp = document.createElement('div');
                        temp.innerHTML = html;
                        return Array.from(temp.querySelectorAll('style'));
                    }}
                    
                    function getBodyStripped(html) {{
                        var temp = document.createElement('div');
                        temp.innerHTML = html;
                        temp.querySelectorAll('style').forEach(s => s.remove());
                        return temp.innerHTML.trim();
                    }}
                    
                    var newBody = getBodyStripped(newHtml);
                    var oldBody = getBodyStripped(el.innerHTML);
                    
                    if (newBody === oldBody) {{
                        // Only styles changed — HMR
                        el.querySelectorAll('style').forEach(s => s.remove());
                        getStyles(newHtml).forEach(s => el.appendChild(s));
                    }} else {{
                        // Full rebuild — preserve scroll
                        var scroller = document.querySelector('.main-layout');
                        var st = scroller ? scroller.scrollTop : 0;
                        
                        el.innerHTML = newHtml;
                        
                        if (scroller) scroller.scrollTop = st;
                    }}
                }})();";

            _webView.CoreWebView2.ExecuteScriptAsync(js);
        }

        /// <summary>
        /// Forward an editor hover snippet to preview for visual flash effect.
        /// </summary>
        public void SendEditorHover(string snippet)
        {
            if (_webView?.CoreWebView2 == null) return;
            var message = JsonSerializer.Serialize(new { type = "editorHover", htmlSnippet = snippet });
            _webView.CoreWebView2.PostWebMessageAsJson(message);
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
                string? msgType = typeProp.GetString();

                string? snippet = root.TryGetProperty("htmlSnippet", out var prop)
                    ? prop.GetString()
                    : null;

                switch (msgType)
                {
                    case "hover":
                        if (!string.IsNullOrWhiteSpace(snippet))
                            ElementHovered?.Invoke(snippet);
                        break;

                    case "unhover":
                        ElementUnhovered?.Invoke();
                        break;

                    case "highlight":
                        if (!string.IsNullOrWhiteSpace(snippet))
                            ElementClicked?.Invoke(snippet);
                        break;
                }
            }
            catch (Exception ex)
            {
                _log.Error("PreviewBridge message error", ex);
            }
        }
    }
}
