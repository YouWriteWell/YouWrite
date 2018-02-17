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
    public partial class UserControl2 : UserControl
    {
        Form2 fr1;

        
        public int idp;// id of the paper 
        public int id; // id of phrase
        public int cha;
        public string phrase;
        public string path;
        public string titleg;
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

   
        public UserControl2(int idpc,int idc,string authors,string phrasec, Form2 fr,int chac)
        {
            id = idc;
            idp = idpc;
            phrase = phrasec;
            path = fr.gettitle(idp);
            cha = chac;

           // this.Dock = DockStyle.Fill;

            fr1 = fr;
            InitializeComponent();
            if (path.Equals("title"))
            {
                label1.Text = path;
            }
            else
            {
                string[] ss = path.Split('\\');
                string title = ss[ss.Count() - 1];
                titleg = title;
                label1.Text = title;
                ss = title.Split('-');
                if (ss.Count() == 3)
                {
                    label2.Text = ss[2].Split('.')[0];
                    label1.Text = "["+ss[0]+"]"+ss[1];
                }
                else if (ss.Count() > 3)
                {
                    label2.Text = ss[ss.Count() - 1].Split('.')[0];
                }
                else
                {
                    label2.Text = authors;  
                }
            }
               
             
            richTextBox1.Text = phrase;
           
            int nblines = (richTextBox1.Text.Length / 117) + 1;
            if (nblines>4)
            {
                nblines = 4;
            }
            int lhight = nblines * 15;
            
            Point p1 = new Point(button1.Location.X, button1.Location.Y + lhight);
            Point p2 = new Point(button2.Location.X, button1.Location.Y + lhight);
            Point p3 = new Point(button3.Location.X, button1.Location.Y + lhight);
            Point p4 = new Point(button4.Location.X, button1.Location.Y + lhight);
            Point p5 = new Point(button5.Location.X, button1.Location.Y + lhight);

            button1.Location = p1;
            button2.Location = p2;
            button3.Location = p3;
            button4.Location = p4;
            button5.Location = p5;
        }
        public void sety(int y)
        {
            this.Height = y;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fr1.past(richTextBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fr1.past(richTextBox1.Text+"("+label1.Text+")");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            fr1.getmorephrases(phrase, id, idp,cha);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(path);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText.Equals(""))
            {
                fr1.addtoclip(richTextBox1.Text + System.Environment.NewLine + "(" + titleg + ")" + System.Environment.NewLine);
            }
            else
            {
                fr1.addtoclip(richTextBox1.SelectedText + System.Environment.NewLine + "(" + titleg + ")" + System.Environment.NewLine);
            }
           
            
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
