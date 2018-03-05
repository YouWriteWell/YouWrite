using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.util;
using OpenNLP.Tools.SentenceDetect;
using OpenNLP.Tools.Tokenize;
//using ICU4NET;
//using ICU4NETExtension;

namespace YouWrite
{
    public partial class Form4 : Form
    {
      
        private string foldername;
       
        private DialogResult result;
       
        private Import _import;
        private string _extension;
       

        private DataTable ListOfFiles;  

        public Form4(string category, int id,string extension)
        {

            _extension = extension;
            InitializeComponent();
            _import = new Import(id);

            _import.setstate += new Import.CallbackEventHandler(setstate);
            
            Text = "Import "+ _extension + " documents (" + category + ")";
        }

     
        private void button1_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                foldername = folderBrowserDialog1.SelectedPath;
                ListOfFiles= _import.listfiles(foldername, _extension);
                dataGridView1.DataSource = ListOfFiles;

            }
        }

        

        private void loadingfiles()
        {
            if (result == DialogResult.OK)
            { 
                var i = 0;
                string filename;
                foreach (var f in Directory.GetFiles(foldername))
                    if (f.Split('.').Count() >= 2 && f.Split('.')[f.Split('.').Count() - 1] == _extension)
                    {
                        filename = f;
                         _import.import(i, filename);
                        i++;
                    }

                _import.CloseImport();
                MessageBox.Show("Opearation completed !");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var t1 = new Thread(loadingfiles);
            t1.IsBackground = true;
            t1.Start();
            button2.Enabled = false;
        }


        private int pp;
        private void setstate(int curr, int max, int papern,string msg)
        {
            if (msg == "")
            {
                
            
            int p;
            p = curr * 100 / max;
            if (p >= pp + 5)
            {
                ListOfFiles.Rows[papern][2] = p.ToString("0.00") + "%";

                dataGridView1.DataSource = ListOfFiles;
                pp = p;
            }
            if (pp == 100) pp = 0;

            }
            else
            {
                ListOfFiles.Rows[papern][2] = msg;
            }
        }

        
    }
}