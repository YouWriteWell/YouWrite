using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.ComponentModel;

namespace YouWrite.AdvancedTextEditorControl.FontComboBoxControl
{
    internal class FontComboBox : ComboBox
    {

        Pen p = new Pen(Color.Black);
        StringFormat sf = new StringFormat();
        Font f = new Font("Arial", 8);

        internal FontComboBox()
        {
            this.DrawMode = DrawMode.OwnerDrawFixed;
            sf.Trimming = StringTrimming.None;
            sf.FormatFlags = StringFormatFlags.NoWrap;
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
                ListFonts();
            this.DropDownHeight = 300;
            this.Font = new Font(this.Font.Name, 12);            
            this.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.MaxDropDownItems = 20;
        }        
        
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);

            //refresh pen and font
            try
            {                
                FontFamily ff = new FontFamily(this.Items[e.Index].ToString());

                if (ff.IsStyleAvailable(FontStyle.Regular) == true)
                {
                    f = new Font(this.Items[e.Index].ToString(), this.Font.Size, FontStyle.Regular);
                }
                else if (ff.IsStyleAvailable(FontStyle.Italic) == true)
                {
                    f = new Font(this.Items[e.Index].ToString(), this.Font.Size, FontStyle.Italic);
                }
                else if (ff.IsStyleAvailable(FontStyle.Bold) == true)
                {
                    f = new Font(this.Items[e.Index].ToString(), this.Font.Size, FontStyle.Bold);
                }
                else
                {
                    f = new Font("Microsoft Sans Serif", this.Font.Size, FontStyle.Regular);
                }
            }
            catch (Exception)
            {
                f = new Font(this.Items[e.Index].ToString(), this.Font.Size, FontStyle.Bold);
            }
            p = new Pen(this.ForeColor);

            e.DrawBackground();            
            
            e.Graphics.DrawString(this.Items[e.Index].ToString(), f, p.Brush, e.Bounds);

            if (e.State == DrawItemState.Focus)
                e.DrawFocusRectangle();

            //clear memory
            p.Dispose();
            f.Dispose();
        }

        private void ListFonts()
        {
            this.Items.Clear();
            System.Drawing.Text.InstalledFontCollection ifc = new System.Drawing.Text.InstalledFontCollection();
            this.BeginUpdate();
            foreach (FontFamily ff in ifc.Families)
            {
                this.Items.Add(ff.Name);
            }

            this.EndUpdate();
        }

    }
}
