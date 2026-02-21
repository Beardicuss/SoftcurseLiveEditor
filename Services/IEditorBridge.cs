using System;
using Microsoft.Web.WebView2.WinForms;

namespace HtmlLiveEditor.Services
{
    public interface IEditorBridge
    {
        bool IsReady { get; }

        /// <summary>Initialize the bridge after WebView2 is ready.</summary>
        void Attach(WebView2 webView);

        /// <summary>Send HTML content to the editor.</summary>
        void SetContent(string html);

        /// <summary>Set the Monaco editor theme.</summary>
        void SetTheme(string theme);

        /// <summary>Set the Monaco editor language mode.</summary>
        void SetLanguage(string language);

        /// <summary>Insert a snippet at the cursor position.</summary>
        void InsertSnippet(string snippet);

        /// <summary>Highlight a specific line in the editor.</summary>
        void HighlightLine(int lineNumber, string cssClass);

        /// <summary>Clear all decorations of a given type.</summary>
        void ClearDecorations(string decoId);

        /// <summary>Fired when Monaco editor reports it is fully loaded.</summary>
        event Action? EditorReady;

        /// <summary>Fired when the user edits code. Payload is the full text.</summary>
        event Action<string>? CodeChanged;

        /// <summary>Fired when the user hovers over a line. Payload is the trimmed line content.</summary>
        event Action<string>? HoverLine;
    }
}
