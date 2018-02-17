using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace TextRuler.AdvancedTextEditorControl
{
    internal partial class AdvancedTextEditor : UserControl
    {

        string _path = "";
        int checkPrint = 0;

        private string GetFilePath()
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Multiselect = false;
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
            o.ReadOnlyChecked = false;
            o.Filter = "Images|*.png;*.bmp;*.gif;*.tif;*.tiff,*.wmf;*.emf";
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
            this.Ruler.LeftIndent = 1;
            this.Ruler.LeftHangingIndent = 1;
            this.Ruler.RightIndent = 1;

            //clear tabs on the ruler
            this.Ruler.SetTabPositionsInPixels(null);
            this.TextEditor.SelectionTabs = null;
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
            //cause SelectionChange event to occur
            this.TextEditor.Select(0, 0);
            this.Ruler.LeftIndent = 1;
            this.Ruler.LeftHangingIndent = 1;
            this.Ruler.RightIndent = 1;

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
                if (TextEditor.SelectionAlignment == HorizontalAlignment.Left)
                {
                    this.btnAlignLeft.Checked = true;
                    this.btnAlignCenter.Checked = false;
                    this.btnAlignRight.Checked = false;
                }
                else if (TextEditor.SelectionAlignment == HorizontalAlignment.Center)
                {
                    this.btnAlignLeft.Checked = false;
                    this.btnAlignCenter.Checked = true;
                    this.btnAlignRight.Checked = false;
                }
                else if (TextEditor.SelectionAlignment == HorizontalAlignment.Right)
                {
                    this.btnAlignLeft.Checked = false;
                    this.btnAlignCenter.Checked = false;
                    this.btnAlignRight.Checked = true;
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
                    this.cmbFontSize.Text = Convert.ToInt32(this.TextEditor.SelectionFont.Size).ToString();
                }
                catch
                {
                    this.cmbFontSize.Text = "";
                }

                try
                {
                    this.cmbFontName.Text = this.TextEditor.SelectionFont.Name;
                }
                catch
                {
                    this.cmbFontName.Text = "";
                }


                if (this.TextEditor.SelectionHangingIndent > 0)
                {
                    this.Ruler.LeftHangingIndent = (int)(this.TextEditor.SelectionHangingIndent / 3.775f); //convert pixels to millimeters
                }

                if (this.TextEditor.SelectionIndent > 0)
                {
                    this.Ruler.LeftIndent = (int)(this.TextEditor.SelectionIndent / 3.775f); //convert pixels to millimeters
                }

                if (this.TextEditor.SelectionRightIndent > 0)
                {
                    this.Ruler.RightIndent = (int)(this.TextEditor.SelectionRightIndent / 3.775f); //convert pixels to millimeters
                }

                if (this.cmbFontName.Text != "")
                {
                    FontFamily ff = new FontFamily(this.cmbFontName.Text);
                    if (ff.IsStyleAvailable(FontStyle.Bold) == true)
                    {
                        this.btnBold.Enabled = true;
                        this.btnBold.Checked = this.TextEditor.SelectionFont.Bold;
                    }
                    else
                    {
                        this.btnBold.Enabled = false;
                        this.btnBold.Checked = false;
                    }

                    if (ff.IsStyleAvailable(FontStyle.Italic) == true)
                    {
                        this.btnItalic.Enabled = true;
                        this.btnItalic.Checked = this.TextEditor.SelectionFont.Italic;
                    }
                    else
                    {
                        this.btnItalic.Enabled = false;
                        this.btnItalic.Checked = false;
                    }

                    if (ff.IsStyleAvailable(FontStyle.Underline) == true)
                    {
                        this.btnUnderline.Enabled = true;
                        this.btnUnderline.Checked = this.TextEditor.SelectionFont.Underline;
                    }
                    else
                    {
                        this.btnUnderline.Enabled = false;
                        this.btnUnderline.Checked = false;
                    }

                    if (ff.IsStyleAvailable(FontStyle.Strikeout) == true)
                    {
                        this.btnStrikeThrough.Enabled = true;
                        this.btnStrikeThrough.Checked = this.TextEditor.SelectionFont.Strikeout;
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
            this.TextEditor.Clear();
        }

        private void cmbFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.TextEditor.SelectionFont = new Font(this.cmbFontName.Text, Convert.ToInt32(this.cmbFontSize.Text), this.TextEditor.SelectionFont.Style);
            }
            catch (Exception)
            {
                MessageBox.Show("Sorry, but selected font does not support current style, please, select another font or switch to another style.");
            }
        }

        private void cmbFontSize_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    this.TextEditor.SelectionFont = new Font(this.cmbFontName.Text, Convert.ToSingle(this.cmbFontSize.Text));
                    this.TextEditor.Focus();
                }
                catch (Exception)
                {
                    MessageBox.Show("Sorry, but selected font does not support current style, please, select another font or switch to another style.");
                }
            }
        }

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

        private void btnBold_Click(object sender, EventArgs e)
        {
            if (this.TextEditor.SelectionFont.Bold == true)
            {
                this.btnBold.Checked = false;
            }
            else
            {
                this.btnBold.Checked = true;
            }
            this.TextEditor.SelectionFont = new Font(this.cmbFontName.Text, Convert.ToSingle(this.cmbFontSize.Text), SwitchBold());
        }

        private void btnAlignLeft_Click(object sender, EventArgs e)
        {
            this.TextEditor.SelectionAlignment = HorizontalAlignment.Left;
            this.btnAlignLeft.Checked = true;
            this.btnAlignRight.Checked = false;
            this.btnAlignCenter.Checked = false;
        }

        private void btnAlignCenter_Click(object sender, EventArgs e)
        {
            this.TextEditor.SelectionAlignment = HorizontalAlignment.Center;
            this.btnAlignLeft.Checked = false;
            this.btnAlignRight.Checked = false;
            this.btnAlignCenter.Checked = true;
        }

        private void btnAlignRight_Click(object sender, EventArgs e)
        {
            this.TextEditor.SelectionAlignment = HorizontalAlignment.Right;
            this.btnAlignLeft.Checked = false;
            this.btnAlignRight.Checked = true;
            this.btnAlignCenter.Checked = false;
        }

        private void Ruler_LeftIndentChanging(int NewValue)
        {
            try
            {
                //MessageBox.Show("1");
                this.TextEditor.SelectionHangingIndent = (int)(this.Ruler.LeftHangingIndent * 3.775f) - (int)(this.Ruler.LeftIndent * 3.775f);
                this.TextEditor.SelectionIndent = (int)(this.Ruler.LeftIndent * 3.775f);
                //this.Ruler.LeftHangingIndent = (int)(this.TextEditor.SelectionHangingIndent / 3.775f);
            }
            catch (Exception)
            {
            }
        }

        private void Ruler_LeftHangingIndentChanging(int NewValue)
        {
            try
            {
                //this.TextEditor.SelectionHangingIndent = (int)(this.Ruler.LeftHangingIndent * 3.775f);                
                this.TextEditor.SelectionHangingIndent = (int)(this.Ruler.LeftHangingIndent * 3.775f) - (int)(this.Ruler.LeftIndent * 3.775f);
            }
            catch (Exception)
            {
            }
        }

        private void Ruler_RightIndentChanging(int NewValue)
        {
            try
            {
                this.TextEditor.SelectionRightIndent = (int)(this.Ruler.RightIndent * 3.775f);
            }
            catch (Exception)
            {
            }
        }

        private void cmbFontName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.TextEditor.SelectionFont = new Font(this.cmbFontName.Text, Convert.ToInt32(this.cmbFontSize.Text), this.TextEditor.SelectionFont.Style);
            }
            catch (Exception)
            {
                MessageBox.Show("Sorry, but selected font does not support current style, please, select another font or switch to another style.");
            }
        }

        private void cmbFontName_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.TextEditor.SelectionFont = new Font(this.cmbFontName.Text, Convert.ToInt32(this.cmbFontSize.Text), this.TextEditor.SelectionFont.Style);
                    this.TextEditor.Focus();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Sorry, but selected font does not support current style, please, select another font or switch to another style.");
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
                this.TextEditor.SelectionFont = GetFont(this.TextEditor.SelectionFont);
            }
            catch (Exception)
            {
            }
        }

        private void mnuTextColor_Click(object sender, EventArgs e)
        {
            try
            {
                this.TextEditor.SelectionColor = GetColor(this.TextEditor.SelectionColor);
            }
            catch (Exception)
            {
            }
        }

        private void mnuHighlightColor_Click(object sender, EventArgs e)
        {
            try
            {
                this.TextEditor.SelectionBackColor = GetColor(this.TextEditor.SelectionBackColor);
            }
            catch (Exception)
            {
            }
        }

        private void Ruler_TabAdded(TextRuler.TextRulerControl.TextRuler.TabEventArgs args)
        {
            try
            {
                this.TextEditor.SelectionTabs = this.Ruler.TabPositionsInPixels.ToArray();
            }
            catch (Exception)
            {
            }
        }

        private void Ruler_TabChanged(TextRuler.TextRulerControl.TextRuler.TabEventArgs args)
        {
            try
            {
                this.TextEditor.SelectionTabs = this.Ruler.TabPositionsInPixels.ToArray();
            }
            catch (Exception)
            {
            }
        }

        private void Ruler_TabRemoved(TextRuler.TextRulerControl.TextRuler.TabEventArgs args)
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

            Image img = Image.FromFile(_imgPath);
            Image tempImage = Clipboard.GetImage();
            Clipboard.Clear();
            Clipboard.SetImage(img);
            this.TextEditor.Paste(DataFormats.GetFormat(DataFormats.Bitmap));
            if (tempImage != null)
                Clipboard.SetImage(tempImage);
            tempImage.Dispose();
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This control was created by Krassovskikh Aleksei. You can use freely use it in your application, but if it possible, mention about creator of that control (this is not required but desired :)  )");
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
                    MessageBox.Show("Sorry, but you entered invalid Date/Time formatting. Validate format and try again.");
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
            if (this.TextEditor.SelectionFont.Italic == true)
            {
                this.btnItalic.Checked = false;
            }
            else
            {
                this.btnItalic.Checked = true;
            }
            this.TextEditor.SelectionFont = new Font(this.cmbFontName.Text, Convert.ToSingle(this.cmbFontSize.Text), SwitchItalic());
        }

        private void btnUnderline_Click(object sender, EventArgs e)
        {
            if (this.TextEditor.SelectionFont.Underline == true)
            {
                this.btnUnderline.Checked = false;
            }
            else
            {
                this.btnUnderline.Checked = true;
            }
            this.TextEditor.SelectionFont = new Font(this.cmbFontName.Text, Convert.ToSingle(this.cmbFontSize.Text), SwitchUnderline());
        }

        private void btnStrikeThrough_Click(object sender, EventArgs e)
        {
            if (this.TextEditor.SelectionFont.Strikeout == true)
            {
                this.btnStrikeThrough.Checked = false;
            }
            else
            {
                this.btnStrikeThrough.Checked = true;
            }
            this.TextEditor.SelectionFont = new Font(this.cmbFontName.Text, Convert.ToSingle(this.cmbFontSize.Text), SwitchStrikeout());
        }

        private void mnuFind_Click(object sender, EventArgs e)
        {
            Dialogs.dlgFind find = new TextRuler.Dialogs.dlgFind();
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
    }
}
