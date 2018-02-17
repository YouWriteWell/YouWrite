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
    public partial class UsingControl : UserControl
    {
        public UsingControl(string l1,string l2,string l3,string l4,string l5,string l6,string t)
        {
            InitializeComponent();

            int num = Convert.ToInt32(l3);
            label1.Text = l1;
            label2.Text = l2;
            label3.Text = l3;

            if (num == 0)
            {
                label1.BackColor = Color.Orange;
                label2.BackColor = Color.Orange;
                label3.BackColor = Color.Orange;  
            }

            label4.Text = l4;
            label5.Text = l5;
            label6.Text = l6;
            textBox1.Text = t;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void UsingControl_Load(object sender, EventArgs e)
        {

        }
    }
}
