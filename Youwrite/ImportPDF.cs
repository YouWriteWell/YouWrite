using System;
using System.Text.RegularExpressions;
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
namespace YouWrite
{
    public partial class Form4 : Form
    {

        private string appDir; // data directory 
        public Form4()
        {
            InitializeComponent();

            appDir = Path.Combine(Environment.GetFolderPath(
   Environment.SpecialFolder.ApplicationData), "YouWrite");
            dir = System.IO.Directory.GetCurrentDirectory();

            source = new SQLiteConnection
              ("Data Source=" + Path.Combine(appDir, "categories.db") + ";Version=3;New=False;Compress=True;");


            mModelPath = dir + @"\";
        }
        private OpenNLP.Tools.SentenceDetect.MaximumEntropySentenceDetector mSentenceDetector;
        private OpenNLP.Tools.Tokenize.EnglishMaximumEntropyTokenizer mTokenizer;
        private string mModelPath;

        private SQLiteConnection sql_con;
        private SQLiteConnection source;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private DataTable DT2; 
        string dir;
        int idc;
        string cat;
        DialogResult result;
        string foldername;
        string filename;
        private void ExecuteQuery(string txtQuery)
        {
            //SetConnection();
            //sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = txtQuery;
            sql_cmd.ExecuteNonQuery();
            //sql_con.Close();
        }
        // To chuck if this b-gram existes 
        private int seqexist2gram(string word1, string word2)
        {
            //SetConnection();
            // sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = "select id from bgram where word1=@word1 and word2 =@word2";
            sql_cmd.Parameters.AddRange(new[]{
                new SQLiteParameter("@word1", word1),
                new SQLiteParameter("@word2", word2)
            });

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
            sql_cmd.CommandText = "select id from tgram where word1=@word1 and word2 =@word2 and word3 = @word3";
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
        public Form4(string category,int id)
        {
            InitializeComponent();
            idc = id;
            dir = System.IO.Directory.GetCurrentDirectory();

            var dbName = Path.Combine(Environment.GetFolderPath(
    Environment.SpecialFolder.ApplicationData), "YouWrite", id.ToString() + ".db");

            source = new SQLiteConnection
   ("Data Source="+dbName +";Version=3;New=False;Compress=True;");



            mModelPath = @"\";
            cat = category;
            this.Text ="Import documents ("+cat+")";
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            result = this.folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                foldername = this.folderBrowserDialog1.SelectedPath;
                listfiles(foldername);
            }

        }
        private void listfiles(string foldername)
        {
            
            DT2 = new DataTable();
            DT2.Columns.Add("Number", typeof(int));
            DT2.Columns.Add("Paper", typeof(string));
            DT2.Columns.Add("State", typeof(string));


            int i = 1;
            foreach (string f in Directory.GetFiles(foldername))
            {
                if ((f.Split('.').Count() >= 2) && (f.Split('.')[f.Split('.').Count()-1] == "pdf"))
                {
                    DT2.Rows.Add(i, f, " ");
                    i++;
                }

            }
            dataGridView1.DataSource = DT2;
        }
        public delegate int aaddphrase(string valeur,int idp);
        public delegate void iinsertword(string[] phrase, int id);

        private void loadingfiles()
        {
            if (result == DialogResult.OK)
            {

                SetConnection();
                int i = 0;
               
                foreach (string f in Directory.GetFiles(foldername))
                {
                   
                    if ((f.Split('.').Count() >= 2) && (f.Split('.')[f.Split('.').Count() - 1] == "pdf"))
                    {
                        filename = f;
                        loaddata(i);
                        i++;

                    }
                    


                }


                CloseConnection();
                SetConnection();
                MessageBox.Show("Opearation completed !");
                
            }

        }
       
        private void button2_Click(object sender, EventArgs e)
        {
            Thread t1 = new Thread(new ThreadStart(loadingfiles));
            t1.IsBackground = true;
            t1.Start();
            button2.Enabled = false;

        }
      
        //ignore this caracters 
        private bool ignore(string s)
        {

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
            var CommandText = new SQLiteCommand("insert into  bgram (word1,word2,freq) values (@word1,@word2,0)", sql_con);
            CommandText.Parameters.AddRange(new[]{
                new SQLiteParameter("@word1", word1),
                new SQLiteParameter("@word2", word2)
            });
            
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

            var CommandText = new SQLiteCommand("insert into  tgram (word1,word2,word3,freq) values (@word1,@word2,@word3,0)", sql_con);
            CommandText.Parameters.AddRange(new[]{
                new SQLiteParameter("@word1", word1),
                new SQLiteParameter("@word2", word2),
                new SQLiteParameter("@word3", word3)
            });
            
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
        private int AddPhrase(string phrase,int idp)
        {
            // SetConnection();
            // sql_con.Open();


            var CommandText = new SQLiteCommand("insert into  phrase (phrase,idp,cha) values (@phrase,@idp,@cha)", sql_con);
            CommandText.Parameters.AddRange(new[]{
                new SQLiteParameter("@phrase", phrase),
                new SQLiteParameter("@idp", idp),
                new SQLiteParameter("@cha", chapter)
            });
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


        int pp = 0;
        private void setstate(int curr, int max, int papern)
        {
            int p;
            p = (int)curr * 100 / max;
            if (p >= pp + 5)
            {
                DT2.Rows[papern][2] = p.ToString("0.00") + "%";

                dataGridView1.DataSource = DT2;
                pp = p;
            }
            if (pp == 100)
            {
                pp = 0;
            }
        }

        //add 3-gram
        private void addref(int idp, string reft,int refn)
        {
            // SetConnection();
            // sql_con.Open();

            var cmd = new SQLiteCommand("insert into  ref (idp,reft,refn,cha) values (@idp,@reft,@refn,@cha)", sql_con);
            cmd.Parameters.AddRange(new[]{
                new SQLiteParameter("@idp", idp),
                new SQLiteParameter("@reft", reft),
                new SQLiteParameter("@refn", refn),
                new SQLiteParameter("@cha", chapter)
            });

            cmd.ExecuteNonQuery();
        }
        int nberr = 0;
        int nbcorr = 0;
        private void addrefs(string refes,int idp) 
        {
            int k = 1;
            bool cont = true;
            int pos1, pos2;
            int startindex = 0;

             string pattern1 = @"[References|REFERENCES][ ]+\[[0-9]+\] ";
                   

                   
            if (Regex.IsMatch(refes, pattern1))
            {
                while (cont)
                {

                    pos1 = refes.IndexOf("[" + k.ToString() + "]", startindex);
                    startindex = pos1;
                    pos2 = refes.IndexOf("[" + (k + 1).ToString() + "]", startindex);
                    startindex = pos2;
                    if (pos2 >= 1 && pos1 >= 1 )
                    {
                        addref(idp, refes.Substring(pos1, pos2 - pos1), k);
                    }
                    else if (pos1 >= 1)
                    {
                        addref(idp, refes.Substring(pos1), k);
                        cont = false;
                    }
                    else
                    {
                        cont = false;
                    }
                    k++;
                }

            }
            else
            {


                
                while (cont)
                {

                    pos1 = refes.IndexOf(k.ToString() + ". ",startindex);
                    startindex = pos1;
                    if (pos1 < 0)
                    { // If we don't extract the reference we councel the referencing process Look last else
                        pos2 = -1;
                    }
                    else
                    {
                        pos2 = refes.IndexOf((k + 1).ToString() + ". ", startindex);
                        startindex = pos2;
                    }
                   
                    if (pos2 >= 1 && pos1 >= 1)
                    {
                        addref(idp, refes.Substring(pos1, pos2 - pos1), k);
                    }
                    else if (pos1 >= 1)
                    {

                        addref(idp, refes.Substring(pos1), k);
                        cont = false;
                    }
                    else
                    {
                        cont = false;
                    }
                    k++;
                }

            }
        
        }
        int chapter;
        int nbref=0;
        private void loaddata(int pn )
        {
            //SetConnection();
            //sql_con.Open();
            bool error = false;
            chapter = 1;
         try
         {
                PDDocument doc = PDDocument.load(filename);
                PDFTextStripper stripper = new PDFTextStripper("UTF-8");
                // MessageBox.Show(stripper.getText(doc));
                string text = stripper.getText(doc);
               
                //MessageBox.Show(filename2);
                var CommandText = new SQLiteCommand("insert into  paper (title) values ('"+filename+ "')", sql_con);
                CommandText.ExecuteNonQuery();
                sql_cmd = sql_con.CreateCommand();

                sql_cmd.CommandText = @"select last_insert_rowid() as rowid";
                DB = new SQLiteDataAdapter(sql_cmd);
                DS.Reset();
                DB.Fill(DS);
                DT = DS.Tables[0];
                int idp;

                if (DT.Rows.Count > 0)
                {
                    DataRow dr = DT.Rows[0];
                    idp = Convert.ToInt32(dr[0].ToString());
                    // sql_con.Close();

                }
                else 
                {
                    idp = -1;
                }
                
                string s = text.Replace(System.Environment.NewLine, " ");

                string[] sentences =SplitSentences(s);
                int nbs = sentences.Count();
                
                string s3;
                int i=0;
                int l=1;
                string pattern1 = l.ToString() + ". ";
                string pattern2 = @"[\[1+\]|\[l+\]] ";
                bool refe=false; // used to index references
                string refes=""; // all references 
                int nbletters = 0; // if we counter 5 seccusive line without any reference we start new chapter
               int nbsentences = 0; 
               foreach (string sentence in sentences)
                {
                    nbsentences++;
                    s3 = Form2.removes(sentence);
                    // MessageBox.Show("I'm here working/" + s3.Normalize() + "===>" + s3.Length.ToString());
                    i++;
                    setstate(i, nbs, pn);
                    int id = (int)Invoke((aaddphrase)AddPhrase, s3,idp); //AddPhrase(s3);
                    string[] tokens = TokenizeSentence(s3);
                  
                    if (tokens.Count() >= 2) { Invoke((iinsertword)InsertWords, tokens, id); }
                   
                    if (sentence.Contains("References") || sentence.Contains("REFERENCES"))
                    {
                        refe = true;
                        l = 1;
                    }
                    

                    if ((Regex.IsMatch(sentence, pattern1) || Regex.IsMatch(sentence, pattern2))&&refe)
                    {
                        nbletters = 0;
                        l++;
                       
                        pattern1 = l.ToString() + ". ";
                        pattern2 = @"\[" + l.ToString() + @"+\] ";
                    }
                    else if (refe)
                    {
                       
                       
                        nbletters += sentence.Length;
                        
                    
                    }


                    if ((nbletters >= 500|| nbsentences >= sentences.Length ) && refe)
                    {
                        addrefs(refes, idp);
                        
                        nbref++;
                        chapter++;
                        refes = "";
                    
                        refe = false;
                        nbletters = 0;
                    }   
                    
                    if (refe)
                    {
                        refes += sentence;
                    }
               }
                
                

         }
         catch(Exception ex)
        {
                error = true;
                DT2.Rows[pn][2] = "Error: "+ex.Message;
                nberr++;
       
              //  MessageBox.Show(ex.Message);
              
     } 
            // CloseConnection();
            //SetConnection();
          /* if(!error){
           DT2.Rows[pn][2] = "Completed";
           nbcorr++;
           
       
            } */
}
        public string parseUsingPDFBox(string input)
        {
            PDDocument doc = PDDocument.load(input);
            PDFTextStripper stripper = new PDFTextStripper();
            return stripper.getText(doc);
        }
        public string[] SplitSentences(string paragraph)
        {
            if (mSentenceDetector == null)
            {
                mSentenceDetector = new OpenNLP.Tools.SentenceDetect.EnglishMaximumEntropySentenceDetector(mModelPath + "EnglishSD.nbin");
            }

            return mSentenceDetector.SentenceDetect(paragraph);
        }
        public string[] TokenizeSentence(string sentence)
        {
            if (mTokenizer == null)
            {
                mTokenizer = new OpenNLP.Tools.Tokenize.EnglishMaximumEntropyTokenizer(mModelPath + "EnglishTok.nbin");
            }

            return mTokenizer.Tokenize(sentence);
        }

    }
}
