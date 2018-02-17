namespace TextRuler.AdvancedTextEditorControl
{
    partial class AdvancedTextEditor
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        //this.fontComboBox1 = new TextRuler.AdvancedTextEditorControl.FontComboBoxControl.FontComboBox();

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvancedTextEditor));
            this.tlpEditorLayout = new System.Windows.Forms.TableLayoutPanel();
            this.Toolbox_Main = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.sepTBMain1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.btnPrintPreview = new System.Windows.Forms.ToolStripButton();
            this.sepTBMain2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCut = new System.Windows.Forms.ToolStripButton();
            this.btnCopy = new System.Windows.Forms.ToolStripButton();
            this.btnPaste = new System.Windows.Forms.ToolStripButton();
            this.sepTBMain3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnUndo = new System.Windows.Forms.ToolStripButton();
            this.btnRedo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.TextEditorMenu = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPrintPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPageSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuCut = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFind = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRuler = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainToolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFormatting = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInsert = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInsertPicture = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInsertDateTime = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbDateTimeFormats = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.txtCustom = new System.Windows.Forms.ToolStripTextBox();
            this.mnuFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFont = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuColors = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTextColor = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHighlightColor = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.TextEditor = new ExtendedRichTextBox();
            this.pnlFormattingToolbar = new System.Windows.Forms.Panel();
            this.Toolbox_Formatting = new System.Windows.Forms.ToolStrip();
            this.cmbFontName = new System.Windows.Forms.ToolStripComboBox();
            this.cmbFontSize = new System.Windows.Forms.ToolStripComboBox();
            this.sepTBFormatting1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnBold = new System.Windows.Forms.ToolStripButton();
            this.btnItalic = new System.Windows.Forms.ToolStripButton();
            this.btnUnderline = new System.Windows.Forms.ToolStripButton();
            this.btnStrikeThrough = new System.Windows.Forms.ToolStripButton();
            this.sepTBFormatting2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAlignLeft = new System.Windows.Forms.ToolStripButton();
            this.btnAlignCenter = new System.Windows.Forms.ToolStripButton();
            this.btnAlignRight = new System.Windows.Forms.ToolStripButton();
            this.Ruler = new TextRuler.TextRulerControl.TextRuler();
            this.prtDoc = new System.Drawing.Printing.PrintDocument();
            this.DocPreview = new System.Windows.Forms.PrintPreviewDialog();
            this.PageSettings = new System.Windows.Forms.PageSetupDialog();
            this.PrintWnd = new System.Windows.Forms.PrintDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tlpEditorLayout.SuspendLayout();
            this.Toolbox_Main.SuspendLayout();
            this.TextEditorMenu.SuspendLayout();
            this.pnlFormattingToolbar.SuspendLayout();
            this.Toolbox_Formatting.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpEditorLayout
            // 
            this.tlpEditorLayout.ColumnCount = 1;
            this.tlpEditorLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpEditorLayout.Controls.Add(this.Toolbox_Main, 0, 1);
            this.tlpEditorLayout.Controls.Add(this.TextEditorMenu, 0, 0);
            this.tlpEditorLayout.Controls.Add(this.TextEditor, 0, 4);
            this.tlpEditorLayout.Controls.Add(this.pnlFormattingToolbar, 0, 2);
            this.tlpEditorLayout.Controls.Add(this.Ruler, 0, 3);
            this.tlpEditorLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpEditorLayout.Location = new System.Drawing.Point(0, 0);
            this.tlpEditorLayout.Name = "tlpEditorLayout";
            this.tlpEditorLayout.RowCount = 5;
            this.tlpEditorLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tlpEditorLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpEditorLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpEditorLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tlpEditorLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpEditorLayout.Size = new System.Drawing.Size(687, 502);
            this.tlpEditorLayout.TabIndex = 0;
            // 
            // Toolbox_Main
            // 
            this.Toolbox_Main.BackColor = System.Drawing.Color.Transparent;
            this.Toolbox_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnOpen,
            this.btnSave,
            this.sepTBMain1,
            this.btnPrint,
            this.btnPrintPreview,
            this.sepTBMain2,
            this.btnCut,
            this.btnCopy,
            this.btnPaste,
            this.sepTBMain3,
            this.btnUndo,
            this.btnRedo,
            this.toolStripSeparator6});
            this.Toolbox_Main.Location = new System.Drawing.Point(0, 23);
            this.Toolbox_Main.Name = "Toolbox_Main";
            this.Toolbox_Main.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.Toolbox_Main.Size = new System.Drawing.Size(687, 25);
            this.Toolbox_Main.TabIndex = 2;
            this.Toolbox_Main.Text = "toolStrip1";
            // 
            // btnNew
            // 
            this.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(23, 22);
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image")));
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(23, 22);
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(23, 22);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // sepTBMain1
            // 
            this.sepTBMain1.Name = "sepTBMain1";
            this.sepTBMain1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnPrint
            // 
            this.btnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(23, 22);
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnPrintPreview
            // 
            this.btnPrintPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrintPreview.Image = ((System.Drawing.Image)(resources.GetObject("btnPrintPreview.Image")));
            this.btnPrintPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrintPreview.Name = "btnPrintPreview";
            this.btnPrintPreview.Size = new System.Drawing.Size(23, 22);
            this.btnPrintPreview.Click += new System.EventHandler(this.btnPrintPreview_Click);
            // 
            // sepTBMain2
            // 
            this.sepTBMain2.Name = "sepTBMain2";
            this.sepTBMain2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnCut
            // 
            this.btnCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCut.Image = ((System.Drawing.Image)(resources.GetObject("btnCut.Image")));
            this.btnCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCut.Name = "btnCut";
            this.btnCut.Size = new System.Drawing.Size(23, 22);
            this.btnCut.Click += new System.EventHandler(this.btnCut_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCopy.Image = ((System.Drawing.Image)(resources.GetObject("btnCopy.Image")));
            this.btnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(23, 22);
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnPaste
            // 
            this.btnPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPaste.Image = ((System.Drawing.Image)(resources.GetObject("btnPaste.Image")));
            this.btnPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(23, 22);
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // sepTBMain3
            // 
            this.sepTBMain3.Name = "sepTBMain3";
            this.sepTBMain3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnUndo
            // 
            this.btnUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnUndo.Image = ((System.Drawing.Image)(resources.GetObject("btnUndo.Image")));
            this.btnUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(23, 22);
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // btnRedo
            // 
            this.btnRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRedo.Image = ((System.Drawing.Image)(resources.GetObject("btnRedo.Image")));
            this.btnRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRedo.Name = "btnRedo";
            this.btnRedo.Size = new System.Drawing.Size(23, 22);
            this.btnRedo.Click += new System.EventHandler(this.btnRedo_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // TextEditorMenu
            // 
            this.TextEditorMenu.BackColor = System.Drawing.Color.Transparent;
            this.TextEditorMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuView,
            this.mnuInsert,
            this.mnuFormat,
            this.mnuHelp});
            this.TextEditorMenu.Location = new System.Drawing.Point(0, 0);
            this.TextEditorMenu.Name = "TextEditorMenu";
            this.TextEditorMenu.Size = new System.Drawing.Size(687, 23);
            this.TextEditorMenu.TabIndex = 1;
            this.TextEditorMenu.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNew,
            this.mnuOpen,
            this.mnuSave,
            this.mnuSaveAs,
            this.toolStripMenuItem1,
            this.mnuPrint,
            this.mnuPrintPreview,
            this.mnuPageSettings});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(35, 19);
            this.mnuFile.Text = "File";
            // 
            // mnuNew
            // 
            this.mnuNew.Image = ((System.Drawing.Image)(resources.GetObject("mnuNew.Image")));
            this.mnuNew.Name = "mnuNew";
            this.mnuNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.mnuNew.Size = new System.Drawing.Size(164, 22);
            this.mnuNew.Text = "New";
            this.mnuNew.Click += new System.EventHandler(this.mnuNew_Click);
            // 
            // mnuOpen
            // 
            this.mnuOpen.Image = ((System.Drawing.Image)(resources.GetObject("mnuOpen.Image")));
            this.mnuOpen.Name = "mnuOpen";
            this.mnuOpen.Size = new System.Drawing.Size(164, 22);
            this.mnuOpen.Text = "Open...";
            this.mnuOpen.Click += new System.EventHandler(this.mnuOpen_Click);
            // 
            // mnuSave
            // 
            this.mnuSave.Image = ((System.Drawing.Image)(resources.GetObject("mnuSave.Image")));
            this.mnuSave.Name = "mnuSave";
            this.mnuSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F12)));
            this.mnuSave.Size = new System.Drawing.Size(164, 22);
            this.mnuSave.Text = "Save";
            this.mnuSave.Click += new System.EventHandler(this.mnuSave_Click);
            // 
            // mnuSaveAs
            // 
            this.mnuSaveAs.Name = "mnuSaveAs";
            this.mnuSaveAs.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.mnuSaveAs.Size = new System.Drawing.Size(164, 22);
            this.mnuSaveAs.Text = "Save As...";
            this.mnuSaveAs.Click += new System.EventHandler(this.mnuSaveAs_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(161, 6);
            // 
            // mnuPrint
            // 
            this.mnuPrint.Image = ((System.Drawing.Image)(resources.GetObject("mnuPrint.Image")));
            this.mnuPrint.Name = "mnuPrint";
            this.mnuPrint.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.mnuPrint.Size = new System.Drawing.Size(164, 22);
            this.mnuPrint.Text = "Print...";
            this.mnuPrint.Click += new System.EventHandler(this.mnuPrint_Click);
            // 
            // mnuPrintPreview
            // 
            this.mnuPrintPreview.Image = ((System.Drawing.Image)(resources.GetObject("mnuPrintPreview.Image")));
            this.mnuPrintPreview.Name = "mnuPrintPreview";
            this.mnuPrintPreview.Size = new System.Drawing.Size(164, 22);
            this.mnuPrintPreview.Text = "Print preview...";
            this.mnuPrintPreview.Click += new System.EventHandler(this.mnuPrintPreview_Click);
            // 
            // mnuPageSettings
            // 
            this.mnuPageSettings.Name = "mnuPageSettings";
            this.mnuPageSettings.Size = new System.Drawing.Size(164, 22);
            this.mnuPageSettings.Text = "Page settings...";
            this.mnuPageSettings.Click += new System.EventHandler(this.mnuPageSettings_Click);
            // 
            // mnuEdit
            // 
            this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuUndo,
            this.mnuRedo,
            this.toolStripMenuItem2,
            this.mnuCut,
            this.mnuCopy,
            this.mnuPaste,
            this.toolStripMenuItem3,
            this.mnuFind});
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(37, 19);
            this.mnuEdit.Text = "Edit";
            // 
            // mnuUndo
            // 
            this.mnuUndo.Image = ((System.Drawing.Image)(resources.GetObject("mnuUndo.Image")));
            this.mnuUndo.Name = "mnuUndo";
            this.mnuUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.mnuUndo.Size = new System.Drawing.Size(155, 22);
            this.mnuUndo.Text = "Undo";
            this.mnuUndo.Click += new System.EventHandler(this.mnuUndo_Click);
            // 
            // mnuRedo
            // 
            this.mnuRedo.Image = ((System.Drawing.Image)(resources.GetObject("mnuRedo.Image")));
            this.mnuRedo.Name = "mnuRedo";
            this.mnuRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.mnuRedo.Size = new System.Drawing.Size(155, 22);
            this.mnuRedo.Text = "Redo";
            this.mnuRedo.Click += new System.EventHandler(this.mnuRedo_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(152, 6);
            // 
            // mnuCut
            // 
            this.mnuCut.Image = ((System.Drawing.Image)(resources.GetObject("mnuCut.Image")));
            this.mnuCut.Name = "mnuCut";
            this.mnuCut.Size = new System.Drawing.Size(155, 22);
            this.mnuCut.Text = "Cut";
            this.mnuCut.Click += new System.EventHandler(this.mnuCut_Click);
            // 
            // mnuCopy
            // 
            this.mnuCopy.Image = ((System.Drawing.Image)(resources.GetObject("mnuCopy.Image")));
            this.mnuCopy.Name = "mnuCopy";
            this.mnuCopy.Size = new System.Drawing.Size(155, 22);
            this.mnuCopy.Text = "Copy";
            this.mnuCopy.Click += new System.EventHandler(this.mnuCopy_Click);
            // 
            // mnuPaste
            // 
            this.mnuPaste.Image = ((System.Drawing.Image)(resources.GetObject("mnuPaste.Image")));
            this.mnuPaste.Name = "mnuPaste";
            this.mnuPaste.Size = new System.Drawing.Size(155, 22);
            this.mnuPaste.Text = "Paste";
            this.mnuPaste.Click += new System.EventHandler(this.mnuPaste_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(152, 6);
            // 
            // mnuFind
            // 
            this.mnuFind.Image = ((System.Drawing.Image)(resources.GetObject("mnuFind.Image")));
            this.mnuFind.Name = "mnuFind";
            this.mnuFind.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.mnuFind.Size = new System.Drawing.Size(155, 22);
            this.mnuFind.Text = "Find...";
            this.mnuFind.Click += new System.EventHandler(this.mnuFind_Click);
            // 
            // mnuView
            // 
            this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRuler,
            this.mnuMainToolbar,
            this.mnuFormatting});
            this.mnuView.Name = "mnuView";
            this.mnuView.Size = new System.Drawing.Size(41, 19);
            this.mnuView.Text = "View";
            // 
            // mnuRuler
            // 
            this.mnuRuler.Name = "mnuRuler";
            this.mnuRuler.Size = new System.Drawing.Size(144, 22);
            this.mnuRuler.Text = "Ruler";
            this.mnuRuler.Click += new System.EventHandler(this.mnuRuler_Click);
            // 
            // mnuMainToolbar
            // 
            this.mnuMainToolbar.Name = "mnuMainToolbar";
            this.mnuMainToolbar.Size = new System.Drawing.Size(144, 22);
            this.mnuMainToolbar.Text = "Main toolbar";
            this.mnuMainToolbar.Click += new System.EventHandler(this.mnuMainToolbar_Click);
            // 
            // mnuFormatting
            // 
            this.mnuFormatting.Name = "mnuFormatting";
            this.mnuFormatting.Size = new System.Drawing.Size(144, 22);
            this.mnuFormatting.Text = "Formatting";
            this.mnuFormatting.Click += new System.EventHandler(this.mnuFormatting_Click);
            // 
            // mnuInsert
            // 
            this.mnuInsert.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuInsertPicture,
            this.mnuInsertDateTime});
            this.mnuInsert.Name = "mnuInsert";
            this.mnuInsert.Size = new System.Drawing.Size(48, 19);
            this.mnuInsert.Text = "Insert";
            // 
            // mnuInsertPicture
            // 
            this.mnuInsertPicture.Image = ((System.Drawing.Image)(resources.GetObject("mnuInsertPicture.Image")));
            this.mnuInsertPicture.Name = "mnuInsertPicture";
            this.mnuInsertPicture.Size = new System.Drawing.Size(134, 22);
            this.mnuInsertPicture.Text = "Picture...";
            this.mnuInsertPicture.Click += new System.EventHandler(this.mnuInsertPicture_Click);
            // 
            // mnuInsertDateTime
            // 
            this.mnuInsertDateTime.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmbDateTimeFormats,
            this.toolStripSeparator1,
            this.txtCustom});
            this.mnuInsertDateTime.Image = ((System.Drawing.Image)(resources.GetObject("mnuInsertDateTime.Image")));
            this.mnuInsertDateTime.Name = "mnuInsertDateTime";
            this.mnuInsertDateTime.Size = new System.Drawing.Size(134, 22);
            this.mnuInsertDateTime.Text = "Date/Time";
            this.mnuInsertDateTime.DropDownOpening += new System.EventHandler(this.mnuInsertDateTime_DropDownOpening);
            // 
            // cmbDateTimeFormats
            // 
            this.cmbDateTimeFormats.AutoSize = false;
            this.cmbDateTimeFormats.BackColor = System.Drawing.Color.OldLace;
            this.cmbDateTimeFormats.DropDownHeight = 200;
            this.cmbDateTimeFormats.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDateTimeFormats.IntegralHeight = false;
            this.cmbDateTimeFormats.Name = "cmbDateTimeFormats";
            this.cmbDateTimeFormats.Size = new System.Drawing.Size(200, 21);
            this.cmbDateTimeFormats.SelectedIndexChanged += new System.EventHandler(this.cmbDateTimeFormats_SelectedIndexChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(257, 6);
            // 
            // txtCustom
            // 
            this.txtCustom.AutoSize = false;
            this.txtCustom.BackColor = System.Drawing.Color.OldLace;
            this.txtCustom.Name = "txtCustom";
            this.txtCustom.Size = new System.Drawing.Size(200, 21);
            this.txtCustom.Text = "specify custom date/time format";
            this.txtCustom.Leave += new System.EventHandler(this.txtCustom_Leave);
            this.txtCustom.Enter += new System.EventHandler(this.txtCustom_Enter);
            this.txtCustom.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtCustom_MouseDown);
            this.txtCustom.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCustom_KeyUp);
            // 
            // mnuFormat
            // 
            this.mnuFormat.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFont,
            this.mnuColors});
            this.mnuFormat.Name = "mnuFormat";
            this.mnuFormat.Size = new System.Drawing.Size(53, 19);
            this.mnuFormat.Text = "Format";
            // 
            // mnuFont
            // 
            this.mnuFont.Image = ((System.Drawing.Image)(resources.GetObject("mnuFont.Image")));
            this.mnuFont.Name = "mnuFont";
            this.mnuFont.Size = new System.Drawing.Size(119, 22);
            this.mnuFont.Text = "Font...";
            this.mnuFont.Click += new System.EventHandler(this.mnuFont_Click);
            // 
            // mnuColors
            // 
            this.mnuColors.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTextColor,
            this.mnuHighlightColor});
            this.mnuColors.Name = "mnuColors";
            this.mnuColors.Size = new System.Drawing.Size(119, 22);
            this.mnuColors.Text = "Colors";
            // 
            // mnuTextColor
            // 
            this.mnuTextColor.Image = ((System.Drawing.Image)(resources.GetObject("mnuTextColor.Image")));
            this.mnuTextColor.Name = "mnuTextColor";
            this.mnuTextColor.Size = new System.Drawing.Size(164, 22);
            this.mnuTextColor.Text = "Text color...";
            this.mnuTextColor.Click += new System.EventHandler(this.mnuTextColor_Click);
            // 
            // mnuHighlightColor
            // 
            this.mnuHighlightColor.Image = ((System.Drawing.Image)(resources.GetObject("mnuHighlightColor.Image")));
            this.mnuHighlightColor.Name = "mnuHighlightColor";
            this.mnuHighlightColor.Size = new System.Drawing.Size(164, 22);
            this.mnuHighlightColor.Text = "Highlight color...";
            this.mnuHighlightColor.Click += new System.EventHandler(this.mnuHighlightColor_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(40, 19);
            this.mnuHelp.Text = "Help";
            // 
            // mnuAbout
            // 
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(114, 22);
            this.mnuAbout.Text = "About";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // TextEditor
            // 
            this.TextEditor.AcceptsTab = true;
            this.TextEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextEditor.HideSelection = false;
            this.TextEditor.Location = new System.Drawing.Point(3, 111);
            this.TextEditor.Name = "TextEditor";
            this.TextEditor.Size = new System.Drawing.Size(681, 388);
            this.TextEditor.TabIndex = 3;
            this.TextEditor.Text = "";
            this.TextEditor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextEditor_KeyDown);
            this.TextEditor.SelectionChanged += new System.EventHandler(this.TextEditor_SelectionChanged);
            // 
            // pnlFormattingToolbar
            // 
            this.pnlFormattingToolbar.Controls.Add(this.Toolbox_Formatting);
            this.pnlFormattingToolbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFormattingToolbar.Location = new System.Drawing.Point(0, 49);
            this.pnlFormattingToolbar.Margin = new System.Windows.Forms.Padding(0);
            this.pnlFormattingToolbar.Name = "pnlFormattingToolbar";
            this.pnlFormattingToolbar.Size = new System.Drawing.Size(687, 26);
            this.pnlFormattingToolbar.TabIndex = 6;
            // 
            // Toolbox_Formatting
            // 
            this.Toolbox_Formatting.BackColor = System.Drawing.Color.Transparent;
            this.Toolbox_Formatting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Toolbox_Formatting.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmbFontName,
            this.cmbFontSize,
            this.sepTBFormatting1,
            this.btnBold,
            this.btnItalic,
            this.btnUnderline,
            this.btnStrikeThrough,
            this.sepTBFormatting2,
            this.btnAlignLeft,
            this.btnAlignCenter,
            this.btnAlignRight});
            this.Toolbox_Formatting.Location = new System.Drawing.Point(0, 0);
            this.Toolbox_Formatting.Name = "Toolbox_Formatting";
            this.Toolbox_Formatting.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.Toolbox_Formatting.Size = new System.Drawing.Size(687, 26);
            this.Toolbox_Formatting.TabIndex = 4;
            this.Toolbox_Formatting.Text = "toolStrip1";
            // 
            // cmbFontName
            // 
            this.cmbFontName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbFontName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbFontName.DropDownHeight = 300;
            this.cmbFontName.IntegralHeight = false;
            this.cmbFontName.Name = "cmbFontName";
            this.cmbFontName.Size = new System.Drawing.Size(170, 26);
            this.cmbFontName.SelectedIndexChanged += new System.EventHandler(this.cmbFontName_SelectedIndexChanged);
            this.cmbFontName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbFontName_KeyUp);
            // 
            // cmbFontSize
            // 
            this.cmbFontSize.AutoSize = false;
            this.cmbFontSize.DropDownHeight = 200;
            this.cmbFontSize.IntegralHeight = false;
            this.cmbFontSize.Items.AddRange(new object[] {
            "8",
            "9",
            "10",
            "11",
            "12",
            "14",
            "16",
            "18",
            "20",
            "22",
            "24",
            "26",
            "30",
            "36",
            "48",
            "60",
            "72",
            "85",
            "100"});
            this.cmbFontSize.MaxDropDownItems = 20;
            this.cmbFontSize.Name = "cmbFontSize";
            this.cmbFontSize.Size = new System.Drawing.Size(50, 21);
            this.cmbFontSize.Text = "8";
            this.cmbFontSize.SelectedIndexChanged += new System.EventHandler(this.cmbFontSize_SelectedIndexChanged);
            this.cmbFontSize.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbFontSize_KeyUp);
            this.cmbFontSize.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbFontSize_KeyDown);
            // 
            // sepTBFormatting1
            // 
            this.sepTBFormatting1.Name = "sepTBFormatting1";
            this.sepTBFormatting1.Size = new System.Drawing.Size(6, 26);
            // 
            // btnBold
            // 
            this.btnBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBold.Image = ((System.Drawing.Image)(resources.GetObject("btnBold.Image")));
            this.btnBold.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBold.Name = "btnBold";
            this.btnBold.Size = new System.Drawing.Size(23, 23);
            this.btnBold.Click += new System.EventHandler(this.btnBold_Click);
            // 
            // btnItalic
            // 
            this.btnItalic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnItalic.Image = ((System.Drawing.Image)(resources.GetObject("btnItalic.Image")));
            this.btnItalic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnItalic.Name = "btnItalic";
            this.btnItalic.Size = new System.Drawing.Size(23, 23);
            this.btnItalic.Click += new System.EventHandler(this.btnItalic_Click);
            // 
            // btnUnderline
            // 
            this.btnUnderline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnUnderline.Image = ((System.Drawing.Image)(resources.GetObject("btnUnderline.Image")));
            this.btnUnderline.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUnderline.Name = "btnUnderline";
            this.btnUnderline.Size = new System.Drawing.Size(23, 23);
            this.btnUnderline.Click += new System.EventHandler(this.btnUnderline_Click);
            // 
            // btnStrikeThrough
            // 
            this.btnStrikeThrough.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnStrikeThrough.Image = ((System.Drawing.Image)(resources.GetObject("btnStrikeThrough.Image")));
            this.btnStrikeThrough.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStrikeThrough.Name = "btnStrikeThrough";
            this.btnStrikeThrough.Size = new System.Drawing.Size(23, 23);
            this.btnStrikeThrough.Click += new System.EventHandler(this.btnStrikeThrough_Click);
            // 
            // sepTBFormatting2
            // 
            this.sepTBFormatting2.Name = "sepTBFormatting2";
            this.sepTBFormatting2.Size = new System.Drawing.Size(6, 26);
            // 
            // btnAlignLeft
            // 
            this.btnAlignLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAlignLeft.Image = ((System.Drawing.Image)(resources.GetObject("btnAlignLeft.Image")));
            this.btnAlignLeft.ImageTransparentColor = System.Drawing.Color.Black;
            this.btnAlignLeft.Name = "btnAlignLeft";
            this.btnAlignLeft.Size = new System.Drawing.Size(23, 23);
            this.btnAlignLeft.Click += new System.EventHandler(this.btnAlignLeft_Click);
            // 
            // btnAlignCenter
            // 
            this.btnAlignCenter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAlignCenter.Image = ((System.Drawing.Image)(resources.GetObject("btnAlignCenter.Image")));
            this.btnAlignCenter.ImageTransparentColor = System.Drawing.Color.Black;
            this.btnAlignCenter.Name = "btnAlignCenter";
            this.btnAlignCenter.Size = new System.Drawing.Size(23, 23);
            this.btnAlignCenter.Click += new System.EventHandler(this.btnAlignCenter_Click);
            // 
            // btnAlignRight
            // 
            this.btnAlignRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAlignRight.Image = ((System.Drawing.Image)(resources.GetObject("btnAlignRight.Image")));
            this.btnAlignRight.ImageTransparentColor = System.Drawing.Color.Black;
            this.btnAlignRight.Name = "btnAlignRight";
            this.btnAlignRight.Size = new System.Drawing.Size(23, 23);
            this.btnAlignRight.Click += new System.EventHandler(this.btnAlignRight_Click);
            // 
            // Ruler
            // 
            this.Ruler.BackColor = System.Drawing.Color.Transparent;
            this.Ruler.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Ruler.Font = new System.Drawing.Font("Arial", 7.25F);
            this.Ruler.LeftMargin = 1;
            this.Ruler.Location = new System.Drawing.Point(3, 85);
            this.Ruler.Name = "Ruler";
            this.Ruler.NoMargins = true;
            this.Ruler.RightMargin = 1;
            this.Ruler.Size = new System.Drawing.Size(681, 20);
            this.Ruler.TabIndex = 7;
            this.Ruler.TabsEnabled = true;
            this.Ruler.TabAdded += new TextRuler.TextRulerControl.TextRuler.TabChangedEventHandler(this.Ruler_TabAdded);
            this.Ruler.TabRemoved += new TextRuler.TextRulerControl.TextRuler.TabChangedEventHandler(this.Ruler_TabRemoved);
            this.Ruler.RightIndentChanging += new TextRuler.TextRulerControl.TextRuler.IndentChangedEventHandler(this.Ruler_RightIndentChanging);
            this.Ruler.LeftHangingIndentChanging += new TextRuler.TextRulerControl.TextRuler.IndentChangedEventHandler(this.Ruler_LeftHangingIndentChanging);
            this.Ruler.TabChanged += new TextRuler.TextRulerControl.TextRuler.TabChangedEventHandler(this.Ruler_TabChanged);
            this.Ruler.LeftIndentChanging += new TextRuler.TextRulerControl.TextRuler.IndentChangedEventHandler(this.Ruler_LeftIndentChanging);
            // 
            // prtDoc
            // 
            this.prtDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.prtDoc_PrintPage);
            this.prtDoc.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.prtDoc_BeginPrint);
            // 
            // DocPreview
            // 
            this.DocPreview.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.DocPreview.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.DocPreview.ClientSize = new System.Drawing.Size(400, 300);
            this.DocPreview.Document = this.prtDoc;
            this.DocPreview.Enabled = true;
            this.DocPreview.Icon = ((System.Drawing.Icon)(resources.GetObject("DocPreview.Icon")));
            this.DocPreview.Name = "DocPreview";
            this.DocPreview.Visible = false;
            // 
            // PageSettings
            // 
            this.PageSettings.Document = this.prtDoc;
            // 
            // PrintWnd
            // 
            this.PrintWnd.Document = this.prtDoc;
            this.PrintWnd.UseEXDialog = true;
            // 
            // AdvancedTextEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpEditorLayout);
            this.Name = "AdvancedTextEditor";
            this.Size = new System.Drawing.Size(687, 502);
            this.Load += new System.EventHandler(this.AdvancedTextEditor_Load);
            this.tlpEditorLayout.ResumeLayout(false);
            this.tlpEditorLayout.PerformLayout();
            this.Toolbox_Main.ResumeLayout(false);
            this.Toolbox_Main.PerformLayout();
            this.TextEditorMenu.ResumeLayout(false);
            this.TextEditorMenu.PerformLayout();
            this.pnlFormattingToolbar.ResumeLayout(false);
            this.pnlFormattingToolbar.PerformLayout();
            this.Toolbox_Formatting.ResumeLayout(false);
            this.Toolbox_Formatting.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpEditorLayout;        
        private System.Windows.Forms.ToolStrip Toolbox_Main;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripButton btnOpen;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripSeparator sepTBMain1;
        private System.Windows.Forms.ToolStripButton btnPrint;
        private System.Windows.Forms.ToolStripButton btnPrintPreview;
        private System.Windows.Forms.ToolStripSeparator sepTBMain2;
        private System.Windows.Forms.ToolStripButton btnCut;
        private System.Windows.Forms.ToolStripButton btnCopy;
        private System.Windows.Forms.ToolStripButton btnPaste;
        private System.Windows.Forms.ToolStripSeparator sepTBMain3;
        private System.Windows.Forms.MenuStrip TextEditorMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuNew;
        private System.Windows.Forms.ToolStripMenuItem mnuOpen;
        private System.Windows.Forms.ToolStripMenuItem mnuSave;
        private System.Windows.Forms.ToolStripMenuItem mnuSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuPrint;
        private System.Windows.Forms.ToolStripMenuItem mnuPrintPreview;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit;
        private System.Windows.Forms.ToolStripMenuItem mnuUndo;
        private System.Windows.Forms.ToolStripMenuItem mnuRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mnuCut;
        private System.Windows.Forms.ToolStripMenuItem mnuCopy;
        private System.Windows.Forms.ToolStripMenuItem mnuPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem mnuFind;
        private System.Windows.Forms.ToolStripMenuItem mnuInsert;
        private System.Windows.Forms.ToolStripMenuItem mnuInsertPicture;
        private System.Windows.Forms.ToolStripMenuItem mnuInsertDateTime;
        private System.Windows.Forms.ToolStripMenuItem mnuFormat;
        private System.Windows.Forms.ToolStripMenuItem mnuFont;
        private System.Windows.Forms.ToolStripMenuItem mnuColors;
        private System.Windows.Forms.ToolStripMenuItem mnuTextColor;
        private System.Windows.Forms.ToolStripMenuItem mnuHighlightColor;
        private System.Windows.Forms.ToolStrip Toolbox_Formatting;
        private System.Windows.Forms.ToolStripComboBox cmbFontName;
        private System.Windows.Forms.ToolStripComboBox cmbFontSize;
        private System.Windows.Forms.ToolStripSeparator sepTBFormatting1;
        private System.Windows.Forms.ToolStripButton btnBold;
        private System.Windows.Forms.ToolStripButton btnItalic;
        private System.Windows.Forms.ToolStripButton btnUnderline;
        private System.Windows.Forms.ToolStripButton btnStrikeThrough;
        private System.Windows.Forms.ToolStripSeparator sepTBFormatting2;
        private System.Windows.Forms.ToolStripButton btnAlignLeft;
        private System.Windows.Forms.ToolStripButton btnAlignCenter;
        private System.Windows.Forms.ToolStripButton btnAlignRight;
        private System.Windows.Forms.ToolStripButton btnUndo;
        private System.Windows.Forms.ToolStripButton btnRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.Panel pnlFormattingToolbar;
        private TextRuler.TextRulerControl.TextRuler Ruler;
        private System.Windows.Forms.ToolStripMenuItem mnuView;
        private System.Windows.Forms.ToolStripMenuItem mnuRuler;
        private System.Windows.Forms.ToolStripMenuItem mnuMainToolbar;
        private System.Windows.Forms.ToolStripMenuItem mnuFormatting;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Drawing.Printing.PrintDocument prtDoc;
        private System.Windows.Forms.PrintPreviewDialog DocPreview;
        private System.Windows.Forms.PageSetupDialog PageSettings;
        private System.Windows.Forms.ToolStripMenuItem mnuPageSettings;
        private System.Windows.Forms.PrintDialog PrintWnd;
        private System.Windows.Forms.ToolStripComboBox cmbDateTimeFormats;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripTextBox txtCustom;
        private System.Windows.Forms.ToolTip toolTip1;
        internal ExtendedRichTextBox TextEditor;
    }
}
