using System;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Web.WebView2.Core;
using HtmlLiveEditor.Config;
using HtmlLiveEditor.Models;
using HtmlLiveEditor.Services;

namespace HtmlLiveEditor
{
    public partial class Form1 : Form
    {
        private readonly IAppLogger _log;
        private readonly IFileService _fileService;
        private readonly IEditorBridge _editor;
        private readonly IPreviewBridge _preview;
        private readonly ISettingsManager _settingsManager;
        private UserSettings _settings;

        private class OpenDocument
        {
            public string Path { get; set; } = string.Empty;
            public string Content { get; set; } = string.Empty;
            public bool IsDirty { get; set; } = false;
            public string Language { get; set; } = "html";
            public Panel TabPanel { get; set; } = null!;
            public Label TitleLabel { get; set; } = null!;
        }

        private List<OpenDocument> _openDocuments = new();
        private OpenDocument? _activeDocument = null;

        private string _lastHtml = string.Empty;
        private bool _isDirty = false;
        private System.Windows.Forms.Timer? _autoSaveTimer;
        private string _currentLanguage = "html";

        public Form1(IAppLogger log, IFileService fileService, IEditorBridge editor, IPreviewBridge preview, ISettingsManager settingsManager)
        {
            _log = log;
            _fileService = fileService;
            _editor = editor;
            _preview = preview;
            _settingsManager = settingsManager;

            InitializeComponent();
            
            // Hide the tree view completely until a workspace is opened
            this.splitContainer2.Panel1Collapsed = true;

            // Load persistent settings
            _settings = _settingsManager.Load();
            
            // Apply Layout Settings
            if (_settings.WindowX >= 0 && _settings.WindowY >= 0)
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new System.Drawing.Point(_settings.WindowX, _settings.WindowY);
            }
            this.Width = Math.Max(800, _settings.WindowWidth);
            this.Height = Math.Max(600, _settings.WindowHeight);
            if (_settings.IsMaximized) this.WindowState = FormWindowState.Maximized;
            this.splitContainer1.SplitterDistance = _settings.SplitterDistance;

            // ── File menu ──
            this.Load += Form1_Load;
            this.openToolStripMenuItem.Click += OnOpen;
            this.openFolderToolStripMenuItem.Click += OnOpenFolder;
            this.saveToolStripMenuItem.Click += OnSave;
            this.saveAsToolStripMenuItem.Click += OnSaveAs;

            // ── TreeView ──
            this.treeViewFiles.NodeMouseDoubleClick += TreeViewFiles_NodeMouseDoubleClick;
            this.newFileToolStripMenuItem.Click += OnNewFile;
            this.newFolderToolStripMenuItem.Click += OnNewFolder;
            this.renameToolStripMenuItem.Click += OnRenameNode;
            this.deleteToolStripMenuItem.Click += OnDeleteNode;
            this.treeContextMenu.Opening += TreeContextMenu_Opening;

            // ── Theme menu ──
            this.lightThemeToolStripMenuItem.Click += (_, _) => { _editor.SetTheme("vs"); _settings.Theme = "vs"; };
            this.darkThemeToolStripMenuItem.Click += (_, _) => { _editor.SetTheme("vs-dark"); _settings.Theme = "vs-dark"; };

            // ── Language menu ──
            this.langHtml.Click += (_, _) => SetLanguage("html");
            this.langCss.Click += (_, _) => SetLanguage("css");
            this.langJs.Click += (_, _) => SetLanguage("javascript");

            // ── Refresh preview / DevTools ──
            this.refreshPreviewMenuItem.Click += (_, _) => RefreshPreview();
            this.devToolsToolStripMenuItem.Click += (_, _) => webViewPreview.CoreWebView2?.OpenDevToolsWindow();
            
            this.detachPreviewMenuItem.Click += (_, _) => 
            {
                if (webViewPreview.Parent == splitContainer1.Panel2)
                {
                    var previewWin = new HtmlLiveEditor.UI.PreviewWindow(webViewPreview, splitContainer1.Panel2);
                    previewWin.Show(this);
                }
            };

            // ── Snippets ──
            this.snippetBoilerplate.Click += (_, _) => _editor.InsertSnippet(Snippets.Boilerplate);
            this.snippetFlexbox.Click += (_, _) => _editor.InsertSnippet(Snippets.Flexbox);
            this.snippetGrid.Click += (_, _) => _editor.InsertSnippet(Snippets.Grid);
            this.snippetForm.Click += (_, _) => _editor.InsertSnippet(Snippets.Form);
            this.snippetTable.Click += (_, _) => _editor.InsertSnippet(Snippets.Table);

            // ── Custom Titlebar ──
            this.btnClose.Click += (_, _) => Close();
            this.btnMin.Click += (_, _) => this.WindowState = FormWindowState.Minimized;
            this.btnMax.Click += (_, _) => this.WindowState = this.WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
            this.titleBarPanel.MouseDown += TitleBarPanel_MouseDown;
            this.lblTitle.MouseDown += TitleBarPanel_MouseDown;
            this.menuStrip1.MouseDown += TitleBarPanel_MouseDown;
        }

        // ═══════════════════════════════════════════
        //  Win32 Native Hooks (Draggable Chromeless Window)
        // ═══════════════════════════════════════════
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void TitleBarPanel_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.Clicks == 2)
                {
                    this.WindowState = this.WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
                }
                else
                {
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0x84) // WM_NCHITTEST
            {
                var cursor = this.PointToClient(Cursor.Position);
                int borderSize = 8;
                
                if (cursor.Y <= borderSize) {
                    if (cursor.X <= borderSize) m.Result = (IntPtr)13; // HTTOPLEFT
                    else if (cursor.X >= ClientSize.Width - borderSize) m.Result = (IntPtr)14; // HTTOPRIGHT
                    else m.Result = (IntPtr)12; // HTTOP
                }
                else if (cursor.Y >= ClientSize.Height - borderSize) {
                    if (cursor.X <= borderSize) m.Result = (IntPtr)16; // HTBOTTOMLEFT
                    else if (cursor.X >= ClientSize.Width - borderSize) m.Result = (IntPtr)17; // HTBOTTOMRIGHT
                    else m.Result = (IntPtr)15; // HTBOTTOM
                }
                else if (cursor.X <= borderSize) m.Result = (IntPtr)10; // HTLEFT
                else if (cursor.X >= ClientSize.Width - borderSize) m.Result = (IntPtr)11; // HTRIGHT
            }
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
            _autoSaveTimer.Tick += async (_, _) =>
            {
                if (!string.IsNullOrEmpty(_lastHtml))
                {
                    try
                    {
                        await _fileService.AutoSaveAsync(_lastHtml);
                        SetStatus("Auto-saved");
                    }
                    catch (Exception ex)
                    {
                        _log.Error("AutoSave failed", ex);
                        SetStatus("⚠ Auto-save failed");
                    }
                }
            };
            _autoSaveTimer.Start();

            if (!string.IsNullOrEmpty(_settings.LastWorkspacePath) && Directory.Exists(_settings.LastWorkspacePath))
            {
                PopulateTreeView(_settings.LastWorkspacePath);
            }

            _log.Info("=== Form1_Load completed ===");
        }

        private void OnEditorReady()
        {
            SetStatus("Editor ready — LINK: STABLE");
            _editor.SetTheme(_settings.Theme);
            SetLanguage(_settings.Language);
        }

        // ═══════════════════════════════════════════
        //  Editor → Preview
        // ═══════════════════════════════════════════

        private void OnCodeChanged(string html)
        {
            _lastHtml = html;
            
            if (_activeDocument != null)
            {
                _activeDocument.Content = html;
                if (!_activeDocument.IsDirty)
                {
                    _activeDocument.IsDirty = true;
                    _activeDocument.TitleLabel.Text = Path.GetFileName(_activeDocument.Path) + " *";
                }
            }
            else
            {
                _isDirty = true;
            }

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
            if (content == null || _fileService.CurrentFilePath == null) return;

            OpenDocumentTab(_fileService.CurrentFilePath);

            if (_fileService.IsLargeFile(content))
            {
                SetStatus("⚠ Large file — preview may be slow");
                _log.Warn($"Large file opened: {content.Length} chars");
            }
        }

        private void OnOpenFolder(object? sender, EventArgs e)
        {
            var folderPath = _fileService.OpenWorkspace();
            if (string.IsNullOrEmpty(folderPath)) return;

            _settings.LastWorkspacePath = folderPath;
            PopulateTreeView(folderPath);
            SetStatus($"Workspace: {Path.GetFileName(folderPath)}");
        }

        private void PopulateTreeView(string path)
        {
            treeViewFiles.Nodes.Clear();
            var rootNode = new TreeNode(Path.GetFileName(path)) { Tag = path };
            treeViewFiles.Nodes.Add(rootNode);

            try
            {
                LoadDirectories(path, rootNode);
                rootNode.Expand();
                this.splitContainer2.Panel1Collapsed = false; // Ensure panel is visible when workspace loaded
            }
            catch (Exception ex)
            {
                _log.Error($"Failed to load workspace files: {path}", ex);
                SetStatus("⚠ Access denied loading workspace files");
            }
        }

        private void LoadDirectories(string dir, TreeNode node)
        {
            try
            {
                foreach (var d in Directory.GetDirectories(dir))
                {
                    if (new DirectoryInfo(d).Attributes.HasFlag(FileAttributes.Hidden)) continue;
                    
                    var n = new TreeNode(Path.GetFileName(d)) { Tag = d };
                    node.Nodes.Add(n);
                    LoadDirectories(d, n);
                }

                foreach (var f in Directory.GetFiles(dir))
                {
                    if (new FileInfo(f).Attributes.HasFlag(FileAttributes.Hidden)) continue;
                    var n = new TreeNode(Path.GetFileName(f)) { Tag = f, ForeColor = System.Drawing.Color.FromArgb(100, 200, 255) };
                    node.Nodes.Add(n);
                }
            }
            catch (UnauthorizedAccessException) { }
        }

        private void TreeViewFiles_NodeMouseDoubleClick(object? sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node == null || e.Node.Tag == null) return;
            string path = e.Node.Tag.ToString()!;

            if (File.Exists(path))
            {
                OpenDocumentTab(path);
            }
        }

        // ═══════════════════════════════════════════
        //  Tab Management
        // ═══════════════════════════════════════════

        private void OpenDocumentTab(string path)
        {
            var existing = _openDocuments.FirstOrDefault(d => d.Path.Equals(path, StringComparison.OrdinalIgnoreCase));
            if (existing != null)
            {
                SwitchToDocument(existing);
                return;
            }

            string content = File.ReadAllText(path);
            string ext = Path.GetExtension(path).ToLowerInvariant();
            string lang = "html";
            if (ext == ".css") lang = "css";
            else if (ext == ".js" || ext == ".ts" || ext == ".json") lang = "javascript"; // Simplified for now

            var doc = new OpenDocument { Path = path, Content = content, Language = lang };

            var pnl = new Panel { Width = 150, Height = 32, Dock = DockStyle.Left, BackColor = Color.FromArgb(25, 25, 25) };
            var lblClose = new Label { Text = "✕", Width = 20, Dock = DockStyle.Right, TextAlign = ContentAlignment.MiddleCenter, ForeColor = Color.Gray, Cursor = Cursors.Hand };
            var lblTitle = new Label { Text = Path.GetFileName(path), AutoSize = false, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft, ForeColor = Color.Gainsboro, Cursor = Cursors.Hand };

            lblClose.MouseEnter += (s, ev) => lblClose.ForeColor = Color.White;
            lblClose.MouseLeave += (s, ev) => lblClose.ForeColor = Color.Gray;
            lblClose.Click += (s, ev) => CloseDocumentTab(doc);

            lblTitle.Click += (s, ev) => SwitchToDocument(doc);
            pnl.Click += (s, ev) => SwitchToDocument(doc);

            pnl.Controls.Add(lblTitle);
            pnl.Controls.Add(lblClose);

            doc.TabPanel = pnl;
            doc.TitleLabel = lblTitle;

            _openDocuments.Add(doc);
            tabBarPanel.Controls.Add(pnl);

            SwitchToDocument(doc);
        }

        private void SwitchToDocument(OpenDocument doc)
        {
            if (_activeDocument != null)
            {
                // Unhighlight old tab
                _activeDocument.Content = _lastHtml; // Save memory state
                _activeDocument.TabPanel.BackColor = Color.FromArgb(25, 25, 25);
                _activeDocument.TitleLabel.ForeColor = Color.Gainsboro;
            }

            _activeDocument = doc;
            _lastHtml = doc.Content;

            doc.TabPanel.BackColor = Color.FromArgb(45, 45, 50); // Highlight
            doc.TitleLabel.ForeColor = Color.White;

            _fileService.GetType().GetProperty("CurrentFilePath")!.SetValue(_fileService, doc.Path);
            SetLanguage(doc.Language);

            _preview.UpdateContent(doc.Content);
            _editor.SetContent(doc.Content);

            this.Text = $"Softcurse LiveScriptor — {Path.GetFileName(doc.Path)}";
            SetStatus($"Opened: {Path.GetFileName(doc.Path)}");
        }

        private void CloseDocumentTab(OpenDocument doc)
        {
            if (doc.IsDirty)
            {
                var result = MessageBox.Show($"Save changes to {Path.GetFileName(doc.Path)} before closing?", "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Cancel) return;
                if (result == DialogResult.Yes)
                {
                    File.WriteAllText(doc.Path, doc.Content, System.Text.Encoding.UTF8);
                }
            }

            _openDocuments.Remove(doc);
            tabBarPanel.Controls.Remove(doc.TabPanel);
            doc.TabPanel.Dispose();

            if (_activeDocument == doc)
            {
                _activeDocument = null;
                _lastHtml = string.Empty;
                if (_openDocuments.Count > 0)
                {
                    SwitchToDocument(_openDocuments.Last());
                }
                else
                {
                    _editor.SetContent("<!-- Welcome to the Sanctum -->");
                    _preview.UpdateContent("<h1>Ready</h1>");
                    this.Text = "Softcurse LiveScriptor";
                    _fileService.GetType().GetProperty("CurrentFilePath")!.SetValue(_fileService, null);
                }
            }
        }

        // ═══════════════════════════════════════════
        //  TreeView Context Menu Actions
        // ═══════════════════════════════════════════

        private void TreeContextMenu_Opening(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            if (treeViewFiles.SelectedNode == null)
            {
                e.Cancel = true;
            }
        }

        private void OnNewFile(object? sender, EventArgs e)
        {
            var targetNode = treeViewFiles.SelectedNode;
            if (targetNode == null || targetNode.Tag == null) return;

            string targetPath = targetNode.Tag.ToString()!;
            if (File.Exists(targetPath)) targetPath = Path.GetDirectoryName(targetPath)!;

            string name = PromptDialog("Enter file name:", "New File");
            if (string.IsNullOrWhiteSpace(name)) return;

            try
            {
                string fullPath = Path.Combine(targetPath, name);
                File.WriteAllText(fullPath, Environment.NewLine);
                var parentNode = File.Exists(targetNode.Tag.ToString()!) ? targetNode.Parent : targetNode;
                var newNode = new TreeNode(name) { Tag = fullPath, ForeColor = System.Drawing.Color.FromArgb(100, 200, 255) };
                parentNode?.Nodes.Add(newNode);
                parentNode?.Expand();
            }
            catch (Exception ex)
            {
                _log.Error("Failed to create file", ex);
                MessageBox.Show("Error creating file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnNewFolder(object? sender, EventArgs e)
        {
            var targetNode = treeViewFiles.SelectedNode;
            if (targetNode == null || targetNode.Tag == null) return;

            string targetPath = targetNode.Tag.ToString()!;
            if (File.Exists(targetPath)) targetPath = Path.GetDirectoryName(targetPath)!;

            string name = PromptDialog("Enter folder name:", "New Folder");
            if (string.IsNullOrWhiteSpace(name)) return;

            try
            {
                string fullPath = Path.Combine(targetPath, name);
                Directory.CreateDirectory(fullPath);
                var parentNode = File.Exists(targetNode.Tag.ToString()!) ? targetNode.Parent : targetNode;
                var newNode = new TreeNode(name) { Tag = fullPath };
                parentNode?.Nodes.Add(newNode);
                parentNode?.Expand();
            }
            catch (Exception ex)
            {
                _log.Error("Failed to create folder", ex);
                MessageBox.Show("Error creating folder: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnRenameNode(object? sender, EventArgs e)
        {
            var targetNode = treeViewFiles.SelectedNode;
            if (targetNode == null || targetNode.Tag == null) return;

            string oldPath = targetNode.Tag.ToString()!;
            bool isFile = File.Exists(oldPath);
            string oldName = targetNode.Text;

            string name = PromptDialog("Enter new name:", "Rename", oldName);
            if (string.IsNullOrWhiteSpace(name) || name == oldName) return;

            try
            {
                string newPath = Path.Combine(Path.GetDirectoryName(oldPath)!, name);
                if (isFile) File.Move(oldPath, newPath);
                else Directory.Move(oldPath, newPath);

                targetNode.Text = name;
                targetNode.Tag = newPath;
            }
            catch (Exception ex)
            {
                _log.Error("Failed to rename", ex);
                MessageBox.Show("Error renaming: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnDeleteNode(object? sender, EventArgs e)
        {
            var targetNode = treeViewFiles.SelectedNode;
            if (targetNode == null || targetNode.Tag == null) return;

            string targetPath = targetNode.Tag.ToString()!;
            bool isFile = File.Exists(targetPath);

            var result = MessageBox.Show(
                $"Are you sure you want to delete '{(isFile ? Path.GetFileName(targetPath) : Path.GetDirectoryName(targetPath))}'?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (isFile) File.Delete(targetPath);
                    else Directory.Delete(targetPath, true);
                    targetNode.Remove();
                }
                catch (Exception ex)
                {
                    _log.Error("Failed to delete", ex);
                    MessageBox.Show("Error deleting: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string PromptDialog(string text, string caption, string defaultVal = "")
        {
            Form prompt = new Form()
            {
                Width = 400,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterParent,
            };
            Label textLabel = new Label() { Left = 20, Top = 20, Text = text, AutoSize = true };
            TextBox textBox = new TextBox() { Left = 20, Top = 50, Width = 340, Text = defaultVal };
            Button confirmation = new Button() { Text = "OK", Left = 260, Width = 100, Top = 80, DialogResult = DialogResult.OK };
            
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }

        private void OnSave(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_lastHtml)) { SetStatus("Nothing to save"); return; }
            if (_fileService.Save(_lastHtml))
            {
                _isDirty = false;
                if (_activeDocument != null)
                {
                    _activeDocument.IsDirty = false;
                    _activeDocument.TitleLabel.Text = Path.GetFileName(_activeDocument.Path);
                }
                SetStatus($"Saved: {Path.GetFileName(_fileService.CurrentFilePath)}");
            }
        }

        private void OnSaveAs(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_lastHtml)) { SetStatus("Nothing to save"); return; }
            if (_fileService.SaveAs(_lastHtml))
            {
                _isDirty = false;
                this.Text = $"Softcurse LiveScriptor — {Path.GetFileName(_fileService.CurrentFilePath)}";
                SetStatus($"Saved As: {Path.GetFileName(_fileService.CurrentFilePath)}");
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            bool anyDirty = _openDocuments.Any(d => d.IsDirty) || _isDirty;
            if (anyDirty)
            {
                var result = MessageBox.Show(
                    "You have unsaved changes in one or more files. Save before closing?",
                    "Unsaved Changes",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Cancel) { e.Cancel = true; return; }
                if (result == DialogResult.Yes)
                {
                    if (_isDirty && _activeDocument == null) OnSave(null, EventArgs.Empty);
                    foreach (var doc in _openDocuments.Where(d => d.IsDirty))
                    {
                        File.WriteAllText(doc.Path, doc.Content, System.Text.Encoding.UTF8);
                    }
                }
            }

            // Save settings
            _settings.WindowWidth = this.Width;
            _settings.WindowHeight = this.Height;
            _settings.IsMaximized = this.WindowState == FormWindowState.Maximized;
            if (this.WindowState == FormWindowState.Normal)
            {
                _settings.WindowX = this.Location.X;
                _settings.WindowY = this.Location.Y;
            }
            _settings.SplitterDistance = this.splitContainer1.SplitterDistance;
            
            _settingsManager.Save(_settings);

            _autoSaveTimer?.Stop();
            _autoSaveTimer?.Dispose();
            base.OnFormClosing(e);
        }

        // ═══════════════════════════════════════════
        //  Language / Theme / Refresh
        // ═══════════════════════════════════════════

        private void SetLanguage(string language)
        {
            _currentLanguage = language;
            _settings.Language = language;
            _editor.SetLanguage(language);
            SetStatus($"Language: {language.ToUpperInvariant()}");
        }

        private void RefreshPreview()
        {
            if (_currentLanguage != "html")
            {
                SetStatus("Preview only available in HTML mode");
                return;
            }
            if (!string.IsNullOrEmpty(_lastHtml))
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
}