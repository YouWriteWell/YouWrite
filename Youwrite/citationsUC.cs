using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YouWrite
{
    public partial class UserControl3 : UserControl
    {
        string path;
        Form2 fr;
        public void sety(int y)
        {
            this.Height = y;
        }

        public UserControl3(string pathc,string textc,Form2 fr1,string reference)
        {
           
            InitializeComponent();
            fr = fr1;
            path = pathc;
            richTextBox1.Text = textc;
            textBox1.Text = reference;
            int nblines = (richTextBox1.Text.Length / 115)+3;
            int lhight = nblines * 17; 

          
            richTextBox1.Size = new Size(richTextBox1.Size.Width, lhight);
            
                Point p1 = new Point(label1.Location.X, label1.Location.Y + lhight);
                Point p2 = new Point(label2.Location.X, label2.Location.Y + lhight);
                Point p3 = new Point(label3.Location.X, label3.Location.Y + lhight);
                Point p4 = new Point(label4.Location.X, label4.Location.Y + lhight);
                Point p5 = new Point(pictureBox1.Location.X, pictureBox1.Location.Y + lhight);
                label1.Location = p1;
                label2.Location = p2;
                label3.Location = p3;
                label4.Location = p4;
                pictureBox1.Location = p5;
                
                             
            if (path.Equals("title"))
            {
                label1.Text = path;
            }
            else
            {
                string[] ss = path.Split('\\');
                string title = ss[ss.Count() - 1];
                label1.Text = title;
                ss = title.Split('-');
                if (ss.Count() == 3)
                {
                    label2.Text = ss[2].Split('.')[0];
                    label1.Text = "[" + ss[0] + "]" + ss[1];
                }
                else if (ss.Count() > 3)
                {
                    label2.Text = ss[ss.Count() - 1].Split('.')[0];
                }
                else
                {
                    label2.Text = "Unkown";
                }
            }
            
        }
        public void highlight(string txt)
        {
            int my1stPosition = richTextBox1.Find(txt, 0, richTextBox1.Text.Length, RichTextBoxFinds.None);


            while (my1stPosition != -1)
            {
                richTextBox1.SelectionStart = my1stPosition;
                richTextBox1.SelectionLength = txt.Length;
                richTextBox1.SelectionColor = Color.White;
                richTextBox1.SelectionBackColor = Color.Blue;
                my1stPosition = richTextBox1.Find(txt, my1stPosition + txt.Length, richTextBox1.Text.Length, RichTextBoxFinds.None);
            }
            
        }
        private void label3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(path);
        }

        private void label4_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText.Equals(""))
            {
                fr.addtoclip(richTextBox1.Text + "[" + label1.Text + "]");
            }
            else
            {
                fr.addtoclip(richTextBox1.SelectedText + "[" + label1.Text + "]");    
            }
            
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Richbox = "+(richTextBox1.Location.Y-richTextBox1.Size.Height).ToString());
            
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
