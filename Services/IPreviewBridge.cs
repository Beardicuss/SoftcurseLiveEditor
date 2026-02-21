using System;
using Microsoft.Web.WebView2.WinForms;

namespace HtmlLiveEditor.Services
{
    public interface IPreviewBridge
    {
        /// <summary>Initialize the bridge after WebView2 is ready.</summary>
        void Attach(WebView2 webView);

        /// <summary>Inject HTML into the preview's #content div (does NOT replace the page).</summary>
        void UpdateContent(string html);

        /// <summary>Forward an editor hover snippet to preview for visual flash.</summary>
        void SendEditorHover(string snippet);

        /// <summary>Fired when the user hovers over a preview element. Payload: HTML snippet.</summary>
        event Action<string>? ElementHovered;

        /// <summary>Fired when unhover occurs.</summary>
        event Action? ElementUnhovered;

        /// <summary>Fired when the user clicks a preview element. Payload: HTML snippet.</summary>
        event Action<string>? ElementClicked;
    }
}
