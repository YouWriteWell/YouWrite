namespace YouWrite
{
    partial class phrases
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

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.advancedTextEditor2 = new YouWrite.AdvancedTextEditorControl.AdvancedTextEditor();
            this.SuspendLayout();
            // 
            // advancedTextEditor2
            // 
            this.advancedTextEditor2.BackColor = System.Drawing.Color.White;
            this.advancedTextEditor2.Location = new System.Drawing.Point(3, -1);
            this.advancedTextEditor2.Name = "advancedTextEditor2";
            this.advancedTextEditor2.Size = new System.Drawing.Size(1058, 608);
            this.advancedTextEditor2.TabIndex = 5;
            this.advancedTextEditor2.Load += new System.EventHandler(this.advancedTextEditor2_Load);
            // 
            // phrases
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.advancedTextEditor2);
            this.Name = "phrases";
            this.Size = new System.Drawing.Size(1060, 610);
            this.Load += new System.EventHandler(this.phrases_Load);
            this.Click += new System.EventHandler(this.phrases_Click);
            this.ResumeLayout(false);

        }

        #endregion

        private YouWrite.AdvancedTextEditorControl.AdvancedTextEditor advancedTextEditor2;
    }
}
