using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SQLite;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.util;
using System.Threading;
using OpenNLP;
//using ICU4NET;
//using ICU4NETExtension;
using System.Web;
using AutocompleteMenuNS;
using RavSoft.GoogleTranslator;
using System.Text.RegularExpressions;
//using AutoUpdaterDotNET;

namespace YouWrite
{
    public partial class Form2 : Form
    {


        private SQLiteConnection sql_con;
        private SQLiteConnection source,source2;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private DataTable DT2 = new DataTable();
        private DataTable DT3;
        int idf; // the id of the first element in the list (used to get phrases)
      
        private OpenNLP.Tools.SentenceDetect.MaximumEntropySentenceDetector mSentenceDetector;
        private OpenNLP.Tools.Tokenize.EnglishMaximumEntropyTokenizer mTokenizer;
  
        private string mModelPath;
       
        private string dir;
        private string appDir; // data directory 
      
        TableLayoutPanel panel;
        public Form2()
        {
            InitializeComponent();

            this.MinimumSize = new Size(1300, 800);
            dir = System.IO.Directory.GetCurrentDirectory();


            appDir = Path.Combine(Environment.GetFolderPath(
    Environment.SpecialFolder.ApplicationData), "YouWrite");

            if (!Directory.Exists(appDir))
            {
                Directory.CreateDirectory(appDir);
            }

           if (!File.Exists(Path.Combine(appDir, "categories.db")))
            {
                File.Copy(Path.Combine(dir, "categories.db"), Path.Combine(appDir,"categories.db"), true);
            }

           if (!File.Exists(Path.Combine(appDir, "model.db")))
            {
              
                File.Copy(Path.Combine(dir, "model.db"), Path.Combine(appDir, "model.db"), true);
            }



            panel = new TableLayoutPanel();
            panel.ColumnCount = 1;
            panel.RowCount = 1;

            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

          

          
            source2 = new SQLiteConnection
    ("Data Source=" + Path.Combine(appDir, "categories.db") + ";Version=3;New=False;Compress=True;");
            
            source = new SQLiteConnection
    ("Data Source=" + Path.Combine(appDir, "model.db") + ";Version=3;New=False;Compress=True;");
           
            // MessageBox.Show(dir);
            mModelPath = dir;
            SetConnection();
            selectdb();

            advancedTextEditor1.textboxchanged += new EventHandler(textchanged);
            advancedTextEditor1.textboxclicked += new EventHandler(textboxclicked);
            advancedTextEditor1.keyup += new EventHandler(keyup);


            //AutoUpdater.Start("http://rbsoft.org/updates/AutoUpdaterTest.xml");
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }


        private void Form2_Load(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            advancedTextEditor1.TextEditor.Cursor = Cursors.IBeam; 
            
        }

        private void keyup(object sender, EventArgs ee)
        {
        /*    KeyEventArgs e=(KeyEventArgs)ee;
            if (e.KeyCode == Keys.Down)
            {
                int ind = advancedTextEditor1.TextEditor.Text.LastIndexOf(" ");
                string s = "";
                if (ind > 0)
                {
                    s = advancedTextEditor1.TextEditor.Text.Substring(0, ind + 1); ;
                }


                if (listBox1.Items.Count >= 1)
                {
                    
                    advancedTextEditor1.TextEditor.Text = s + listBox1.Items[0].ToString();
                    advancedTextEditor1.TextEditor.SelectionStart = advancedTextEditor1.TextEditor.Text.Length;
                }

            }
    
    */
    }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        // Sqllite functions
        private void SetConnection()
        {
         
           
           //MessageBox.Show(System.IO.Directory.GetCurrentDirectory());
            source.Open();

            if (sql_con != null)
            {
                sql_con.Close();
            }
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
        private void ExecuteQuery(string txtQuery)
        {
            //SetConnection();
            //sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = txtQuery;
            sql_cmd.ExecuteNonQuery();
            //sql_con.Close();
        }


        private void createdb(string name,string description)
        {
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
                Environment.SpecialFolder.ApplicationData),"YouWrite", id.ToString() + ".db");

                File.Copy(appDir+@"\model.db", dbName, true);
            }
            //  sql_con.Close();

        }

        // To check if this b-gram existes 
        private int seqexist2gram(string word1, string word2)
        {
            //SetConnection();
           // sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = "select id from bgram where word1 like '" + word1 + "' and word2 like'" + word2 + "'";
           
                DB = new SQLiteDataAdapter(sql_cmd);
                DS.Reset();
                DB.Fill(DS);
                DT = DS.Tables[0];

                if (DT.Rows.Count > 0)
                {
                    DataRow dr = DT.Rows[0];
                    int id = Convert.ToInt32(dr[0].ToString());
                    // sql_con.Close();
                    return (id);

                }
                else
                {
                    // sql_con.Close();
                    return -1;
                }
           

        }
        // To check if this 3-gram existes 
        private int seqexist3gram(string word1, string word2, string word3)
        {
           // SetConnection();
           // sql_con.Open();
            
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = "select id from tgram where word1='" + word1 + "' and word2='" + word2 + "'and word3='" + word3 + "'";
            DB = new SQLiteDataAdapter(sql_cmd);
            DS.Reset();
            DB.Fill(DS);
            DT = DS.Tables[0];

            if (DT.Rows.Count > 0)
            {
                DataRow dr = DT.Rows[0];
                int id = Convert.ToInt32(dr[0].ToString());
               // sql_con.Close();
                return (id);

            }
            else
            {
            //    sql_con.Close();
                return -1;
            }

        }

        
        
        
// Remove special caracters 
        public static string removes(string s) 
        {
            
            string[] chars = new string[] { "/", "!", "@", "#", "$", "%", "^", "&", "*", "%", "\"", "|" };
            if (s.Contains("'"))
            {
               s = s.Replace("'", "''"); 
            }

            for (int i = 0; i < chars.Length; i++)
    {

      if(s.Contains(chars[i]))
      {
          s = s.Replace(chars[i], " ");
      }  
    }
            return s;
        
        }

        //ignore this caracters 
        private bool ignore(string s)
        {
            return false;
            string[] chars = new string[] { "/", "!", "@", "#", "$", "%", "^", "&", "*", "%", "'", "\"", "|", "{", "}", "[", "]", ",", ".", "=", "$", "-", "<", ">", ";", ")", "(", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            for (int i = 0; i < chars.Length; i++)
            {
                if (s.Contains(chars[i]))
                {
                    return true;
                }
            }
            return false;

        }

        private int Add(string word1, string word2)
        {
            //SetConnection();
           // sql_con.Open();
            var CommandText = new SQLiteCommand("insert into  bgram (word1,word2,freq) values ('" + word1 + "','" + word2 + "',0)", sql_con);
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
                return (id);

            }
          //  sql_con.Close();
            return -1;


        }
        //add 3-gram
        private int Add(string word1, string word2, string word3)
        {
           // SetConnection();
           // sql_con.Open();
          
            var CommandText = new SQLiteCommand("insert into  tgram (word1,word2,word3,freq) values ('" + word1 + "','" + word2 + "','" + word3 + "',0)", sql_con);
            CommandText.ExecuteNonQuery();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = @"select last_insert_rowid() as rowid";
            DB = new SQLiteDataAdapter(sql_cmd);
            DS.Reset();
            DB.Fill(DS);
            DT = DS.Tables[0];



            DataRow dr = DT.Rows[0];
            int id = Convert.ToInt32(dr[0].ToString());
           // sql_con.Close();
            return (id);



        }

        private void Update2gram(int id)
        {

            string txtSQLQuery = "update bgram set freq =(freq+1) where id = " + id.ToString();
            ExecuteQuery(txtSQLQuery);
        }
        private void Update3gram(int id)
        {

            string txtSQLQuery = "update tgram set freq =(freq+1) where id = " + id.ToString();
            ExecuteQuery(txtSQLQuery);
        }
        //add phrase
        private int AddPhrase(string phrase)
        {
            // SetConnection();
            // sql_con.Open();

            byte[] encod = Encoding.Default.GetBytes(phrase);
            string phrase1 = Encoding.UTF8.GetString(encod);
            var CommandText = new SQLiteCommand("insert into  phrase (phrase) values ('@phrase')", sql_con);

            CommandText.Parameters.AddWithValue("@phrase", phrase1);

            try
            {
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
                    return (id);

                }
            }
            catch { }
           // sql_con.Close();
            return -1;



        }
        //add phrasegramrelation
        private void AddPhraseGram(int idp, int idg, int type)
        {
           // SetConnection();
           // sql_con.Open();
            var CommandText = new SQLiteCommand("insert into  grampaper (idp,idg,type) values (" + idp.ToString() + "," + idg.ToString() + "," + type.ToString() + ")", sql_con);
            CommandText.ExecuteNonQuery();
           // sql_con.Close();
        }

        // insert words in database
        private void InsertWords(string[] words, int idphrase)
        {
            int id2, id3;
            string word1 = words[0];
            string word2 = words[1];
            string word3;
            int idseq2;
            int idseq3;
            idseq2 = seqexist2gram(word1, word2);
            if (idseq2 > 0)
            {
                Update2gram(idseq2);
            }
            else
            {
                id2 = Add(word1, word2);
                AddPhraseGram(idphrase, id2, 2);

            }



            for (int i = 2; i < words.Count(); i++)
            {
                word3 = words[i];

                idseq3 = seqexist3gram(word1, word2, word3);

                if (idseq3 > 0)
                {
                    Update3gram(idseq3);

                }
                else
                {
                    id3 = Add(word1, word2, word3);
                    AddPhraseGram(idphrase, id3, 3);

                }
                idseq2 = seqexist2gram(word2, word3);
                if (idseq2 > 0)
                {
                    Update2gram(idseq2);
                }
                else
                {
                    id2 = Add(word2, word3);
                    AddPhraseGram(idphrase, id2, 2);
                }
                word1 = word2;
                word2 = word3;

            }


        }
        public string parseUsingPDFBox(string input)
        {
            PDDocument doc = PDDocument.load(input);
            PDFTextStripper stripper = new PDFTextStripper();
            return stripper.getText(doc);
        }
        private void selectdb()
        {

            source2 = new SQLiteConnection
("Data Source=" + Path.Combine(appDir, "categories.db") + ";Version=3;New=False;Compress=True;");

          
            source2.Open();

            sql_cmd = source2.CreateCommand();
            sql_cmd.CommandText = @"select id,name from category";
            DB = new SQLiteDataAdapter(sql_cmd);
            DS.Reset();
            DB.Fill(DS);
            DT3 = new DataTable(); 
            DT2 = DS.Tables[0];
            DT3 = DS.Tables[0];
            comboBox2.Items.Clear();

            if (DT2.Rows.Count > 0)
            {



                for (int i = 0; i < DT2.Rows.Count; i++)
                {
                    DataRow dr = DT2.Rows[i];
                    comboBox2.Items.Add(dr[1].ToString());
                   
                }
            }
            //DT3 = DT2.Clone();
            //  sql_con.Close();

            source2.Close();
            

        }
       
        public string[] SplitSentences(string paragraph)
        {
            if (mSentenceDetector == null)
            {
                mSentenceDetector = new OpenNLP.Tools.SentenceDetect.EnglishMaximumEntropySentenceDetector(mModelPath + @"\EnglishSD.nbin");
            }

            return mSentenceDetector.SentenceDetect(paragraph);
        }

        public string[] TokenizeSentence(string sentence)
        {
            if (mTokenizer == null)
            {
                mTokenizer = new OpenNLP.Tools.Tokenize.EnglishMaximumEntropyTokenizer(mModelPath + @"\EnglishTok.nbin");
            }

            return mTokenizer.Tokenize(sentence);
        }
        int yusing = 0;

        public DataTable ExecuteSelect(SQLiteCommand cmd)
        {
          
            SQLiteDataAdapter DB = new SQLiteDataAdapter(cmd);
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            DS.Reset();
            DB.Fill(DS);
            DT = DS.Tables[0];
            
            return DT;

        }
        private void getusing(string word1, string word2) {
            
          // count order word1 --> word2
            sql_cmd = sql_con.CreateCommand();
             sql_cmd.CommandText = "select freq from bgram where word1 = @word1 and word2 = @word2";
                
                sql_cmd.Parameters.AddRange(new[]{
                new SQLiteParameter("@word1", word1),
                new SQLiteParameter("@word2", word2)
            });
               
                DT = ExecuteSelect(sql_cmd);
                int count1 = DT.Rows.Count;




            // count order word2 --> word1
                sql_cmd = sql_con.CreateCommand();
                sql_cmd.CommandText = "select freq from bgram where word1 = @word2 and word2 = @word1";

                //sql_cmd.Parameters.Add(word11);
                //sql_cmd.Parameters.Add(word22);
                sql_cmd.Parameters.AddRange(new[]{
                new SQLiteParameter("@word1", word1),
                new SQLiteParameter("@word2", word2)
            });
                DT = ExecuteSelect(sql_cmd);
                int count2 = DT.Rows.Count;

         
            // search for the words in the center
                sql_cmd = sql_con.CreateCommand();
                string ite1 = "";
                if (count1 > 0)
                {
                    sql_cmd.CommandText = "select word2,id from tgram where word1 = @word1 and word3 = @word2 order by freq desc LIMIT 0, 50";
                    sql_cmd.Parameters.AddRange(new[]{
                new SQLiteParameter("@word1", word1),
                new SQLiteParameter("@word2", word2)
            });
                    DT =  ExecuteSelect(sql_cmd);
                    if (DT.Rows.Count > 0)
                    {
                        DataRow dr;
                        if (idf == -1)
                        {
                            dr = DT.Rows[0];
                            idf = Convert.ToInt32(dr[1].ToString());
                            //getphrases(idf, 2, word2);
                        }
                        for (int i = 0; i < DT.Rows.Count; i++)
                        {
                            dr = DT.Rows[i];
                            if (!ignore(dr[0].ToString()))
                            {
                                ite1 = ite1 + " , " + dr[0].ToString();
                            }

                        }
                    }

                }
                string ite2 = ""; 
                if (count2 > 0)
                {
                    sql_cmd.CommandText = "select word2,id from tgram where word1 = @word2 and word3 = @word1 order by freq desc LIMIT 0, 50";
                    sql_cmd.Parameters.AddRange(new[]{
                new SQLiteParameter("@word1", word1),
                new SQLiteParameter("@word2", word2)
            });
                    DT = ExecuteSelect(sql_cmd);
                    if (DT.Rows.Count > 0)
                    {
                        DataRow dr;
                        if (idf == -1)
                        {
                            dr = DT.Rows[0];
                            idf = Convert.ToInt32(dr[1].ToString());
                            //getphrases(idf, 2, word2);
                        }
                        for (int i = 0; i < DT.Rows.Count; i++)
                        {
                            dr = DT.Rows[i];
                            if (!ignore(dr[0].ToString()))
                            {
                                ite2 = ite2 + " , " + dr[0].ToString();
                            }

                        }
                    }
                }
                string ite3 = "";
                if (count1 == 0 && count2 == 0)
                {

                    sql_cmd.CommandText = "select word2,id from bgram where word1 = @word1 order by freq desc LIMIT 0, 100";
                    //sql_cmd.Parameters.Add(word11);
                    //sql_cmd.Parameters.Add(word22);
                    sql_cmd.Parameters.AddRange(new[]{
                new SQLiteParameter("@word1", word1),
                new SQLiteParameter("@word2", word2)
            });

                  DT = ExecuteSelect(sql_cmd);


                    if (DT.Rows.Count > 0)
                    {
                        DataRow dr;
                        if (idf == -1)
                        {
                            dr = DT.Rows[0];
                            idf = Convert.ToInt32(dr[1].ToString());
                            //getphrases(idf, 2, word2);
                        }
                        for (int i = 0; i < DT.Rows.Count; i++)
                        {
                            dr = DT.Rows[i];
                            if (!ignore(dr[0].ToString()))
                            {
                                ite3 = ite3 + " , " + word1 + " " + dr[0].ToString();
                            }

                        }
                    }
                    sql_cmd.CommandText = "select word2,id from bgram where word1 = @word2 order by freq desc LIMIT 0, 100";
                    //sql_cmd.Parameters.Add(word11);
                    //sql_cmd.Parameters.Add(word22);
                    sql_cmd.Parameters.AddRange(new[]{
                new SQLiteParameter("@word1", word1),
                new SQLiteParameter("@word2", word2)
            });

                    DT = ExecuteSelect(sql_cmd);


                    if (DT.Rows.Count > 0)
                    {
                        DataRow dr;
                        if (idf == -1)
                        {
                            dr = DT.Rows[0];
                            idf = Convert.ToInt32(dr[1].ToString());
                            //getphrases(idf, 2, word2);
                        }
                        for (int i = 0; i < DT.Rows.Count; i++)
                        {
                            dr = DT.Rows[i];
                            if (!ignore(dr[0].ToString()))
                            {
                                ite3 = ite3 + " , " + word2 + " " + dr[0].ToString();
                            }

                        }
                    }
  
                }
                string ite;
                if (count1 >= count2 && count1 !=0 )
                {
                    ite = word1 + "..." + word2 + " : " + ite1 +" || "+word2+"..."+ word1+" : "+ite2;
                }
                else if (count2 != 0)
                {
                    ite = word2+"..."+ word1+" : "+ite2+" || "+word1 + "..." + word2 + " : " + ite1 ;
                }
                else
                {
                    ite= ite3;
                }

                
                
                   
          
          
               
              
               
            // add them to panel
                UsingControl uc = new UsingControl(word1, word2, count1.ToString(), word2, word1, count2.ToString(), ite);
                uc.Location = new Point(0,yusing);
                yusing += uc.Size.Height;
            panel7.Controls.Add(uc);
        }
       
        private void getnextword(string word1, string word2,string word3,string lastletter)
        {
          //MessageBox.Show("|" + word1 + "|" + word2+"|"+lastletter+"|");
            label10.Text =word1 ;
            label11.Text = word2;
            label12.Text = lastletter;
           //word1 = removes(word1);
           //word2 = removes(word2);
           //word3 = removes(word3);
           
            if (checkBox2.Checked)
            {
                MessageBox.Show("word1 = "+ word1+" word2 = "+ word2+ " word3 = " +word3);
            }
            
      
     
            if ((lastletter.Equals(" ")))
            {
                



                listBox1.Items.Clear();
                sql_cmd = sql_con.CreateCommand();
                //SetConnection();
                //SQLiteParameter word11 = new SQLiteParameter("@word11", SqlDbType.Text) { Value = word1 };
                //SQLiteParameter word22 = new SQLiteParameter("@word22", SqlDbType.Text) { Value = word2 };

                if (radioButton4.Checked)
                {
                    sql_cmd.CommandText = "select word3,id from tgram where word1=@word1 and word2 = @word2 order by freq desc LIMIT 0, 100";
                }
                else
                {
                    sql_cmd.CommandText = "select word2,id from tgram where word1=@word2 and word3 =@word3 order by freq desc LIMIT 0, 100";
                }

                sql_cmd.Parameters.AddRange(new[]{
                new SQLiteParameter("@word1", word1),
                new SQLiteParameter("@word2", word2),
                new SQLiteParameter("@word3", word3)
            });

                //sql_cmd.Parameters.Add(word11);
                //sql_cmd.Parameters.Add(word22);

                DB = new SQLiteDataAdapter(sql_cmd);
                DS.Reset();
                DB.Fill(DS);
                DT = DS.Tables[0];

                idf = -1;

                if (DT.Rows.Count > 0)
                {
                    DataRow dr = DT.Rows[0];
                    idf = Convert.ToInt32(dr[1].ToString());
                    //getphrases(idf, 3, word2);

                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        dr = DT.Rows[i];
                       if (!ignore(dr[0].ToString()))
                       {
                            listBox1.Items.Add(dr[0].ToString());
                       }
                        
                    }
                }

                listBox1.Items.Add("------------------");
                sql_cmd = sql_con.CreateCommand();

                //SQLiteParameter word11 = new SQLiteParameter("@word11", SqlDbType.Text) { Value = word1 };
                //SQLiteParameter word22 = new SQLiteParameter("@word22", SqlDbType.Text) { Value = word2 };

                if (radioButton4.Checked)
                {
                    sql_cmd.CommandText = "select word2,id from bgram where word1=@word2 order by freq desc LIMIT 0, 100";
                }
                else
                {
                    sql_cmd.CommandText = "select word1,id from bgram where word2=@word3 order by freq desc LIMIT 0, 100";
                }
                //sql_cmd.Parameters.Add(word11);
                //sql_cmd.Parameters.Add(word22);
                sql_cmd.Parameters.AddRange(new[]{
                new SQLiteParameter("@word1", word1),
                new SQLiteParameter("@word2", word2),
                new SQLiteParameter("@word3", word3)
            });
                DB = new SQLiteDataAdapter(sql_cmd);
                DS.Reset();
                DB.Fill(DS);
                DT = DS.Tables[0];

                if (DT.Rows.Count > 0)
                {
                    DataRow dr;
                    if (idf == -1)
                    {
                        dr = DT.Rows[0];
                        idf = Convert.ToInt32(dr[1].ToString());
                        //getphrases(idf, 2, word2);
                    }
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        dr = DT.Rows[i];
                        if (!ignore(dr[0].ToString()))
                        {
                            listBox1.Items.Add(dr[0].ToString());
                        }
                        
                    }
                }
            }
            else
            {
                sql_cmd = sql_con.CreateCommand();

                sql_cmd.CommandText = "select word1,id from bgram where word1 like @word2 order by freq desc LIMIT 0, 100";
                //richTextBox1.Text = sql_cmd.CommandText;
                //sql_cmd.Parameters.Add(word11);
                //sql_cmd.Parameters.Add(word22);
                sql_cmd.Parameters.AddRange(new[]{
                new SQLiteParameter("@word1", word1),
                new SQLiteParameter("@word2", word2+"%"),
                new SQLiteParameter("@word3", word3)
            });
                DB = new SQLiteDataAdapter(sql_cmd);
                DS.Reset();
                DB.Fill(DS);
                DT = DS.Tables[0];
                
                
                
                List<string> list=new List<string>();
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                   if (!ignore(listBox1.Items[i].ToString()))
                    {
                        list.Add(listBox1.Items[i].ToString());
                   } 
                    
                    
                }
                listBox1.Items.Clear();
              var items = new List<AutocompleteItem>();
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].StartsWith(word2,true,System.Globalization.CultureInfo.CurrentCulture))
	{
                        
	listBox1.Items.Add(list[i]);



    items.Add(new SnippetAutocompleteItem(list[i]));
                     
	}
                
                             
                }
                
                list.Clear();

                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    DataRow dr = DT.Rows[i];

                    int n = listBox1.FindString(dr[0].ToString());
                   // MessageBox.Show(n.ToString() + " " + dr[0].ToString());
                   
                    if (n < 0)
                    {
                        if (!ignore(dr[0].ToString()))
                        {
                            listBox1.Items.Add(dr[0].ToString());
                            items.Add(new SnippetAutocompleteItem(dr[0].ToString()));
                        } 
                       
                    }
                }
                advancedTextEditor1.autocompleteMenu1.SetAutocompleteItems(items);
                if (panel6.Visible == true)
                {
                    autocompleteMenu1.SetAutocompleteItems(items);
                    this.autocompleteMenu1.Show(textBox5, false);
                }
            }
                // sql_con.Close();

            

        }
        AutocompleteMenuNS.AutocompleteMenu autocompleteMenu1=new AutocompleteMenu();
        private void button4_Click(object sender, EventArgs e)
        {

        }
        // get the title of specific paper 
        public string gettitle(int idp)
        {
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = "select title from paper where id=" + idp.ToString();

            //sql_cmd.Parameters.Add(word11);
            //sql_cmd.Parameters.Add(word22);

            DB = new SQLiteDataAdapter(sql_cmd);
            DS.Reset();
            DB.Fill(DS);
            DT = DS.Tables[0];

            idf = -1;

            if (DT.Rows.Count > 0)
            {
                DataRow dr = DT.Rows[0];

                return (dr[0].ToString());

            }
            else
            {
                return "title";
            }


        }

        private void updatecombo(int n) 
        {
           
        }
        DateTime starttime=DateTime.Now;
        private void textchanged(object sender, EventArgs e) 
        {
           
            
            // Do some work
            TimeSpan timeDiff = DateTime.Now - starttime;
            starttime = DateTime.Now;

            


                // MessageBox.Show(advancedTextEditor1.TextEditor.SelectionStart.ToString());
                string s = advancedTextEditor1.TextEditor.Text.Substring(0, advancedTextEditor1.TextEditor.SelectionStart);

                string lastletter="";
                string[] s1 = TokenizeSentence(s);
                string[] sss = TokenizeSentence(advancedTextEditor1.TextEditor.Text);
                updatecombo(s1.Count());
                string s33 = ""; // word 3
            if (s1.Count() > 0)
                {
                lastletter = s.Substring(s.Length - 1, 1); // use it later to know complete or predect
            }
                if (timeDiff.Milliseconds >= 500 ||  lastletter.Equals(" "))
                {
 
            // if there words in the text
                if (s1.Count() > 0)
                {
                    getphrases();//show phrases
                    string s22 = s1[s1.Count() - 1];//take the last word n
                    string s11;
                  
                    if (s1.Count() >= 2)// if they there more then two the take n-1 word
                    {
                        s11 = s1[s1.Count() - 2];
                    }
                    else// one word
                    {
                        // if (s == "") { //listBox1.Items.Clear(); }
                        s11 = "";
                    }
                    if (!s22.Equals(""))
                    {
                        if (sss.Count() > s1.Count())
                        {
                            s33 = sss[s1.Count()];
                        }
                        //MessageBox.Show("S11=" + s11 + "S22" + s22 + "S33" + s33);
                        getnextword(s11, s22, s33, lastletter);
                    };
                }
                else if (s.Length == 0)
                {

                    listBox1.Items.Clear();

                }
            }
          }

        private void textboxclicked(object sender, EventArgs e)
        {
            //show the selected word 

            listBox2.Items.Clear();
            int pos = advancedTextEditor1.TextEditor.SelectionStart;
            int spacepos = 0;
            int spaceposp = 0;

            for (int i = 0; i < pos; i++)
            {
                //  MessageBox.Show(richTextBox1.Text.ToCharArray()[i].ToString());
                if (advancedTextEditor1.TextEditor.Text.ToCharArray()[i].Equals(' '))
                {
                    spaceposp = spacepos;
                    spacepos = i;
                    // MessageBox.Show("space");
                }

            }
            string s = advancedTextEditor1.TextEditor.Text.Substring(spaceposp);

            if (s.Split(' ').Count() > 3)
            {
                label17.Text = s.Split(' ')[1] + "..." + s.Split(' ')[3];
                //MessageBox.Show(s.Split(' ')[1]);
                //MessageBox.Show(s.Split(' ')[2]);
                //MessageBox.Show(s.Split(' ')[3]);
               SQLiteCommand sql_cmd2 = sql_con.CreateCommand();
                //SetConnection();
                //SQLiteParameter word11 = new SQLiteParameter("@word11", SqlDbType.Text) { Value = word1 };
                //SQLiteParameter word22 = new SQLiteParameter("@word22", SqlDbType.Text) { Value = word2 };

                sql_cmd2.CommandText = "select word2 from tgram where word1 like '" + s.Split(' ')[1] + "' and word3 like '" + s.Split(' ')[3] + "' order by freq desc LIMIT 0, 100";

               // MessageBox.Show(sql_cmd2.CommandText);
                //sql_cmd.Parameters.Add(word11);
                //sql_cmd.Parameters.Add(word22);

                SQLiteDataAdapter DB4 = new SQLiteDataAdapter(sql_cmd2);
                DataSet DS4 = new DataSet();
                DS4.Reset();
                DB4.Fill(DS4);
                DataTable DT4= new DataTable(); 
                  DT4 =  DS4.Tables[0];

                
                
                if (DT4.Rows.Count > 0)
                {
                //    MessageBox.Show("Clicked");
                    DataRow dr;
                    
                  //  var items = new List<AutocompleteItem>();
                    
                    for (int i = 0; i < DT4.Rows.Count; i++)
                    {
                        //MessageBox.Show("Clicked2");
                        dr = DT4.Rows[i];
                        int n = listBox2.FindString(dr[0].ToString());
                        if (!ignore(dr[0].ToString()) && n <0)
                        {
                          // MessageBox.Show(dr[0].ToString());
                           listBox2.Items.Add(dr[0].ToString());
                           //items.Add(new SnippetAutocompleteItem(dr[0].ToString()));
                        }

                    }

                    //advancedTextEditor1.autocompleteMenu2.SetAutocompleteItems(items);
                   // advancedTextEditor1.autocompleteMenu2.Show(advancedTextEditor1.TextEditor, true);
                   
                }


            }
            
        
        
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        /*    string s = textBox1.Text;
            getphrases();
            string[] s1 = s.Split(' ');
            string s22 = s1[s1.Count() - 1];
            string s11;
            if (s1.Count() >= 2)
            {
                s11 = s1[s1.Count() - 2];
            }
            else
            {
                s11 = s22;
            }
           // if (!s22.Equals(" ") && !s11.Equals(" ")) { getnextword(s11, s22); };*/
        }

        
          
        
        private void getphrases()
        {
            int hightc = 0;
            //panel1.Controls.Clear();
           
            string[] words = TokenizeSentence(removes(advancedTextEditor1.TextEditor.Text));
            int numberofwords = 0;
            //MessageBox.Show(numberofwords.ToString());
            string txt="";
            int start = 0;

            if ((words.Count() - numberofwords >= 0) && numberofwords>0)
            {
                start = words.Count() - numberofwords;


                if (radioButton1.Checked)
                {

                    txt = String.Join(" ", words, start, numberofwords);
                    
                }
                else
                {
                    txt = String.Join("%", words, start, numberofwords);
                }


            }


            


            if (!txt.Equals(""))
            {



                sql_cmd = sql_con.CreateCommand();
                //MessageBox.Show(txt);



                // sql_cmd.CommandText = "select phrase from phrase,grampaper where phrase.id=grampaper.idp and grampaper.idg =" + idg.ToString() + "and grampaper.type =" + type.ToString();
                sql_cmd.CommandText = "select phrase,idp,id from phrase where phrase like '%" + txt + "%' LIMIT 0, 20";


                SQLiteDataAdapter DB = new SQLiteDataAdapter(sql_cmd);
                DataSet DS = new DataSet();
                DS.Reset();
                DB.Fill(DS);
                DataTable DT = DS.Tables[0];
                
                int my1stPosition;
                if (DT.Rows.Count > 0)
                {
                    panel1.Controls.Clear();
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        DataRow dr = DT.Rows[i];


                        UserControl2 userControl11 = new UserControl2(Convert.ToInt32(dr[1]), Convert.ToInt32(dr[2]), "Authors", dr[0].ToString(), this, Convert.ToInt32(dr[3]));
                        Point p = new Point(0, hightc);
                        userControl11.Location = p;
                        int nblines = (userControl11.richTextBox1.Text.Length / 117) + 1;
                        if (nblines > 4)
                        {
                            nblines = 4;
                        }
                        int lhight = 150 + nblines * 15;
                        MessageBox.Show("hightc = " + hightc.ToString() + ", lhight =" + lhight.ToString());
                        if (lhight > userControl11.Height)
                        {
                            lhight = userControl11.Height;
                        }

                        userControl11.Height = lhight;
                        hightc = hightc + lhight;

                        panel1.Controls.Add(userControl11);
            

                    }
                    txt = String.Join(" ", words, start, numberofwords);
                    if (radioButton1.Checked)
                    {
                        highlight(txt);  

                    }
                    else
                    {
                        for (int i = start; i < start + numberofwords; i++)
                        {
                            highlight(words[i]);
                        }
                         
                    }
                    

                }

                // sql_con.Close();



            }
 
                }

        public void past(string txt) {
            advancedTextEditor1.TextEditor.Text += " " + txt;
        }
        // phraseid = id of the selected phrase
        // text = the searched word 
        // idp = id of the paper
       int nbp = 5;
       string txtg;
       int phraseidg;
       int idpg;
       int chag;


       public void getmorephrases(string txt, int phraseid, int idp,int cha)
       {

           
           txtg = txt;
           phraseidg = phraseid;
           idpg = idp;
           chag = cha;

           sql_cmd = sql_con.CreateCommand();

           sql_cmd.CommandText = "select phrase  from phrase  where id >=   " + (phraseid - nbp).ToString() + " and id <= " + (phraseid + nbp).ToString() + " and idp=" + idp.ToString() + " and cha=" + cha.ToString();

           SQLiteDataAdapter DB = new SQLiteDataAdapter(sql_cmd);
           DataSet DS = new DataSet();
           DS.Reset();
           DB.Fill(DS);
           DataTable DT = DS.Tables[0];

           richTextBox2.Text = "";

           if (DT.Rows.Count > 0)
           {

              
               DataRow dr;



               for (int i = 0; i < DT.Rows.Count; i++)
               {



                   dr = DT.Rows[i];



                   richTextBox2.Text += dr[0].ToString();


               }
               int my1stPosition;
               


               sql_cmd = sql_con.CreateCommand();

               sql_cmd.CommandText = "select refn,reft  from ref  where idp = " + idp+" and cha= "+cha;

               DB = new SQLiteDataAdapter(sql_cmd);
               DS = new DataSet();
               DS.Reset();
               DB.Fill(DS);
              DT = DS.Tables[0];

               richTextBox2.Text += System.Environment.NewLine + "References" + System.Environment.NewLine;
               if (DT.Rows.Count > 0)
               {




                   for (int i = 0; i < DT.Rows.Count; i++)
                   {
                       dr = DT.Rows[i];

                       if (richTextBox2.Text.Contains("[" + (i+1).ToString() + "]"))
                       {
                           richTextBox2.Text += dr[1].ToString() + System.Environment.NewLine;
                           my1stPosition = richTextBox2.Find("[" + (i + 1).ToString() + "]", 0, richTextBox2.Text.Length, RichTextBoxFinds.None);
                                                    
                               richTextBox2.SelectionStart = my1stPosition;
                               richTextBox2.SelectionLength = 4;
                               richTextBox2.SelectionBackColor = Color.GreenYellow;
            
                       }
                       
                   }
                   my1stPosition = richTextBox2.Find("References", 0, richTextBox2.Text.Length, RichTextBoxFinds.MatchCase);
                   if (my1stPosition >= 0)
                   {
                       richTextBox2.SelectionStart = my1stPosition;
                       richTextBox2.SelectionLength = "References".Length;
                       richTextBox2.SelectionFont = new Font(
          richTextBox2.Font.FontFamily,
          16,
          richTextBox2.Font.Style
       );

                       for (int i = 0; i < DT.Rows.Count; i++)
                       {
                           dr = DT.Rows[i];

                           if (richTextBox2.Text.Contains("[" + (i + 1).ToString() + "]"))
                           {
                               my1stPosition = richTextBox2.Find("[" + (i + 1).ToString() + "]", 0, richTextBox2.Text.Length, RichTextBoxFinds.None);
                               while (my1stPosition >0)
                               {
                                   richTextBox2.SelectionStart = my1stPosition;
                                   richTextBox2.SelectionLength = 4;
                                   richTextBox2.SelectionBackColor = Color.GreenYellow;
                                   my1stPosition = richTextBox2.Find("[" + (i + 1).ToString() + "]", my1stPosition+4, richTextBox2.Text.Length, RichTextBoxFinds.None);
                               
                               }
                               
                           }

                       }
                   }
                   my1stPosition = richTextBox2.Find(txt, 0, richTextBox2.Text.Length, RichTextBoxFinds.None);
                   if (my1stPosition >= 0)
                   {
                       richTextBox2.SelectionStart = my1stPosition;
                       richTextBox2.SelectionLength = txt.Length;
                       richTextBox2.SelectionBackColor = Color.Yellow;

                   }
                  

               }

               panel2.Visible = true;
           }
       }


       private string getrefs(string text, int idp) {

           Regex regex = new Regex("(\\[)[0-9]*?\\]");
           var match = regex.Match(text);
           string refs = " <b>References :</b>  <br>";
           while (match.Success)
	            {

                    refs += getref(Convert.ToInt32(match.Value.Substring(1, match.Value.Length - 2)), idp) + "</p>";
              match = match.NextMatch();
	                  }
           refs = refs.Replace("[","<p><font color=\"blue\">[");
           refs = refs.Replace("]", "]</font>");
           
           return refs;
   
       }
       private string getref(int reference,int idp)
       {
           sql_cmd = sql_con.CreateCommand();

               sql_cmd.CommandText = "select reft  from ref  where idp = " + idp+" and refn= "+reference;

               DB = new SQLiteDataAdapter(sql_cmd);
               DS = new DataSet();
               DS.Reset();
               DB.Fill(DS);
               DT = DS.Tables[0];

               richTextBox2.Text += System.Environment.NewLine + "References" + System.Environment.NewLine;
               if (DT.Rows.Count > 0)
               {
                     DataRow  dr = DT.Rows[0];
                     return dr[0].ToString();
                                
                   }
               return "["+reference.ToString()+"] is not indexed, see the paper";
                    
       }

       private string getpaperinfohtml(string path)
       {

           string[] ss = path.Split('\\');
           string papername = ss[ss.Count() - 1];
           ss = papername.Split('-');

           string title="", year="", authors="";
         
           if (ss.Count() >= 3)
           {
               year = ss[0];
              int mm=1;
              while (mm <= ss.Count() - 2)
              {
                  title = title+ss[mm];
                  mm++;
               }
               
               authors = ss[ss.Count()-1].Split('.')[0];
              
           }
           else 
           {
               return papername;
           }

           string result = "<p><b>Paper </b></p></br><p><b>Title:</b>" + title + "</p></br><p><b>Authors: </b>" + authors + "</p></br> <p><b>  Year: </b>" + year + "</p> </br>";
           return result;
           
       }

        private void getphrases(string text)
        {
            int hightc = 0;
           // panel1.Controls.Clear();
            string[] words = TokenizeSentence(removes(text));
            int numberofwords = words.Count();
           SQLiteCommand sql_cmd2;
            //MessageBox.Show(numberofwords.ToString());
            string txt = "";
            int start = 0;

                if (radioButton1.Checked)
                {

                    txt = String.Join(" ", words);

                }
                else
                {
                    txt = String.Join("%", words);
                }





                List<int> list = new List<int>();
                int idpp = -1; // this is used to avoid the repetion of phrases


            if (!txt.Equals(""))
            {
                sql_cmd = sql_con.CreateCommand();
                //MessageBox.Show(txt);

                if (checkBox1.Checked)
                {
                    ExecuteQuery("PRAGMA case_sensitive_like=ON");
                }
                else
                {
                    ExecuteQuery("PRAGMA case_sensitive_like=OFF");
                }    
                // sql_cmd.CommandText = "select phrase from phrase,grampaper where phrase.id=grampaper.idp and grampaper.idg =" + idg.ToString() + "and grampaper.type =" + type.ToString();
                sql_cmd.CommandText = "select phrase.phrase,paper.id,phrase.id,cha,title  from paper, phrase  where paper.id=phrase.idp and phrase  like '%" + txt + "%'  order by phrase.idp LIMIT 0," + textBox2.Text;
                
                SQLiteDataAdapter DB = new SQLiteDataAdapter(sql_cmd);
                DataSet DS = new DataSet();
                DS.Reset();
                DB.Fill(DS);
                DataTable DT = DS.Tables[0];
               // StreamWriter file2 = new StreamWriter(@"map.mm");
               // file2.WriteLine("<map version=\"1.0.1\">");
               // file2.WriteLine("<node TEXT=\""+text+ "\">");
              
              
            
                int my1stPosition;
                if (DT.Rows.Count > 0)
                {
                    panel1.Controls.Clear();
                    object[] arr;
                    DataRow dr;
            
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {      
                        dr = DT.Rows[i];
                      
                        if (dr[1].ToString()=="")
                        {
                            dr[1] = -1; 
                        }

                        if (dr[3].ToString() == "")
                        {
                            dr[3] = -1;
                        }

                        if ((dr[2].ToString() != "") && (Convert.ToInt32(dr[2]) != idpp))
                        {
                              list.Clear();
                              idpp = Convert.ToInt32(dr[1]);
                        }
                        UserControl2 userControl11 = new UserControl2(Convert.ToInt32(dr[1]), Convert.ToInt32(dr[2]), "Authors", dr[0].ToString(), this, Convert.ToInt32(dr[3]));
                       // file2.WriteLine("<node LINK=\"" + dr[4] + "\" TEXT=\"" + dr[0].ToString() + "\">");
                        
                        sql_cmd2 = sql_con.CreateCommand();
                        sql_cmd2.CommandText = "select phrase,id,idp from phrase  where id >=   " + (Convert.ToInt32(dr[2]) - 5).ToString() + " and id <= " + (Convert.ToInt32(dr[2]) + 5).ToString() + " and idp=" + dr[1].ToString();
           SQLiteDataAdapter DB2 = new SQLiteDataAdapter(sql_cmd2);
           DataSet DS2 = new DataSet();
           DS2.Reset();
           DB2.Fill(DS2);
           DataTable DT2 = DS2.Tables[0];
           if (DT2.Rows.Count > 0)
           {
               DataRow dr2;

               for (int l = 0; l < DT2.Rows.Count; l++)
               {
                   dr2=DT2.Rows[l];
                   if (!(list.Contains(Convert.ToInt32(dr2[1]))))
                   {
                      list.Add(Convert.ToInt32(dr2[1]));

                      //file2.WriteLine("<node LINK=\"" + dr[4] + "\" TEXT=\"" + dr2[0].ToString() + "\"> \n");
                     // file2.WriteLine("<richcontent TYPE=\"NOTE\"><html><body> <p><b>Paper</b> </p></br>" + getpaperinfohtml(dr[4].ToString())+"</br>" + getrefs(dr2[0].ToString(), Convert.ToInt32(dr[1])) + "</body></html></richcontent>");
                   //file2.WriteLine("</node>");
                   }
               }
           }
           
          // file2.WriteLine(" <richcontent TYPE=\"NOTE\"><html><body>"+dr[4]+"</body></html></richcontent>");      
          // file2.WriteLine("</node>");



                       
                        Point p = new Point(0, hightc);

                        userControl11.Location = p;
                        int nblines = (userControl11.richTextBox1.Text.Length / 117) + 1;
                        int lhight = 80 + nblines * 13;
                      
                        if (lhight > userControl11.Height-10)
                        {
                            lhight = userControl11.Height-10;
                        }

                        userControl11.Height = lhight;
                        userControl11.Width = panel1.Width;
                        hightc = hightc + lhight;

                        panel1.Controls.Add(userControl11);
                        if (radioButton1.Checked)
                        {
                           userControl11.highlight(txt);

                        }
                        else
                        {
                            for (int j = start; j < start + numberofwords; j++)
                            {
                                userControl11.highlight(words[j]);
                            }

                        }

                    }
                    txt = String.Join(" ", words, start, numberofwords);
                    if (radioButton1.Checked)
                    {
                        highlight(txt);

                    }
                    else
                    {
                        for (int i = start; i < start + numberofwords; i++)
                        {
                            highlight(words[i]);
                        }

                    }


                }

                // sql_con.Close();
               // file2.WriteLine("</node>");
                //file2.WriteLine("</map>");
                //file2.Close();


            }

        }

        private void highlight(string txt)
        {
           
 
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
           /* if (e.KeyCode == Keys.Down)
            {
                int ind = advancedTextEditor1.TextEditor.Text.LastIndexOf(" ");
                string s = "";
                if (ind > 0)
                {
                    s = advancedTextEditor1.TextEditor.Text.Substring(0, ind + 1);
                }


                if (listBox1.Items.Count >= 1)
                {
                    advancedTextEditor1.TextEditor.Text = "";
                    advancedTextEditor1.TextEditor.Text = s + listBox1.Items[0].ToString();
                    advancedTextEditor1.TextEditor.SelectionStart = textBox1.Text.Length;
                }

            }
            */
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            int ind = advancedTextEditor1.TextEditor.Text.LastIndexOf(" ");
            string s = "";
            if (ind > 0)
            {
                s = advancedTextEditor1.TextEditor.Text.Substring(0, ind + 1); ;
            }


            if (listBox1.Items.Count >= 1)
            {

                advancedTextEditor1.TextEditor.Text = s + listBox1.Items[listBox1.SelectedIndex].ToString();
                advancedTextEditor1.TextEditor.SelectionStart = advancedTextEditor1.TextEditor.Text.Length;
            }
        }

        private void phrases1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            getphrases();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            getphrases();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            getphrases();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form3 fr = new Form3();
            fr.Show();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

          
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            source2 = new SQLiteConnection
("Data Source=" + Path.Combine(appDir, "categories.db") + ";Version=3;New=False;Compress=True;");
            source2.Open();
            //SetConnection();
            sql_cmd = source2.CreateCommand();
            sql_cmd.CommandText = @"select id,name from category";
            DB = new SQLiteDataAdapter(sql_cmd);
            DS.Reset();
            DB.Fill(DS);
            DT3 = new DataTable();
            DT3 = DS.Tables[0];
            DataRow dr = DT3.Rows[comboBox2.SelectedIndex];


            var dbName = Path.Combine(Environment.GetFolderPath(
Environment.SpecialFolder.ApplicationData), "YouWrite", dr[0].ToString() + ".db");


            source = new SQLiteConnection
             ("Data Source=" + dbName +";Version=3;New=False;Compress=True;");
            SetConnection();
       
            textchanged(sender,e);
            source2.Close();
        }

        private void Form2_Activated(object sender, EventArgs e)
        {
            selectdb();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
           getphrases(advancedTextEditor1.TextEditor.SelectedText);

            /*Translator tr = new Translator();
            tr.SourceLanguage = "french";
            tr.TargetLanguage = "english";
            tr.SourceText = advancedTextEditor1.TextEditor.SelectedText;
            tr.Translate();
            MessageBox.Show(tr.Translation);*/

            
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //show the selected word 

            listBox2.Items.Clear();
            int pos = advancedTextEditor1.TextEditor.SelectionStart;
            int spacepos = 0;
            int spaceposp = 0;

            for (int i = 0; i < pos; i++)
            {
                //  MessageBox.Show(richTextBox1.Text.ToCharArray()[i].ToString());
                if (advancedTextEditor1.TextEditor.Text.ToCharArray()[i].Equals(' '))
                {
                    spaceposp = spacepos;
                    spacepos = i;
                    // MessageBox.Show("space");
                }

            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            nbp++;
            getmorephrases(txtg, phraseidg, idpg,chag);
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            nbp--;
            getmorephrases(txtg, phraseidg, idpg,chag);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel3.Visible = true; 
            
        }


        private void getcontext(int idp,int refn,int nb,string reft) 
        { 
         sql_cmd = sql_con.CreateCommand();

         sql_cmd.CommandText = "select id,phrase from phrase  where phrase like '%[" + refn.ToString() + "]%' and idp =" + idp.ToString();

            SQLiteDataAdapter DB = new SQLiteDataAdapter(sql_cmd);
            DataSet DS = new DataSet();
            DS.Reset();
            DB.Fill(DS);
            DataTable DT = DS.Tables[0];

            string txt = "";

            if (DT.Rows.Count > 0)
            {


                DataRow dr;
                SQLiteDataAdapter DB2;
                DataSet DS2;
                DataTable DT2;
                DataRow dr2;
                
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    dr = DT.Rows[i];

                    sql_cmd = sql_con.CreateCommand();

                    sql_cmd.CommandText = "select phrase  from phrase  where id >=   " + (Convert.ToInt32(dr[0].ToString()) - nb).ToString() + " and id <= " + (Convert.ToInt32(dr[0].ToString()) + nb).ToString() + " and idp=" + idp.ToString();
                    DB2 = new SQLiteDataAdapter(sql_cmd);
                    DS2 = new DataSet();
                    DS2.Reset();
                    DB2.Fill(DS2);
                    DT2 = DS2.Tables[0];


                    if (DT2.Rows.Count > 0)
                    {
                        
                        for (int j = 0; j < DT2.Rows.Count; j++)
                        {
                            dr2 = DT2.Rows[j];
                            txt += dr2[0].ToString();
                           // richTextBox3.Text += dr2[0].ToString();
                        }

                    }

                    txt += System.Environment.NewLine + "..." + System.Environment.NewLine; 
                  //  richTextBox3.Text += System.Environment.NewLine + "..." + System.Environment.NewLine + reft + System.Environment.NewLine;
                    
                }

            }
            
         
            if (!txt.Equals(""))
            {
                
                UserControl3 uc = new UserControl3(gettitle(idp), txt,this,reft);
                Point p = new Point(0, hightc3);
                uc.Location = p;
                int nblines = (txt.Length / 115) + 2;
               // if (nblines > 12)
                //{
                 //   nblines = 12;
                //}
                int lhight =nblines*17;
                //if (lhight > 85)
                //{
                uc.Size = new Size(uc.Size.Width,uc.Size.Height+lhight+30);    
               // uc.sety(uc.Size.Height + lhight);
                    
                //}
                uc.highlight("[" + refn.ToString() + "]");
               
                
                panel4.Controls.Add(uc);
                hightc3 += uc.Size.Height;
                
            }
           
                            
            //richTextBox3.Text += System.Environment.NewLine + "------------------------------------------------------------" + System.Environment.NewLine;
        }
        int hightc3=0;
        private void button10_Click(object sender, EventArgs e)
        {
           
            sql_cmd = sql_con.CreateCommand();
            
            sql_cmd.CommandText = "select idp,refn,reft  from ref  where reft like '%"+textBox3.Text+"%' ";

            SQLiteDataAdapter DB = new SQLiteDataAdapter(sql_cmd);
            DataSet DS = new DataSet();
            DS.Reset();
            DB.Fill(DS);
            DataTable DT = DS.Tables[0];

            richTextBox3.Text = "";
            panel4.Controls.Clear();
            if (DT.Rows.Count > 0)
            {

                label20.Text = DT.Rows.Count.ToString();
                DataRow dr;


                hightc3 = 0;
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    dr = DT.Rows[i];        
                    getcontext(Convert.ToInt32(dr[0].ToString()), Convert.ToInt32(dr[1].ToString()), Convert.ToInt32(textBox4.Text), dr[2].ToString());
                
                }
                int my1stPosition;
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    dr = DT.Rows[i];

                    if (richTextBox3.Text.Contains("[" + dr[1].ToString() + "]"))
                    {

                        my1stPosition = richTextBox3.Find("[" + (dr[1].ToString()).ToString() + "]", 0, richTextBox3.Text.Length, RichTextBoxFinds.None);

                        while (my1stPosition > 0)
                        {
                            richTextBox3.SelectionStart = my1stPosition;
                            richTextBox3.SelectionLength = 4;
                            richTextBox3.SelectionBackColor = Color.GreenYellow;
                            my1stPosition = richTextBox3.Find("[" + (dr[1].ToString()).ToString() + "]", my1stPosition+4, richTextBox3.Text.Length, RichTextBoxFinds.None);
                        }


                    }

                }

            }
            else
            {
                label20.Text = "0";
            }
            panel4.Update();
            panel4.Refresh();
                }

        private void button11_Click(object sender, EventArgs e)
        {
            panel3.Visible = false; 
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            panel5.Visible = true;
        }
        public void addtoclip(string text)
        {
            richTextBox4.Text += System.Environment.NewLine + "-------------------------" + System.Environment.NewLine + text;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
        }

        private void button13_Click(object sender, EventArgs e)
        {
         
        }

        private void button13_Click_1(object sender, EventArgs e)
        {
            if (richTextBox2.SelectedText.Equals(""))
            {
                addtoclip(richTextBox2.Text);
            }
            else {
                addtoclip(richTextBox2.SelectedText);
            }
            
        }

        private void button14_Click(object sender, EventArgs e)
        {
            richTextBox4.Text = "";
        }
        void review() {
            yusing = 0;
            panel7.Controls.Clear();
            panel7.Visible = false;
            string[] tokens = TokenizeSentence(textBox5.Text);
            if (tokens.Length >= 2)
            {
                for (int i = 0; i < tokens.Length - 1; i++)
                {
                    getusing(tokens[i], tokens[i + 1]);
                }
            }
            panel7.Visible = true;
        
        }
        private void button15_Click(object sender, EventArgs e)
        {
            review(); 
                    
        }

        private void button16_Click(object sender, EventArgs e)
        {
            panel6.Visible = false;
            /*string s = reviewingsentences[0]+" ";
            for (int i = 1; i < reviewingsentences.Length; i++)
			{
                s = String.Concat(s, reviewingsentences[i] + " ");
			}*/
            reviewingsentences[indexreview] = textBox5.Text;
            advancedTextEditor1.TextEditor.Text = String.Concat(reviewingsentences);

        }
        string[] reviewingsentences;
        int indexreview;
        private void button17_Click(object sender, EventArgs e)
        {
            panel7.Controls.Clear();
            
            indexreview = 0;
            reviewingsentences = SplitSentences(advancedTextEditor1.TextEditor.Text);
            textBox5.Text = reviewingsentences[0];
            panel6.Visible = true;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            reviewingsentences[indexreview] = textBox5.Text;
            if ((indexreview + 1) < reviewingsentences.Length)
            {
                indexreview++;
                textBox5.Text = reviewingsentences[indexreview];
            }
            review();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            reviewingsentences[indexreview] = textBox5.Text;
            if ((indexreview - 1) >= 0)
            {
                indexreview--;
                textBox5.Text = reviewingsentences[indexreview];
            }
            review();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            getphrases(textBox5.SelectedText);
        }

        private void advancedTextEditor1_Load(object sender, EventArgs e)
        {

        }

        

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            // MessageBox.Show(advancedTextEditor1.TextEditor.SelectionStart.ToString());
           


                string s = textBox5.Text.Substring(0, textBox5.SelectionStart);

                string lastletter;
                string[] s1 = TokenizeSentence(s);
                string[] sss = TokenizeSentence(textBox5.Text);
                updatecombo(s1.Count());
                string s33 = ""; // word 3

                // if there words in the text
                if (s1.Count() > 0)
                {
                    getphrases();//show phrases
                    string s22 = s1[s1.Count() - 1];//take the last word n
                    string s11;
                    lastletter = s.Substring(s.Length - 1, 1); // use it later to know complete or predect
                    if (s1.Count() >= 2)// if they there more then two the take n-1 word
                    {
                        s11 = s1[s1.Count() - 2];
                    }
                    else// one word
                    {
                        // if (s == "") { //listBox1.Items.Clear(); }
                        s11 = "";
                    }
                    if (!s22.Equals(""))
                    {
                        if (sss.Count() > s1.Count())
                        {
                            s33 = sss[s1.Count()];
                        }
                        //MessageBox.Show("S11=" + s11 + "S22" + s22 + "S33" + s33);
                        getnextword(s11, s22, s33, lastletter);
                    };
                }
                else if (s.Length == 0)
                {

                    listBox1.Items.Clear();
                    
                }
        }

        private void textBox5_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            int pos = textBox5.SelectionStart;
            int spacepos = 0;
            int spaceposp = 0;

            for (int i = 0; i < pos; i++)
            {
                //  MessageBox.Show(richTextBox1.Text.ToCharArray()[i].ToString());
                if (textBox5.Text.ToCharArray()[i].Equals(' '))
                {
                    spaceposp = spacepos;
                    spacepos = i;
                    // MessageBox.Show("space");
                }

            }
            string s = textBox5.Text.Substring(spaceposp);

            if (s.Split(' ').Count() > 3)
            {
                label17.Text = s.Split(' ')[1] + "..." + s.Split(' ')[3].Split('.',',','!','?')[0];
                //MessageBox.Show(s.Split(' ')[1]);
                //MessageBox.Show(s.Split(' ')[2]);
                //MessageBox.Show(s.Split(' ')[3]);
                SQLiteCommand sql_cmd2 = sql_con.CreateCommand();
                //SetConnection();
                //SQLiteParameter word11 = new SQLiteParameter("@word11", SqlDbType.Text) { Value = word1 };
                //SQLiteParameter word22 = new SQLiteParameter("@word22", SqlDbType.Text) { Value = word2 };

                sql_cmd2.CommandText = "select word2 from tgram where word1 like '" + s.Split(' ')[1] + "' and word3 like '" + s.Split(' ')[3].Split('.', ',', '!', '?')[0] + "' order by freq desc LIMIT 0, 100";

                // MessageBox.Show(sql_cmd2.CommandText);
                //sql_cmd.Parameters.Add(word11);
                //sql_cmd.Parameters.Add(word22);

                SQLiteDataAdapter DB4 = new SQLiteDataAdapter(sql_cmd2);
                DataSet DS4 = new DataSet();
                DS4.Reset();
                DB4.Fill(DS4);
                DataTable DT4 = new DataTable();
                DT4 = DS4.Tables[0];



                if (DT4.Rows.Count > 0)
                {
                    //    MessageBox.Show("Clicked");
                    DataRow dr;

                    //  var items = new List<AutocompleteItem>();

                    for (int i = 0; i < DT4.Rows.Count; i++)
                    {
                        //MessageBox.Show("Clicked2");
                        dr = DT4.Rows[i];
                        int n = listBox2.FindString(dr[0].ToString());
                        if (!ignore(dr[0].ToString()) && n < 0)
                        {
                            // MessageBox.Show(dr[0].ToString());
                            listBox2.Items.Add(dr[0].ToString());
                            //items.Add(new SnippetAutocompleteItem(dr[0].ToString()));
                        }

                    }

                    //advancedTextEditor1.autocompleteMenu2.SetAutocompleteItems(items);
                    // advancedTextEditor1.autocompleteMenu2.Show(advancedTextEditor1.TextEditor, true);

                }


            }
            
        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            sql_con.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void phrases1_Load_1(object sender, EventArgs e)
        {

        }

        private void advancedTextEditor1_Load_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {

        }

        
              }
    

}
