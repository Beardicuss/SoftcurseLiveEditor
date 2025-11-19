namespace HtmlLiveEditor
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewEditor;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewPreview;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem; // Added
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lightThemeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem darkThemeToolStripMenuItem;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.webViewEditor = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.webViewPreview = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem(); // Added
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lightThemeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.darkThemeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();

            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webViewEditor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.webViewPreview)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();

            // menuStrip1
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.fileToolStripMenuItem,
                this.viewToolStripMenuItem
            });
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1600, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";

            // fileToolStripMenuItem
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.openToolStripMenuItem,
                this.saveToolStripMenuItem,
                this.saveAsToolStripMenuItem // Added to dropdown
            });
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";

            // openToolStripMenuItem
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openToolStripMenuItem.Text = "&Open";

            // saveToolStripMenuItem
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveToolStripMenuItem.Text = "&Save";

            // saveAsToolStripMenuItem - Added
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.S)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveAsToolStripMenuItem.Text = "Save &As";

            // viewToolStripMenuItem
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.lightThemeToolStripMenuItem,
                this.darkThemeToolStripMenuItem
            });
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";

            // lightThemeToolStripMenuItem
            this.lightThemeToolStripMenuItem.Name = "lightThemeToolStripMenuItem";
            this.lightThemeToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.lightThemeToolStripMenuItem.Text = "Light";

            // darkThemeToolStripMenuItem
            this.darkThemeToolStripMenuItem.Name = "darkThemeToolStripMenuItem";
            this.darkThemeToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.darkThemeToolStripMenuItem.Text = "Dark";

            // splitContainer1
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1.Controls.Add(this.webViewEditor);
            this.splitContainer1.Panel2.Controls.Add(this.webViewPreview);
            this.splitContainer1.Size = new System.Drawing.Size(1600, 876);
            this.splitContainer1.SplitterDistance = 800;
            this.splitContainer1.TabIndex = 0;

            // webViewEditor
            this.webViewEditor.AllowExternalDrop = true;
            this.webViewEditor.CreationProperties = null;
            this.webViewEditor.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webViewEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webViewEditor.Location = new System.Drawing.Point(0, 0);
            this.webViewEditor.Name = "webViewEditor";
            this.webViewEditor.Size = new System.Drawing.Size(800, 876);
            this.webViewEditor.TabIndex = 0;
            this.webViewEditor.ZoomFactor = 1D;

            // webViewPreview
            this.webViewPreview.AllowExternalDrop = true;
            this.webViewPreview.CreationProperties = null;
            this.webViewPreview.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webViewPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webViewPreview.Location = new System.Drawing.Point(0, 0);
            this.webViewPreview.Name = "webViewPreview";
            this.webViewPreview.Size = new System.Drawing.Size(796, 876);
            this.webViewPreview.TabIndex = 0;
            this.webViewPreview.ZoomFactor = 1D;

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1600, 900);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "HTML Live Editor";

            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.webViewEditor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.webViewPreview)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}