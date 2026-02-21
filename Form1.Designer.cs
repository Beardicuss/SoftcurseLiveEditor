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
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lightThemeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem darkThemeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertSnippetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem snippetBoilerplate;
        private System.Windows.Forms.ToolStripMenuItem snippetFlexbox;
        private System.Windows.Forms.ToolStripMenuItem snippetGrid;
        private System.Windows.Forms.ToolStripMenuItem snippetForm;
        private System.Windows.Forms.ToolStripMenuItem snippetTable;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem langHtml;
        private System.Windows.Forms.ToolStripMenuItem langCss;
        private System.Windows.Forms.ToolStripMenuItem langJs;
        private System.Windows.Forms.ToolStripMenuItem refreshPreviewMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;

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
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertSnippetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.snippetBoilerplate = new System.Windows.Forms.ToolStripMenuItem();
            this.snippetFlexbox = new System.Windows.Forms.ToolStripMenuItem();
            this.snippetGrid = new System.Windows.Forms.ToolStripMenuItem();
            this.snippetForm = new System.Windows.Forms.ToolStripMenuItem();
            this.snippetTable = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lightThemeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.darkThemeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.langHtml = new System.Windows.Forms.ToolStripMenuItem();
            this.langCss = new System.Windows.Forms.ToolStripMenuItem();
            this.langJs = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshPreviewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();

            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webViewEditor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.webViewPreview)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();

            // ═══ MenuStrip ═══
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.fileToolStripMenuItem,
                this.editToolStripMenuItem,
                this.viewToolStripMenuItem
            });
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1600, 24);
            this.menuStrip1.TabIndex = 1;

            // ─── File ───
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.openToolStripMenuItem,
                this.saveToolStripMenuItem,
                this.saveAsToolStripMenuItem
            });
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";

            // Open
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.openToolStripMenuItem.Text = "&Open";

            // Save
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.saveToolStripMenuItem.Text = "&Save";

            // Save As
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.S)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.saveAsToolStripMenuItem.Text = "Save &As";

            // ─── Edit ───
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.insertSnippetToolStripMenuItem
            });
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";

            // Insert Snippet
            this.insertSnippetToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.snippetBoilerplate,
                this.snippetFlexbox,
                this.snippetGrid,
                this.snippetForm,
                this.snippetTable
            });
            this.insertSnippetToolStripMenuItem.Name = "insertSnippetToolStripMenuItem";
            this.insertSnippetToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.insertSnippetToolStripMenuItem.Text = "Insert &Snippet";

            this.snippetBoilerplate.Name = "snippetBoilerplate";
            this.snippetBoilerplate.Size = new System.Drawing.Size(180, 22);
            this.snippetBoilerplate.Text = "HTML Boilerplate";

            this.snippetFlexbox.Name = "snippetFlexbox";
            this.snippetFlexbox.Size = new System.Drawing.Size(180, 22);
            this.snippetFlexbox.Text = "Flexbox Layout";

            this.snippetGrid.Name = "snippetGrid";
            this.snippetGrid.Size = new System.Drawing.Size(180, 22);
            this.snippetGrid.Text = "CSS Grid Layout";

            this.snippetForm.Name = "snippetForm";
            this.snippetForm.Size = new System.Drawing.Size(180, 22);
            this.snippetForm.Text = "Form";

            this.snippetTable.Name = "snippetTable";
            this.snippetTable.Size = new System.Drawing.Size(180, 22);
            this.snippetTable.Text = "Table";

            // ─── View ───
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.lightThemeToolStripMenuItem,
                this.darkThemeToolStripMenuItem,
                new System.Windows.Forms.ToolStripSeparator(),
                this.languageToolStripMenuItem,
                new System.Windows.Forms.ToolStripSeparator(),
                this.refreshPreviewMenuItem
            });
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";

            // Light theme
            this.lightThemeToolStripMenuItem.Name = "lightThemeToolStripMenuItem";
            this.lightThemeToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.lightThemeToolStripMenuItem.Text = "&Light Theme";

            // Dark theme
            this.darkThemeToolStripMenuItem.Name = "darkThemeToolStripMenuItem";
            this.darkThemeToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.darkThemeToolStripMenuItem.Text = "&Dark Theme";

            // Language
            this.languageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.langHtml,
                this.langCss,
                this.langJs
            });
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            this.languageToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.languageToolStripMenuItem.Text = "L&anguage";

            this.langHtml.Name = "langHtml";
            this.langHtml.Size = new System.Drawing.Size(150, 22);
            this.langHtml.Text = "HTML";

            this.langCss.Name = "langCss";
            this.langCss.Size = new System.Drawing.Size(150, 22);
            this.langCss.Text = "CSS";

            this.langJs.Name = "langJs";
            this.langJs.Size = new System.Drawing.Size(150, 22);
            this.langJs.Text = "JavaScript";

            // Refresh Preview
            this.refreshPreviewMenuItem.Name = "refreshPreviewMenuItem";
            this.refreshPreviewMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.refreshPreviewMenuItem.Size = new System.Drawing.Size(200, 22);
            this.refreshPreviewMenuItem.Text = "&Refresh Preview";

            // ═══ StatusStrip ═══
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.statusLabel
            });
            this.statusStrip1.Location = new System.Drawing.Point(0, 878);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1600, 22);
            this.statusStrip1.TabIndex = 2;

            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(40, 17);
            this.statusLabel.Text = "Ready";

            // ═══ SplitContainer ═══
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1.Controls.Add(this.webViewEditor);
            this.splitContainer1.Panel2.Controls.Add(this.webViewPreview);
            this.splitContainer1.Size = new System.Drawing.Size(1600, 854);
            this.splitContainer1.SplitterDistance = 800;
            this.splitContainer1.TabIndex = 0;

            // ═══ WebViews ═══
            this.webViewEditor.AllowExternalDrop = true;
            this.webViewEditor.CreationProperties = null;
            this.webViewEditor.DefaultBackgroundColor = System.Drawing.Color.FromArgb(5, 3, 8);
            this.webViewEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webViewEditor.Location = new System.Drawing.Point(0, 0);
            this.webViewEditor.Name = "webViewEditor";
            this.webViewEditor.Size = new System.Drawing.Size(800, 854);
            this.webViewEditor.TabIndex = 0;
            this.webViewEditor.ZoomFactor = 1D;

            this.webViewPreview.AllowExternalDrop = true;
            this.webViewPreview.CreationProperties = null;
            this.webViewPreview.DefaultBackgroundColor = System.Drawing.Color.Black;
            this.webViewPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webViewPreview.Location = new System.Drawing.Point(0, 0);
            this.webViewPreview.Name = "webViewPreview";
            this.webViewPreview.Size = new System.Drawing.Size(796, 854);
            this.webViewPreview.TabIndex = 0;
            this.webViewPreview.ZoomFactor = 1D;

            // ═══ Form1 ═══
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1600, 900);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Softcurse LiveScriptor";

            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.webViewEditor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.webViewPreview)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}