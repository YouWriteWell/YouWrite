using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace YouWrite
{
    public partial class Form3 : Form
    {
        string dir;
        private string _databasesDir; // data directory 
        private string _appDir; 
        private Databases _database;
        private DataTable DT;
        public Form3()
        {
            InitializeComponent();
            comboBox2.SelectedIndex = 0;
            _databasesDir = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData), "YouWrite");
            _appDir = Directory.GetCurrentDirectory();
            SQLiteConnection source = new SQLiteConnection
    ("Data Source=" + Path.Combine(_databasesDir, "categories.db") + ";Version=3;New=False;Compress=True;");
            _database = new Databases(_appDir, _databasesDir);
            _database.SetConnection(source);
            updateCategories();
        }

        private void updateCategories()
        {
            DT = _database.getCategories();
            
            if (DT.Rows.Count > 0)
            {             
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    DataRow dr = DT.Rows[i];
                    comboBox1.Items.Add(dr[1].ToString());

                }
            }
          
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked && textBox1.Text != "")
            {
                int id = _database.creatDB(textBox1.Text, textBox2.Text);
                Form4 form4 = new Form4(textBox1.Text, id,comboBox2.Text);
                var screen = Screen.FromPoint(Cursor.Position);
                form4.StartPosition = FormStartPosition.Manual;
                form4.Left = screen.Bounds.Left + screen.Bounds.Width / 2 - form4.Width / 2;
                form4.Top = screen.Bounds.Top + screen.Bounds.Height / 2 - form4.Height / 2;

                this.Hide();
                form4.Show();
            }
            else if (radioButton1.Checked && comboBox1.Text != "")
            {

                DataRow dr = DT.Rows[comboBox1.SelectedIndex];


                Form4 form4 = new Form4(comboBox1.Text,Convert.ToInt32(dr[0].ToString()), comboBox2.Text);
                var screen = Screen.FromPoint(Cursor.Position);
                form4.StartPosition = FormStartPosition.Manual;
                form4.Left = screen.Bounds.Left + screen.Bounds.Width / 2 - form4.Width / 2;
                form4.Top = screen.Bounds.Top + screen.Bounds.Height / 2 - form4.Height / 2;

                this.Hide();
                form4.Show();

            }
            else
            {
                MessageBox.Show("Select a category or create a new one");
            
            }

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
