using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace YouWrite.Dialogs
{
    public partial class dlgFind : Form
    {

        AdvancedTextEditorControl.AdvancedTextEditor caller;
        int lastStop = 0;

        internal AdvancedTextEditorControl.AdvancedTextEditor Caller
        {
            set { caller = value; }
        }

        public dlgFind()
        {
            InitializeComponent();
        }

        private RichTextBoxFinds GetOptions()
        {
            RichTextBoxFinds rtbf = new RichTextBoxFinds();

            rtbf = RichTextBoxFinds.None;

            if (this.chkMatchCase.Checked == true)
            {
                rtbf = rtbf | RichTextBoxFinds.MatchCase;
            }

            if (this.chkWholeWord.Checked == true)
            {
                rtbf = rtbf | RichTextBoxFinds.WholeWord;
            }

            return rtbf;
        }

        private void btnFindNext_Click(object sender, EventArgs e)
        {
            if (lastStop == -1) { lastStop = 0; }

            lastStop = caller.TextEditor.Find(this.txtFindThis.Text, lastStop, GetOptions());

            if (lastStop == -1)
            {
                MessageBox.Show("Search is completed.");
            }
            else
            {
                lastStop = lastStop + this.txtFindThis.Text.Length; 
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }       
    }
}
