using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;


namespace YouWrite.AdvancedTextEditorControl
{
    internal partial class AdvancedTextEditor : UserControl
    {

        string _path = "";
        int checkPrint = 0;
        public event EventHandler textboxchanged;
        public event EventHandler textboxclicked;
        public event EventHandler keyup;

        private string GetFilePath()
        {
           
            OpenFileDialog o = new OpenFileDialog();
            o.Multiselect = false;
            o.RestoreDirectory = true;
            o.ShowReadOnly = false;
            o.ReadOnlyChecked = false;
            o.Filter = "RTF (*.rtf)|*.rtf|TXT (*.txt)|*.txt";
            if (o.ShowDialog(this) == DialogResult.OK)
            {
                return o.FileName;
            }
            else
            {
                return "";
            }
        }
        private string SetFilePath()
        {
            SaveFileDialog s = new SaveFileDialog();
            s.Filter = "RTF (*.rtf)|*.rtf|TXT (*.txt)|*.txt";
            if (s.ShowDialog(this) == DialogResult.OK)
            {
                return s.FileName;
            }
            else
            {
                return "";
            }
        }
        private Color GetColor(Color initColor)
        {
            using (ColorDialog cd = new ColorDialog())
            {
                cd.Color = initColor;
                cd.AllowFullOpen = true;
                cd.AnyColor = true;
                cd.FullOpen = true;
                cd.ShowHelp = false;
                cd.SolidColorOnly = false;
                if (cd.ShowDialog() == DialogResult.OK)
                    return cd.Color;
                else
                    return initColor;
            }
        }
        private Font GetFont(Font initFont)
        {
            using (FontDialog fd = new FontDialog())
            {
                fd.Font = initFont;
                fd.AllowSimulations = true;
                fd.AllowVectorFonts = true;
                fd.AllowVerticalFonts = true;
                fd.FontMustExist = true;
                fd.ShowHelp = false;
                fd.ShowEffects = true;
                fd.ShowColor = false;
                fd.ShowApply = false;
                fd.FixedPitchOnly = false;

                if (fd.ShowDialog() == DialogResult.OK)
                    return fd.Font;
                else
                    return initFont;
            }
        }
        private string GetImagePath()
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Multiselect = false;
            o.ShowReadOnly = false;
            o.RestoreDirectory = true;
            o.ReadOnlyChecked = false;
            o.Filter = "Images|*.png;*.bmp;*.jpg;*.jpeg;*.gif;*.tif;*.tiff,*.wmf;*.emf";
            if (o.ShowDialog(this) == DialogResult.OK)
            {
                return o.FileName;
            }
            else
            {
                return "";
            }
        }

        private void Clear()
        {
            _path = "";
            this.TextEditor.Clear();

            //set indents to default positions
            this.TextEditor.Select(0, 0);
            this.Ruler.LeftIndent = 0;
            this.Ruler.LeftHangingIndent = 0;
            this.Ruler.RightIndent = 0;
            this.TextEditor.SelectionIndent = 0;
            this.TextEditor.SelectionRightIndent = 0;
            this.TextEditor.SelectionHangingIndent = 0;

            //clear tabs on the ruler
            this.Ruler.SetTabPositionsInPixels(null);
            this.TextEditor.SelectionTabs = null;

            ExtendedRichTextBox.ParaListStyle pls = new ExtendedRichTextBox.ParaListStyle();

            pls.Type = ExtendedRichTextBox.ParaListStyle.ListType.None;
            pls.Style = ExtendedRichTextBox.ParaListStyle.ListStyle.NumberAndParenthesis;

            this.TextEditor.SelectionListType = pls;
        }
        private void Open()
        {
            try
            {
                string file = GetFilePath();

                if (file != "")
                {
                    Clear();
                    try
                    {
                        this.TextEditor.Rtf = System.IO.File.ReadAllText(file, System.Text.Encoding.Default);
                    }
                    catch (Exception) //error occured, that means we loaded invalid RTF, so load as plain text
                    {
                        this.TextEditor.Text = System.IO.File.ReadAllText(file, System.Text.Encoding.Default);
                    }
                    _path = file;
                }
                file = null;
            }
            catch (Exception)
            {
                Clear();
            }
        }
        private void Save(bool SaveAs)
        {
            try
            {
                if (SaveAs == true)
                {
                    string file = SetFilePath();

                    if (file != "")
                    {
                        this.TextEditor.SaveFile(file, RichTextBoxStreamType.RichText);
                        _path = file;
                        file = null;
                    }
                }
                else
                {
                    if (_path == "")
                    {
                        string file = SetFilePath();

                        if (file != "")
                        {
                            this.TextEditor.SaveFile(file, RichTextBoxStreamType.RichText);
                            _path = file;
                            file = null;
                        }
                    }
                    else
                    {
                        this.TextEditor.SaveFile(_path, RichTextBoxStreamType.RichText);
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        
        
        public AdvancedTextEditor()
        {
            InitializeComponent();
            this.autocompleteMenu1.Show(this.TextEditor, false);
            this.mnuRuler.Checked = true;
            this.mnuMainToolbar.Checked = true;
            this.mnuFormatting.Checked = true;

            System.Drawing.Text.InstalledFontCollection col = new System.Drawing.Text.InstalledFontCollection();

            this.cmbFontName.Items.Clear();

            foreach (FontFamily ff in col.Families)
            {
                this.cmbFontName.Items.Add(ff.Name);
            }

            col.Dispose();

            this.TextEditor.Select(0, 0);
            this.Ruler.LeftIndent = 0;
            this.Ruler.LeftHangingIndent = 0;
            this.Ruler.RightIndent = 0;
            this.TextEditor.SelectionIndent = 0;
            this.TextEditor.SelectionRightIndent = 0;
            this.TextEditor.SelectionHangingIndent = 0;            
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void mnuNew_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnCut_Click(object sender, EventArgs e)
        {
            this.TextEditor.Cut();
        }

        private void mnuCut_Click(object sender, EventArgs e)
        {
            this.TextEditor.Cut();
        }

        private void mnuCopy_Click(object sender, EventArgs e)
        {
            this.TextEditor.Copy();
        }

        private void mnuPaste_Click(object sender, EventArgs e)
        {
            this.TextEditor.Paste();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            this.TextEditor.Copy();
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            this.TextEditor.Paste();
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            this.TextEditor.Undo();
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            this.TextEditor.Redo();
        }

        private void mnuUndo_Click(object sender, EventArgs e)
        {
            this.TextEditor.Undo();
        }

        private void mnuRedo_Click(object sender, EventArgs e)
        {
            this.TextEditor.Redo();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void mnuOpen_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void TextEditor_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                #region Alignment
                if (TextEditor.SelectionAlignment == ExtendedRichTextBox.RichTextAlign.Left)
                {
                    this.btnAlignLeft.Checked = true;
                    this.btnAlignCenter.Checked = false;
                    this.btnAlignRight.Checked = false;
                    this.btnJustify.Checked = false;
                }
                else if (TextEditor.SelectionAlignment == ExtendedRichTextBox.RichTextAlign.Center)
                {
                    this.btnAlignLeft.Checked = false;
                    this.btnAlignCenter.Checked = true;
                    this.btnAlignRight.Checked = false;
                    this.btnJustify.Checked = false;
                }
                else if (TextEditor.SelectionAlignment == ExtendedRichTextBox.RichTextAlign.Right)
                {
                    this.btnAlignLeft.Checked = false;
                    this.btnAlignCenter.Checked = false;
                    this.btnAlignRight.Checked = true;
                    this.btnJustify.Checked = false;
                }
                else if (TextEditor.SelectionAlignment == ExtendedRichTextBox.RichTextAlign.Justify)
                {
                    this.btnAlignLeft.Checked = false;
                    this.btnAlignRight.Checked = false;
                    this.btnAlignCenter.Checked = false;
                    this.btnJustify.Checked = true;
                }
                else
                {
                    this.btnAlignLeft.Checked = true;
                    this.btnAlignCenter.Checked = false;
                    this.btnAlignRight.Checked = false;
                }

                #endregion

                #region Tab positions
                this.Ruler.SetTabPositionsInPixels(this.TextEditor.SelectionTabs);
                #endregion

                #region Font
                try
                {
                    this.cmbFontSize.Text = Convert.ToInt32(this.TextEditor.SelectionFont2.Size).ToString();
                }
                catch
                {
                    this.cmbFontSize.Text = "";
                }

                try
                {
                    this.cmbFontName.Text = this.TextEditor.SelectionFont2.Name;
                }
                catch
                {
                    this.cmbFontName.Text = "";
                }

                if (this.cmbFontName.Text != "")
                {
                    FontFamily ff = new FontFamily(this.cmbFontName.Text);
                    if (ff.IsStyleAvailable(FontStyle.Bold) == true)
                    {
                        this.btnBold.Enabled = true;
                        this.btnBold.Checked = this.TextEditor.SelectionCharStyle.Bold;
                    }
                    else
                    {
                        this.btnBold.Enabled = false;
                        this.btnBold.Checked = false;
                    }

                    if (ff.IsStyleAvailable(FontStyle.Italic) == true)
                    {
                        this.btnItalic.Enabled = true;
                        this.btnItalic.Checked = this.TextEditor.SelectionCharStyle.Italic;
                    }
                    else
                    {
                        this.btnItalic.Enabled = false;
                        this.btnItalic.Checked = false;
                    }

                    if (ff.IsStyleAvailable(FontStyle.Underline) == true)
                    {
                        this.btnUnderline.Enabled = true;
                        this.btnUnderline.Checked = this.TextEditor.SelectionCharStyle.Underline;
                    }
                    else
                    {
                        this.btnUnderline.Enabled = false;
                        this.btnUnderline.Checked = false;
                    }

                    if (ff.IsStyleAvailable(FontStyle.Strikeout) == true)
                    {
                        this.btnStrikeThrough.Enabled = true;
                        this.btnStrikeThrough.Checked = this.TextEditor.SelectionCharStyle.Strikeout;
                    }
                    else
                    {
                        this.btnStrikeThrough.Enabled = false;
                        this.btnStrikeThrough.Checked = false;
                    }

                    ff.Dispose();
                }
                else
                {
                    this.btnBold.Checked = false;
                    this.btnItalic.Checked = false;
                    this.btnUnderline.Checked = false;
                    this.btnStrikeThrough.Checked = false;
                }
                #endregion

                if (this.TextEditor.SelectionLength < this.TextEditor.TextLength - 1)
                {
                    this.Ruler.LeftIndent = (int)(this.TextEditor.SelectionIndent / this.Ruler.DotsPerMillimeter); //convert pixels to millimeter

                    this.Ruler.LeftHangingIndent = (int)((float)this.TextEditor.SelectionHangingIndent / this.Ruler.DotsPerMillimeter) + this.Ruler.LeftIndent; //convert pixels to millimeters

                    this.Ruler.RightIndent = (int)(this.TextEditor.SelectionRightIndent / this.Ruler.DotsPerMillimeter); //convert pixels to millimeters                
                }

                switch (this.TextEditor.SelectionListType.Type)
                {
                    case ExtendedRichTextBox.ParaListStyle.ListType.None:
                        this.btnNumberedList.Checked = false;
                        this.btnBulletedList.Checked = false;
                        break;
                    case ExtendedRichTextBox.ParaListStyle.ListType.SmallLetters:
                        this.btnNumberedList.Checked = false;
                        this.btnBulletedList.Checked = false;
                        break;
                    case ExtendedRichTextBox.ParaListStyle.ListType.CapitalLetters:
                        this.btnNumberedList.Checked = false;
                        this.btnBulletedList.Checked = false;
                        break;
                    case ExtendedRichTextBox.ParaListStyle.ListType.SmallRoman:
                        this.btnNumberedList.Checked = false;
                        this.btnBulletedList.Checked = false;
                        break;
                    case ExtendedRichTextBox.ParaListStyle.ListType.CapitalRoman:
                        this.btnNumberedList.Checked = false;
                        this.btnBulletedList.Checked = false;
                        break;
                    case ExtendedRichTextBox.ParaListStyle.ListType.Bullet:
                        this.btnNumberedList.Checked = false;
                        this.btnBulletedList.Checked = true;
                        break;
                    case ExtendedRichTextBox.ParaListStyle.ListType.Numbers:
                        this.btnNumberedList.Checked = true;
                        this.btnBulletedList.Checked = false;
                        break;
                    case ExtendedRichTextBox.ParaListStyle.ListType.CharBullet:
                        this.btnNumberedList.Checked = true;
                        this.btnBulletedList.Checked = false;
                        break;
                    default:
                        break;
                }

                this.TextEditor.UpdateObjects();                
            }
            catch (Exception)
            {
            }
        }

        private void AdvancedTextEditor_Load(object sender, EventArgs e)
        {
            //code below will cause refreshing formatting by adding and removing (changing) text
            this.TextEditor.Select(0, 0);
            this.TextEditor.AppendText("some text");
            this.TextEditor.Select(0, 0);
            this.TextEditor.Clear();
            this.TextEditor.SetLayoutType(ExtendedRichTextBox.LayoutModes.WYSIWYG);
        }

        private void cmbFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.cmbFontSize.Focused) return;
                this.TextEditor.SelectionFont2 = new Font(this.cmbFontName.Text, Convert.ToInt32(this.cmbFontSize.Text), this.TextEditor.SelectionFont.Style);
            }
            catch (Exception)
            {
                
            }
        }

        private void cmbFontSize_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    this.TextEditor.SelectionFont2 = new Font(this.cmbFontName.Text, Convert.ToSingle(this.cmbFontSize.Text));
                    this.TextEditor.Focus();
                }
                catch (Exception)
                {
                }
            }
        }

        #region Old style formatting

        private FontStyle SwitchBold()
        {
            FontStyle fs = new FontStyle();

            fs = FontStyle.Regular;

            if (this.TextEditor.SelectionFont.Italic == true)
            {
                fs = FontStyle.Italic;
            }

            if (this.TextEditor.SelectionFont.Underline == true)
            {
                fs = fs | FontStyle.Underline;
            }

            if (this.TextEditor.SelectionFont.Strikeout == true)
            {
                fs = fs | FontStyle.Strikeout;
            }

            if (this.TextEditor.SelectionFont.Bold == false)
            {
                fs = fs | FontStyle.Bold;
            }

            return fs;
        }
        private FontStyle SwitchItalic()
        {
            FontStyle fs = new FontStyle();

            fs = FontStyle.Regular;

            if (this.TextEditor.SelectionFont.Underline == true)
            {
                fs = fs | FontStyle.Underline;
            }

            if (this.TextEditor.SelectionFont.Strikeout == true)
            {
                fs = fs | FontStyle.Strikeout;
            }

            if (this.TextEditor.SelectionFont.Bold == true)
            {
                fs = fs | FontStyle.Bold;
            }

            if (this.TextEditor.SelectionFont.Italic == false)
            {
                fs = fs | FontStyle.Italic;
            }

            return fs;
        }
        private FontStyle SwitchUnderline()
        {
            FontStyle fs = new FontStyle();

            fs = FontStyle.Regular;

            if (this.TextEditor.SelectionFont.Strikeout == true)
            {
                fs = fs | FontStyle.Strikeout;
            }

            if (this.TextEditor.SelectionFont.Bold == true)
            {
                fs = fs | FontStyle.Bold;
            }

            if (this.TextEditor.SelectionFont.Italic == true)
            {
                fs = fs | FontStyle.Italic;
            }

            if (this.TextEditor.SelectionFont.Underline == false)
            {
                fs = fs | FontStyle.Underline;
            }

            return fs;
        }
        private FontStyle SwitchStrikeout()
        {
            FontStyle fs = new FontStyle();

            fs = FontStyle.Regular;

            if (this.TextEditor.SelectionFont.Bold == true)
            {
                fs = fs | FontStyle.Bold;
            }

            if (this.TextEditor.SelectionFont.Italic == true)
            {
                fs = fs | FontStyle.Italic;
            }

            if (this.TextEditor.SelectionFont.Underline == true)
            {
                fs = fs | FontStyle.Underline;
            }

            if (this.TextEditor.SelectionFont.Strikeout == false)
            {
                fs = fs | FontStyle.Strikeout;
            }

            return fs;
        }

        #endregion

        private void btnBold_Click(object sender, EventArgs e)
        {
            if (this.TextEditor.SelectionCharStyle.Bold == true)
            {
                this.btnBold.Checked = false;
                ExtendedRichTextBox.CharStyle cs = this.TextEditor.SelectionCharStyle;
                cs.Bold = false;
                this.TextEditor.SelectionCharStyle = cs;
                cs = null;
            }
            else
            {
                this.btnBold.Checked = true;
                ExtendedRichTextBox.CharStyle cs = this.TextEditor.SelectionCharStyle;
                cs.Bold = true;
                this.TextEditor.SelectionCharStyle = cs;
                cs = null;
            }
        }

        private void btnAlignLeft_Click(object sender, EventArgs e)
        {
            this.TextEditor.SelectionAlignment = ExtendedRichTextBox.RichTextAlign.Left;
            this.btnAlignLeft.Checked = true;
            this.btnAlignRight.Checked = false;
            this.btnAlignCenter.Checked = false;
            this.btnJustify.Checked = false;
        }

        private void btnAlignCenter_Click(object sender, EventArgs e)
        {
            this.TextEditor.SelectionAlignment = ExtendedRichTextBox.RichTextAlign.Center;
            this.btnAlignLeft.Checked = false;
            this.btnAlignRight.Checked = false;
            this.btnAlignCenter.Checked = true;
            this.btnJustify.Checked = false;
        }

        private void btnAlignRight_Click(object sender, EventArgs e)
        {
            this.TextEditor.SelectionAlignment = ExtendedRichTextBox.RichTextAlign.Right;
            this.btnAlignLeft.Checked = false;
            this.btnAlignRight.Checked = true;
            this.btnAlignCenter.Checked = false;
            this.btnJustify.Checked = false;
        }

        private void Ruler_LeftIndentChanging(int NewValue)
        {
            try
            {
                this.TextEditor.SelectionIndent = (int)(this.Ruler.LeftIndent * this.Ruler.DotsPerMillimeter);
                this.TextEditor.SelectionHangingIndent = (int)(this.Ruler.LeftHangingIndent * this.Ruler.DotsPerMillimeter) - (int)(this.Ruler.LeftIndent * this.Ruler.DotsPerMillimeter);                
            }
            catch (Exception)
            {
            }
        }

        private void Ruler_LeftHangingIndentChanging(int NewValue)
        {
            try
            {                
                this.TextEditor.SelectionHangingIndent = (int)(this.Ruler.LeftHangingIndent * this.Ruler.DotsPerMillimeter) - (int)(this.Ruler.LeftIndent * this.Ruler.DotsPerMillimeter);
            }
            catch (Exception)
            {
            }
        }

        private void Ruler_RightIndentChanging(int NewValue)
        {
            try
            {
                this.TextEditor.SelectionRightIndent = (int)(this.Ruler.RightIndent * this.Ruler.DotsPerMillimeter);
            }
            catch (Exception)
            {
            }
        }

        private void cmbFontName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.cmbFontName.Focused) return;
                this.TextEditor.SelectionFont2 = new Font(this.cmbFontName.Text, Convert.ToInt32(this.cmbFontSize.Text));
            }
            catch (Exception)
            {                
            }
        }

        private void cmbFontName_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.TextEditor.SelectionFont2 = new Font(this.cmbFontName.Text, Convert.ToInt32(this.cmbFontSize.Text));
                    this.TextEditor.Focus();
                }
            }
            catch (Exception)
            {                
            }
        }

        private void mnuRuler_Click(object sender, EventArgs e)
        {
            if (this.Ruler.Visible == true)
            {
                this.Ruler.Visible = false;
                this.mnuRuler.Checked = false;
            }
            else
            {
                this.Ruler.Visible = true;
                this.mnuRuler.Checked = true;
            }
        }

        private void mnuMainToolbar_Click(object sender, EventArgs e)
        {
            if (this.Toolbox_Main.Visible == true)
            {
                this.Toolbox_Main.Visible = false;
                this.mnuMainToolbar.Checked = false;
            }
            else
            {
                this.Toolbox_Main.Visible = true;
                this.mnuMainToolbar.Checked = true;
            }
        }

        private void mnuFormatting_Click(object sender, EventArgs e)
        {
            if (this.Toolbox_Formatting.Visible == true)
            {
                this.Toolbox_Formatting.Visible = false;
                this.mnuFormatting.Checked = false;
            }
            else
            {
                this.Toolbox_Formatting.Visible = true;
                this.mnuFormatting.Checked = true;
            }
        }

        private void mnuFont_Click(object sender, EventArgs e)
        {
            try
            {
                this.TextEditor.SelectionFont2 = GetFont(this.TextEditor.SelectionFont);
            }
            catch (Exception)
            {
            }
        }

        private void mnuTextColor_Click(object sender, EventArgs e)
        {
            try
            {
                this.TextEditor.SelectionColor2 = GetColor(this.TextEditor.SelectionColor);
            }
            catch (Exception)
            {
            }
        }

        private void mnuHighlightColor_Click(object sender, EventArgs e)
        {
            try
            {
                this.TextEditor.SelectionBackColor2 = GetColor(this.TextEditor.SelectionBackColor);
            }
            catch (Exception)
            {
            }
        }

        private void Ruler_TabAdded(YouWrite.TextRulerControl.TextRuler.TabEventArgs args)
        {
            try
            {
                this.TextEditor.SelectionTabs = this.Ruler.TabPositionsInPixels.ToArray();
            }
            catch (Exception)
            {
            }
        }

        private void Ruler_TabChanged(YouWrite.TextRulerControl.TextRuler.TabEventArgs args)
        {
            try
            {
                this.TextEditor.SelectionTabs = this.Ruler.TabPositionsInPixels.ToArray();
            }
            catch (Exception)
            {
            }
        }

        private void Ruler_TabRemoved(YouWrite.TextRulerControl.TextRuler.TabEventArgs args)
        {
            try
            {
                this.TextEditor.SelectionTabs = this.Ruler.TabPositionsInPixels.ToArray();
            }
            catch (Exception)
            {
            }
        }

        private void cmbFontSize_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D0 || e.KeyCode == Keys.D1 || e.KeyCode == Keys.D2 ||
                e.KeyCode == Keys.D3 || e.KeyCode == Keys.D4 || e.KeyCode == Keys.D5 ||
                e.KeyCode == Keys.D6 || e.KeyCode == Keys.D7 || e.KeyCode == Keys.D8 ||
                e.KeyCode == Keys.D9 || e.KeyCode == Keys.NumPad0 || e.KeyCode == Keys.NumPad1 ||
                e.KeyCode == Keys.NumPad2 || e.KeyCode == Keys.NumPad3 || e.KeyCode == Keys.NumPad4 ||
                e.KeyCode == Keys.NumPad5 || e.KeyCode == Keys.NumPad6 || e.KeyCode == Keys.NumPad7 ||
                e.KeyCode == Keys.NumPad8 || e.KeyCode == Keys.NumPad9 || e.KeyCode == Keys.Back ||
                e.KeyCode == Keys.Enter || e.KeyCode == Keys.Delete)
            {
                //allow key
            }
            else
            {
                e.SuppressKeyPress = true;
            }
        }

        private void mnuInsertPicture_Click(object sender, EventArgs e)
        {
            string _imgPath = GetImagePath();
            if (_imgPath == "")
                return;            
            this.TextEditor.InsertImage(_imgPath);
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("YouWrite is developped by Noureddine Haouari");
        }

        private void prtDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            checkPrint = this.TextEditor.Print(checkPrint, this.TextEditor.TextLength, e);

            if (checkPrint < this.TextEditor.TextLength)
                e.HasMorePages = true;
            else
                e.HasMorePages = false;
        }

        private void prtDoc_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            checkPrint = 0;
        }

        private void mnuPageSettings_Click(object sender, EventArgs e)
        {
            this.PageSettings.ShowDialog(this);
        }

        private void mnuPrintPreview_Click(object sender, EventArgs e)
        {
            this.DocPreview.ShowDialog(this);
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            this.DocPreview.ShowDialog(this);
        }

        delegate void printDialogHelperDelegate(); // Helper delegate for PrintDialog bug

        /// <summary>
        /// Helper thread which sole purpose is to invoke PrintDialogHelper function
        /// to circumvent the PrintDialog focus problem reported on
        /// https://connect.microsoft.com/VisualStudio/feedback/ViewFeedback.aspx?FeedbackID=234179
        /// </summary>
        private void PrintHelpThread()
        {
            if (InvokeRequired)
            {
                printDialogHelperDelegate d = new printDialogHelperDelegate(PrintHelpThread);
                Invoke(d);
            }
            else
            {
                PrintDialogHelper();
            }
        }

        /// <summary>
        /// Shows the print dialog (invoked from a different thread to get the focus to the dialog)
        /// </summary>
        private void PrintDialogHelper()
        {
            if (PrintWnd.ShowDialog(this) == DialogResult.OK)
            {
                this.prtDoc.Print();
            }
        }
        
        private void btnPrint_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(PrintHelpThread);            
            t.Start();
        }

        private void mnuPrint_Click(object sender, EventArgs e)
        {
            this.PrintWnd.ShowDialog(this);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save(false);
        }

        private void mnuSave_Click(object sender, EventArgs e)
        {
            Save(false);
        }

        private void mnuSaveAs_Click(object sender, EventArgs e)
        {
            Save(true);
        }

        private void mnuInsertDateTime_DropDownOpening(object sender, EventArgs e)
        {
            this.cmbDateTimeFormats.Items.Clear();

            this.cmbDateTimeFormats.Items.Add("Select date/time format");
            this.cmbDateTimeFormats.Items.Add(DateTime.Now.ToString("D"));
            this.cmbDateTimeFormats.Items.Add(DateTime.Now.ToString("f"));
            this.cmbDateTimeFormats.Items.Add(DateTime.Now.ToString("F"));
            this.cmbDateTimeFormats.Items.Add(DateTime.Now.ToString("g"));
            this.cmbDateTimeFormats.Items.Add(DateTime.Now.ToString("G"));
            this.cmbDateTimeFormats.Items.Add(DateTime.Now.ToString("m"));
            this.cmbDateTimeFormats.Items.Add(DateTime.Now.ToString("t"));
            this.cmbDateTimeFormats.Items.Add(DateTime.Now.ToString("T"));

            this.cmbDateTimeFormats.SelectedIndex = 0;
        }

        private void cmbDateTimeFormats_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbDateTimeFormats.SelectedIndex == 0)
                return;

            this.TextEditor.AppendText(Environment.NewLine + this.cmbDateTimeFormats.SelectedItem.ToString());
            this.mnuInsert.DropDown.Close();
        }

        private void txtCustom_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.txtCustom.Text == "")
                {
                    return;
                }

                try
                {
                    this.TextEditor.AppendText(Environment.NewLine + DateTime.Now.ToString(txtCustom.Text));
                }
                catch (Exception)
                {                    
                }
                txtCustom.Text = "specify custom date/time format";
                this.mnuInsert.DropDown.Close();
            }
        }

        private void txtCustom_Leave(object sender, EventArgs e)
        {
            txtCustom.Text = "specify custom date/time format";
            this.mnuInsert.DropDown.Close();
        }

        private void txtCustom_MouseDown(object sender, MouseEventArgs e)
        {
            txtCustom.Text = "";
        }

        private void txtCustom_Enter(object sender, EventArgs e)
        {
            txtCustom.Text = "";
        }

        private void btnItalic_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.TextEditor.SelectionCharStyle.Italic == true)
                {
                    this.btnItalic.Checked = false;
                    ExtendedRichTextBox.CharStyle cs = this.TextEditor.SelectionCharStyle;
                    cs.Italic = false;
                    this.TextEditor.SelectionCharStyle = cs;
                    cs = null;
                }
                else
                {
                    this.btnItalic.Checked = true;
                    ExtendedRichTextBox.CharStyle cs = this.TextEditor.SelectionCharStyle;
                    cs.Italic = true;
                    this.TextEditor.SelectionCharStyle = cs;
                    cs = null;
                }
            }
            catch (Exception)
            {
            }            
        }

        private void btnUnderline_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.TextEditor.SelectionCharStyle.Underline == true)
                {
                    this.btnUnderline.Checked = false;
                    ExtendedRichTextBox.CharStyle cs = this.TextEditor.SelectionCharStyle;
                    cs.Underline = false;
                    this.TextEditor.SelectionCharStyle = cs;
                    cs = null;
                }
                else
                {
                    this.btnUnderline.Checked = true;
                    ExtendedRichTextBox.CharStyle cs = this.TextEditor.SelectionCharStyle;
                    cs.Underline = true;
                    this.TextEditor.SelectionCharStyle = cs;
                    cs = null;
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnStrikeThrough_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.TextEditor.SelectionCharStyle.Strikeout == true)
                {
                    this.btnStrikeThrough.Checked = false;
                    ExtendedRichTextBox.CharStyle cs = this.TextEditor.SelectionCharStyle;
                    cs.Strikeout = false;
                    this.TextEditor.SelectionCharStyle = cs;
                    cs = null;
                }
                else
                {
                    this.btnStrikeThrough.Checked = true;
                    ExtendedRichTextBox.CharStyle cs = this.TextEditor.SelectionCharStyle;
                    cs.Strikeout = true;
                    this.TextEditor.SelectionCharStyle = cs;
                    cs = null;
                }
            }
            catch (Exception)
            {
            }
        }

        private void mnuFind_Click(object sender, EventArgs e)
        {
            Dialogs.dlgFind find = new YouWrite.Dialogs.dlgFind();
            find.txtFindThis.Text = this.TextEditor.SelectedText;
            find.Caller = this;
            find.Show(this);
        }

        private void TextEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.B && e.Control == true)
            {
                this.btnBold.PerformClick();
            }

            if (e.Control == true && e.KeyCode == Keys.I)
            {
                this.btnItalic.PerformClick();
                e.SuppressKeyPress = true;
            }

            if (e.Control == true && e.KeyCode == Keys.U)
            {
                this.btnUnderline.PerformClick();
            }
        }

        private void btnJustify_Click(object sender, EventArgs e)
        {
            this.TextEditor.SelectionAlignment = ExtendedRichTextBox.RichTextAlign.Justify;
            this.btnAlignLeft.Checked = false;
            this.btnAlignRight.Checked = false;
            this.btnAlignCenter.Checked = false;
            this.btnJustify.Checked = true;
        }

        private void Ruler_BothLeftIndentsChanged(int LeftIndent, int HangIndent)
        {
            this.TextEditor.SelectionIndent = (int)(this.Ruler.LeftIndent * this.Ruler.DotsPerMillimeter);
            this.TextEditor.SelectionHangingIndent = (int)(this.Ruler.LeftHangingIndent * this.Ruler.DotsPerMillimeter) - (int)(this.Ruler.LeftIndent * this.Ruler.DotsPerMillimeter);            
        }

        private void TextEditor_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            try
            {
                Process.Start(e.LinkText);
            }
            catch (Exception)
            {
            }
        }

        private void btnNumberedList_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.btnNumberedList.Checked)
                {
                    this.btnBulletedList.Checked = false;
                    this.btnNumberedList.Checked = false;
                    ExtendedRichTextBox.ParaListStyle pls = new ExtendedRichTextBox.ParaListStyle();

                    pls.Type = ExtendedRichTextBox.ParaListStyle.ListType.None;
                    pls.Style = ExtendedRichTextBox.ParaListStyle.ListStyle.NumberAndParenthesis;

                    this.TextEditor.SelectionListType = pls;
                }
                else
                {
                    this.btnBulletedList.Checked = false;
                    this.btnNumberedList.Checked = true;
                    ExtendedRichTextBox.ParaListStyle pls = new ExtendedRichTextBox.ParaListStyle();

                    pls.Type = ExtendedRichTextBox.ParaListStyle.ListType.Numbers;
                    pls.Style = ExtendedRichTextBox.ParaListStyle.ListStyle.NumberInPar;

                    this.TextEditor.SelectionListType = pls;
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnBulletedList_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.btnBulletedList.Checked)
                {
                    this.btnBulletedList.Checked = false;
                    this.btnNumberedList.Checked = false;
                    ExtendedRichTextBox.ParaListStyle pls = new ExtendedRichTextBox.ParaListStyle();

                    pls.Type = ExtendedRichTextBox.ParaListStyle.ListType.None;
                    pls.Style = ExtendedRichTextBox.ParaListStyle.ListStyle.NumberAndParenthesis;

                    this.TextEditor.SelectionListType = pls;
                }
                else
                {
                    this.btnBulletedList.Checked = true;
                    this.btnNumberedList.Checked = false;
                    ExtendedRichTextBox.ParaListStyle pls = new ExtendedRichTextBox.ParaListStyle();

                    pls.Type = ExtendedRichTextBox.ParaListStyle.ListType.Bullet;
                    pls.Style = ExtendedRichTextBox.ParaListStyle.ListStyle.NumberAndParenthesis;

                    this.TextEditor.SelectionListType = pls;
                }
            }
            catch (Exception)
            {
            }
        }

        private void TextEditor_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this.TextEditor.SelectionType == RichTextBoxSelectionTypes.Object ||
                    this.TextEditor.SelectionType == RichTextBoxSelectionTypes.MultiObject)
                {
                    MessageBox.Show(Convert.ToString(this.TextEditor.SelectedObject().sizel.Width));
                }
            }
        }

        private void TextEditor_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void mnuULWave_Click(object sender, EventArgs e)
        {
            this.TextEditor.SelectionUnderlineStyle = ExtendedRichTextBox.UnderlineStyle.Wave;
        }

        private void mnuULineSolid_Click(object sender, EventArgs e)
        {
            this.TextEditor.SelectionUnderlineStyle = ExtendedRichTextBox.UnderlineStyle.DashDotDot;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TextEditor_TextChanged(object sender, EventArgs e)
        {
            if (this.textboxchanged != null)
                this.textboxchanged(this, e);
            
           
        }

        private void Ruler_Load(object sender, EventArgs e)
        {

        }

        private void Toolbox_Formatting_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void TextEditor_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.keyup != null)
                this.keyup(this,e);
    }

        private void backupDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void TextEditor_Click(object sender, EventArgs e)
        {
            if (this.textboxclicked != null)
                this.textboxclicked(this, e);
            
        }

        private void pdfFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void textFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
