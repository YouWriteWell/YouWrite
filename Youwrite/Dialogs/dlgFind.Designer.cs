namespace YouWrite.Dialogs
{
    partial class dlgFind
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnFindNext = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtFindThis = new System.Windows.Forms.TextBox();
            this.lblFindComm = new System.Windows.Forms.Label();
            this.chkMatchCase = new System.Windows.Forms.CheckBox();
            this.chkWholeWord = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnFindNext
            // 
            this.btnFindNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFindNext.Location = new System.Drawing.Point(150, 103);
            this.btnFindNext.Name = "btnFindNext";
            this.btnFindNext.Size = new System.Drawing.Size(106, 23);
            this.btnFindNext.TabIndex = 0;
            this.btnFindNext.Text = "Find next";
            this.btnFindNext.UseVisualStyleBackColor = true;
            this.btnFindNext.Click += new System.EventHandler(this.btnFindNext_Click);
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Location = new System.Drawing.Point(262, 103);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtFindThis
            // 
            this.txtFindThis.Location = new System.Drawing.Point(12, 25);
            this.txtFindThis.Name = "txtFindThis";
            this.txtFindThis.Size = new System.Drawing.Size(325, 20);
            this.txtFindThis.TabIndex = 3;
            // 
            // lblFindComm
            // 
            this.lblFindComm.AutoSize = true;
            this.lblFindComm.Location = new System.Drawing.Point(12, 9);
            this.lblFindComm.Name = "lblFindComm";
            this.lblFindComm.Size = new System.Drawing.Size(49, 13);
            this.lblFindComm.TabIndex = 4;
            this.lblFindComm.Text = "Find this:";
            // 
            // chkMatchCase
            // 
            this.chkMatchCase.AutoSize = true;
            this.chkMatchCase.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkMatchCase.Location = new System.Drawing.Point(12, 57);
            this.chkMatchCase.Name = "chkMatchCase";
            this.chkMatchCase.Size = new System.Drawing.Size(82, 17);
            this.chkMatchCase.TabIndex = 5;
            this.chkMatchCase.Text = "Match case";
            this.chkMatchCase.UseVisualStyleBackColor = true;
            // 
            // chkWholeWord
            // 
            this.chkWholeWord.AutoSize = true;
            this.chkWholeWord.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkWholeWord.Location = new System.Drawing.Point(12, 80);
            this.chkWholeWord.Name = "chkWholeWord";
            this.chkWholeWord.Size = new System.Drawing.Size(150, 17);
            this.chkWholeWord.TabIndex = 6;
            this.chkWholeWord.Text = "Search for the whole word";
            this.chkWholeWord.UseVisualStyleBackColor = true;
            // 
            // dlgFind
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 138);
            this.Controls.Add(this.chkWholeWord);
            this.Controls.Add(this.chkMatchCase);
            this.Controls.Add(this.lblFindComm);
            this.Controls.Add(this.txtFindThis);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnFindNext);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgFind";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Find";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFindNext;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblFindComm;
        private System.Windows.Forms.CheckBox chkMatchCase;
        private System.Windows.Forms.CheckBox chkWholeWord;
        internal System.Windows.Forms.TextBox txtFindThis;
    }
}