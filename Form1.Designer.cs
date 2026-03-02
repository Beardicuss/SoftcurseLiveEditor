namespace HtmlLiveEditor
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView treeViewFiles;
        private System.Windows.Forms.Panel tabBarPanel;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewEditor;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewPreview;
        private System.Windows.Forms.Panel titleBarPanel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnMax;
        private System.Windows.Forms.Button btnMin;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFolderToolStripMenuItem;
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
        private System.Windows.Forms.ToolStripMenuItem devToolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem detachPreviewMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ContextMenuStrip treeContextMenu;
        private System.Windows.Forms.ToolStripMenuItem newFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.treeViewFiles = new System.Windows.Forms.TreeView();
            this.tabBarPanel = new System.Windows.Forms.Panel();
            this.webViewEditor = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.webViewPreview = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.titleBarPanel = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnMax = new System.Windows.Forms.Button();
            this.btnMin = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.devToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detachPreviewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.treeContextMenu = new System.Windows.Forms.ContextMenuStrip();
            this.newFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();

            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webViewEditor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.webViewPreview)).BeginInit();
            this.titleBarPanel.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.treeContextMenu.SuspendLayout();
            this.SuspendLayout();

            // ═══ TitleBarPanel ═══
            this.titleBarPanel.BackColor = System.Drawing.Color.FromArgb(18, 18, 20);
            this.titleBarPanel.Controls.Add(this.lblTitle);
            this.titleBarPanel.Controls.Add(this.btnMin);
            this.titleBarPanel.Controls.Add(this.btnMax);
            this.titleBarPanel.Controls.Add(this.btnClose);
            this.titleBarPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleBarPanel.Location = new System.Drawing.Point(0, 0);
            this.titleBarPanel.Name = "titleBarPanel";
            this.titleBarPanel.Size = new System.Drawing.Size(1600, 32);
            this.titleBarPanel.TabIndex = 3;

            // btnClose
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnClose.Location = new System.Drawing.Point(1550, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(50, 32);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "✕";
            this.btnClose.UseVisualStyleBackColor = true;

            // btnMax
            this.btnMax.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMax.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMax.FlatAppearance.BorderSize = 0;
            this.btnMax.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnMax.Location = new System.Drawing.Point(1500, 0);
            this.btnMax.Name = "btnMax";
            this.btnMax.Size = new System.Drawing.Size(50, 32);
            this.btnMax.TabIndex = 1;
            this.btnMax.Text = "⬜";
            this.btnMax.UseVisualStyleBackColor = true;

            // btnMin
            this.btnMin.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMin.FlatAppearance.BorderSize = 0;
            this.btnMin.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnMin.Location = new System.Drawing.Point(1450, 0);
            this.btnMin.Name = "btnMin";
            this.btnMin.Size = new System.Drawing.Size(50, 32);
            this.btnMin.TabIndex = 2;
            this.btnMin.Text = "—";
            this.btnMin.UseVisualStyleBackColor = true;

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(12, 7);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(130, 17);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "Softcurse LiveScriptor";

            // ═══ MenuStrip ═══
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.fileToolStripMenuItem,
                this.editToolStripMenuItem,
                this.viewToolStripMenuItem
            });
            this.menuStrip1.Location = new System.Drawing.Point(0, 32);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1600, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
            this.menuStrip1.ForeColor = System.Drawing.Color.Gainsboro;

            // ─── File ───
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.openToolStripMenuItem,
                this.openFolderToolStripMenuItem,
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
            this.openToolStripMenuItem.Text = "&Open File";

            // Open Folder
            this.openFolderToolStripMenuItem.Name = "openFolderToolStripMenuItem";
            this.openFolderToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.O)));
            this.openFolderToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.openFolderToolStripMenuItem.Text = "Open F&older";

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
                this.refreshPreviewMenuItem,
                this.devToolsToolStripMenuItem,
                new System.Windows.Forms.ToolStripSeparator(),
                this.detachPreviewMenuItem
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

            // DevTools
            this.devToolsToolStripMenuItem.Name = "devToolsToolStripMenuItem";
            this.devToolsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.devToolsToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.devToolsToolStripMenuItem.Text = "Toggle &DevTools";

            // Detach Preview
            this.detachPreviewMenuItem.Name = "detachPreviewMenuItem";
            this.detachPreviewMenuItem.Size = new System.Drawing.Size(200, 22);
            this.detachPreviewMenuItem.Text = "Detach &Preview Pane";

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

            // ═══ SplitContainer2 (Tree vs Editor/Preview) ═══
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 56);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Panel1.Controls.Add(this.treeViewFiles);
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer2.Size = new System.Drawing.Size(1600, 822);
            this.splitContainer2.SplitterDistance = 250;
            this.splitContainer2.TabIndex = 1;
            this.splitContainer2.BackColor = System.Drawing.Color.FromArgb(20, 20, 20);

            // ═══ TreeView ═══
            this.treeViewFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewFiles.BackColor = System.Drawing.Color.FromArgb(10, 10, 12);
            this.treeViewFiles.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.treeViewFiles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeViewFiles.ContextMenuStrip = this.treeContextMenu;
            this.treeViewFiles.Location = new System.Drawing.Point(0, 0);
            this.treeViewFiles.Name = "treeViewFiles";
            this.treeViewFiles.Size = new System.Drawing.Size(250, 822);
            this.treeViewFiles.TabIndex = 0;
            this.treeViewFiles.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

            // ═══ TreeContextMenuStrip ═══
            this.treeContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.newFileToolStripMenuItem,
                this.newFolderToolStripMenuItem,
                new System.Windows.Forms.ToolStripSeparator(),
                this.renameToolStripMenuItem,
                this.deleteToolStripMenuItem
            });
            this.treeContextMenu.Name = "treeContextMenu";
            this.treeContextMenu.Size = new System.Drawing.Size(181, 120);

            // New File
            this.newFileToolStripMenuItem.Name = "newFileToolStripMenuItem";
            this.newFileToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.newFileToolStripMenuItem.Text = "New &File";

            // New Folder
            this.newFolderToolStripMenuItem.Name = "newFolderToolStripMenuItem";
            this.newFolderToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.newFolderToolStripMenuItem.Text = "New F&older";

            // Rename
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.renameToolStripMenuItem.Text = "&Rename";

            // Delete
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.deleteToolStripMenuItem.Text = "&Delete";

            // ═══ SplitContainer1 (Editor vs Preview) ═══
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1.Controls.Add(this.webViewEditor);
            this.splitContainer1.Panel1.Controls.Add(this.tabBarPanel);
            this.splitContainer1.Panel2.Controls.Add(this.webViewPreview);
            this.splitContainer1.Size = new System.Drawing.Size(1346, 822);
            this.splitContainer1.SplitterDistance = 673;
            this.splitContainer1.TabIndex = 0;

            // ═══ TabBarPanel ═══
            this.tabBarPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabBarPanel.Height = 32;
            this.tabBarPanel.BackColor = System.Drawing.Color.FromArgb(16, 16, 18);
            this.tabBarPanel.Name = "tabBarPanel";
            this.tabBarPanel.TabIndex = 1;

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
            this.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
            this.ClientSize = new System.Drawing.Size(1600, 900);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.titleBarPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Softcurse LiveScriptor";

            this.titleBarPanel.ResumeLayout(false);
            this.titleBarPanel.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
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
            this.treeContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}