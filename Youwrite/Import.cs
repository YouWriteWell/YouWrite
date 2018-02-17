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
        private string appDir; // data directory 
        
        public Form3()
        {
            InitializeComponent();
            dir = System.IO.Directory.GetCurrentDirectory();
            appDir = Path.Combine(Environment.GetFolderPath(
    Environment.SpecialFolder.ApplicationData), "YouWrite");
            source = new SQLiteConnection
    ("Data Source=" + Path.Combine(appDir, "categories.db") + ";Version=3;New=False;Compress=True;");

            SetConnection();
            selectdb();
        }
        private SQLiteConnection sql_con;
        private SQLiteConnection source;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        // Sqllite functions
        private void SetConnection()
        {

            //MessageBox.Show(System.IO.Directory.GetCurrentDirectory());
            source.Open();

            sql_con = new SQLiteConnection("Data Source=:memory:");

            sql_con.Open();

            // copy db file to memory

            source.BackupDatabase(sql_con, "main", "main", -1, null, 0);
            source.Close();

        }
        private void CloseConnection()
        {
            source.Open();

            // save memory db to file
            sql_con.BackupDatabase(source, "main", "main", -1, null, 0);
            source.Close();
        }
        private void selectdb()
        {
           // SetConnection();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = @"select id,name from category";
            DB = new SQLiteDataAdapter(sql_cmd);
            DS.Reset();
            DB.Fill(DS);
            DT = DS.Tables[0];


            if (DT.Rows.Count > 0)
            {
                
        

                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    DataRow dr = DT.Rows[i];
                    comboBox1.Items.Add(dr[1].ToString());

                }
            }
            //  sql_con.Close();
            //CloseConnection();

        }

        private void ExecuteQuery(string txtQuery)
        {
            //SetConnection();
            //sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = txtQuery;
            sql_cmd.ExecuteNonQuery();
            //sql_con.Close();
        }

        private int createdb(string name, string description)
        {
           // SetConnection();
            var CommandText = new SQLiteCommand("insert into  category (name,description) values ('" + name + "','" + description + "')", sql_con);
            CommandText.ExecuteNonQuery();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = @"select last_insert_rowid() as rowid";
            DB = new SQLiteDataAdapter(sql_cmd);
            DS.Reset();
            DB.Fill(DS);
            DT = DS.Tables[0];

            if (DT.Rows.Count > 0)
            {
                DataRow dr = DT.Rows[0];
                int id = Convert.ToInt32(dr[0].ToString());
                // sql_con.Close();
                var dbName = Path.Combine(Environment.GetFolderPath(
     Environment.SpecialFolder.ApplicationData), "YouWrite", id.ToString() + ".db");

                File.Copy(Path.Combine(appDir,"model.db"), dbName, true);
            
            CloseConnection();
                SetConnection();
                return id;
            }
            //  sql_con.Close();

            
            return -1;
        }
      
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked && textBox1.Text != "")
            {
                int id = createdb(textBox1.Text, textBox2.Text);
               Form4 form4 = new Form4(textBox1.Text, id);
                var screen = Screen.FromPoint(Cursor.Position);
                form4.StartPosition = FormStartPosition.Manual;
                form4.Left = screen.Bounds.Left + screen.Bounds.Width / 2 - form4.Width / 2;
                form4.Top = screen.Bounds.Top + screen.Bounds.Height / 2 - form4.Height / 2;



                // MessageBox.Show(id.ToString());
                this.Hide();
                form4.Show();
            }
            else if (radioButton1.Checked && comboBox1.Text != "")
            {

                DataRow dr = DT.Rows[comboBox1.SelectedIndex];


                Form4 form4 = new Form4(comboBox1.Text,Convert.ToInt32(dr[0].ToString()));
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked && textBox1.Text != "")
            {
                int id = createdb(textBox1.Text, textBox2.Text);
                Form4 form4 = new Form4(textBox1.Text, id);
                // MessageBox.Show(id.ToString());

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

                Form6 form6 = new Form6(comboBox1.Text, Convert.ToInt32(dr[0].ToString()));

                var screen = Screen.FromPoint(Cursor.Position);
                form6.StartPosition = FormStartPosition.Manual;
                form6.Left = screen.Bounds.Left + screen.Bounds.Width / 2 - form6.Width / 2;
                form6.Top = screen.Bounds.Top + screen.Bounds.Height / 2 - form6.Height / 2;
                this.Hide();
                form6.Show();

            }
            else
            {
                MessageBox.Show("Select a category or create a new one");

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked && textBox1.Text != "")
            {
                int id = createdb(textBox1.Text, textBox2.Text);
                Form4 form4 = new Form4(textBox1.Text, id);

                var screen = Screen.FromPoint(Cursor.Position);
                form4.StartPosition = FormStartPosition.Manual;
                form4.Left = screen.Bounds.Left + screen.Bounds.Width / 2 - form4.Width / 2;
                form4.Top = screen.Bounds.Top + screen.Bounds.Height / 2 - form4.Height / 2;

                // MessageBox.Show(id.ToString());
                this.Hide();
                form4.Show();
            }
            else if (radioButton1.Checked && comboBox1.Text != "")
            {

                DataRow dr = DT.Rows[comboBox1.SelectedIndex];

                Form7 form7 = new Form7(comboBox1.Text, Convert.ToInt32(dr[0].ToString()));

                var screen = Screen.FromPoint(Cursor.Position);
                form7.StartPosition = FormStartPosition.Manual;
                form7.Left = screen.Bounds.Left + screen.Bounds.Width / 2 - form7.Width / 2;
                form7.Top = screen.Bounds.Top + screen.Bounds.Height / 2 - form7.Height / 2;

                this.Hide();
                form7.Show();

            }
            else
            {
                MessageBox.Show("Select a category or create a new one");

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
