using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;

namespace HtmlLiveEditor.UI
{
    public class PreviewWindow : Form
    {
        private readonly WebView2 _webView;
        private readonly Control _originalParent;

        public PreviewWindow(WebView2 webView, Control originalParent)
        {
            _webView = webView;
            _originalParent = originalParent;

            this.Text = "Softcurse LiveScriptor — Live Preview";
            this.Width = 1000;
            this.Height = 800;
            this.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
            this.Icon = originalParent.FindForm()?.Icon;
            this.StartPosition = FormStartPosition.CenterScreen;

            _originalParent.Controls.Remove(_webView);
            this.Controls.Add(_webView);
            _webView.Dock = DockStyle.Fill;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            this.Controls.Remove(_webView);
            _originalParent.Controls.Add(_webView);
            _webView.Dock = DockStyle.Fill;
            base.OnFormClosing(e);
        }
    }
}
