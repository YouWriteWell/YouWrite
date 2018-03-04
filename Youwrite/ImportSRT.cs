using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.util;
using OpenNLP.Tools.SentenceDetect;
using OpenNLP.Tools.Tokenize;

namespace YouWrite
{
    public partial class Form7 : Form
    {
        public delegate int aaddphrase(string valeur, int idp);

        public delegate void iinsertword(string[] phrase, int id);

        private readonly string appDir; // data directory 
        private readonly string cat;
        private int chapter;
        private SQLiteDataAdapter DB;
        private readonly string dir;
        private readonly DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private DataTable DT2;
        private string filename;
        private string foldername;
        private int idc;
        private readonly string mModelPath;
        private MaximumEntropySentenceDetector mSentenceDetector;
        private EnglishMaximumEntropyTokenizer mTokenizer;
        private int nbcorr = 0;
        private int nberr;
        private int nbref;

        private int pp;
        private DialogResult result;
        private readonly SQLiteConnection source;
        private SQLiteCommand sql_cmd;

        private SQLiteConnection sql_con;

        public Form7()
        {
            InitializeComponent();
            dir = Directory.GetCurrentDirectory();
            appDir = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData), "YouWrite");
            dir = Directory.GetCurrentDirectory();

            source = new SQLiteConnection
                ("Data Source=" + Path.Combine(appDir, "categories.db") + ";Version=3;New=False;Compress=True;");


            mModelPath = dir + @"\";
        }

        public Form7(string category, int id)
        {
            InitializeComponent();
            idc = id;
            dir = Directory.GetCurrentDirectory();

            var dbName = Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.ApplicationData), "YouWrite", id + ".db");

            source = new SQLiteConnection
                ("Data Source=" + dbName + ";Version=3;New=False;Compress=True;");


            mModelPath = dir + @"\";
            cat = category;
            Text = "Import documents (" + cat + ")";
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

        // To chuck if this b-gram existes 
        private int seqexist2gram(string word1, string word2)
        {
            var encod = Encoding.Default.GetBytes(word1);
            var word11 = Encoding.UTF8.GetString(encod);

            encod = Encoding.Default.GetBytes(word2);
            var word22 = Encoding.UTF8.GetString(encod);


            //SetConnection();
            // sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = "select id from bgram where word1=@word1 and word2 =@word2";
            sql_cmd.Parameters.AddRange(new[]
            {
                new SQLiteParameter("@word1", word11),
                new SQLiteParameter("@word2", word22)
            });

            DB = new SQLiteDataAdapter(sql_cmd);
            DS.Reset();
            DB.Fill(DS);
            DT = DS.Tables[0];

            if (DT.Rows.Count > 0)
            {
                var dr = DT.Rows[0];
                var id = Convert.ToInt32(dr[0].ToString());
                // sql_con.Close();
                return id;
            }

            // sql_con.Close();
            return -1;
        }

        // To chuck if this 3-gram existes 
        private int seqexist3gram(string word1, string word2, string word3)
        {
            // SetConnection();
            // sql_con.Open();

            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = "select id from tgram where word1=@word1 and word2 =@word2 and word3 = @word3";
            sql_cmd.Parameters.AddRange(new[]
            {
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
                var dr = DT.Rows[0];
                var id = Convert.ToInt32(dr[0].ToString());
                // sql_con.Close();
                return id;
            }

            //    sql_con.Close();
            return -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                foldername = folderBrowserDialog1.SelectedPath;
                listfiles(foldername);
            }
        }

        private void listfiles(string foldername)
        {
            DT2 = new DataTable();
            DT2.Columns.Add("Number", typeof(int));
            DT2.Columns.Add("Paper", typeof(string));
            DT2.Columns.Add("State", typeof(string));


            var i = 1;
            foreach (var f in Directory.GetFiles(foldername))
                if (f.Split('.').Count() >= 2 && f.Split('.')[f.Split('.').Count() - 1] == "srt")
                {
                    DT2.Rows.Add(i, f, " ");
                    i++;
                }

            dataGridView1.DataSource = DT2;
        }

        private void loadingfiles()
        {
            if (result == DialogResult.OK)
            {
                SetConnection();
                var i = 0;

                foreach (var f in Directory.GetFiles(foldername))
                    if (f.Split('.').Count() >= 2 && f.Split('.')[f.Split('.').Count() - 1] == "srt")
                    {
                        filename = f;
                        loaddata(i);
                        i++;
                    }


                CloseConnection();
                SetConnection();
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

        //ignore this caracters 
        private bool ignore(string s)
        {
            string[] chars =
            {
                "/", "!", "@", "#", "$", "%", "^", "&", "*", "%", "'", "\"", "|", "{", "}", "[", "]", ",", ".", "=",
                "$", "-", "<", ">", ";", ")", "(", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"
            };
            for (var i = 0; i < chars.Length; i++)
                if (s.Contains(chars[i]))
                    return true;
            return false;
        }

        private int Add(string word1, string word2)
        {
            //SetConnection();
            // sql_con.Open();
            var CommandText =
                new SQLiteCommand("insert into  bgram (word1,word2,freq) values (@word1,@word2,0)", sql_con);
            CommandText.Parameters.AddRange(new[]
            {
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
                var dr = DT.Rows[0];
                var id = Convert.ToInt32(dr[0].ToString());
                // sql_con.Close();
                return id;
            }

            //  sql_con.Close();
            return -1;
        }

        //add 3-gram
        private int Add(string word1, string word2, string word3)
        {
            // SetConnection();
            // sql_con.Open();

            var CommandText =
                new SQLiteCommand("insert into  tgram (word1,word2,word3,freq) values (@word1,@word2,@word3,0)",
                    sql_con);
            CommandText.Parameters.AddRange(new[]
            {
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


            var dr = DT.Rows[0];
            var id = Convert.ToInt32(dr[0].ToString());
            // sql_con.Close();
            return id;
        }

        private void Update2gram(int id)
        {
            var txtSQLQuery = "update bgram set freq =(freq+1) where id = " + id;
            ExecuteQuery(txtSQLQuery);
        }

        private void Update3gram(int id)
        {
            var txtSQLQuery = "update tgram set freq =(freq+1) where id = " + id;
            ExecuteQuery(txtSQLQuery);
        }

        //add phrase
        private int AddPhrase(string phrase, int idp)
        {
            // SetConnection();
            // sql_con.Open();

            var encod = Encoding.Default.GetBytes(phrase);
            var phrase1 = Encoding.UTF8.GetString(encod);


            var CommandText =
                new SQLiteCommand("insert into  phrase (phrase,idp,cha) values (@phrase,@idp,@cha)", sql_con);
            CommandText.Parameters.AddRange(new[]
            {
                new SQLiteParameter("@phrase", phrase1),
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
                    var dr = DT.Rows[0];
                    var id = Convert.ToInt32(dr[0].ToString());
                    // sql_con.Close();
                    return id;
                }
            }
            catch
            {
            }

            // sql_con.Close();
            return -1;
        }

        //add phrasegramrelation
        private void AddPhraseGram(int idp, int idg, int type)
        {
            // SetConnection();
            // sql_con.Open();
            var CommandText =
                new SQLiteCommand("insert into  grampaper (idp,idg,type) values (" + idp + "," + idg + "," + type + ")",
                    sql_con);
            CommandText.ExecuteNonQuery();
            // sql_con.Close();
        }

        // insert words in database
        private void InsertWords(string[] words, int idphrase)
        {
            int id2, id3;
            var word1 = words[0];
            var word2 = words[1];
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


            for (var i = 2; i < words.Count(); i++)
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

            //  db file to memory

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

        private void setstate(int curr, int max, int papern)
        {
            int p;
            p = curr * 100 / max;
            if (p >= pp + 5)
            {
                DT2.Rows[papern][2] = p.ToString("0.00") + "%";

                dataGridView1.DataSource = DT2;
                pp = p;
            }

            if (pp == 100) pp = 0;
        }

        //add 3-gram
        private void addref(int idp, string reft, int refn)
        {
            // SetConnection();
            // sql_con.Open();

            var cmd = new SQLiteCommand("insert into  ref (idp,reft,refn,cha) values (@idp,@reft,@refn,@cha)", sql_con);
            cmd.Parameters.AddRange(new[]
            {
                new SQLiteParameter("@idp", idp),
                new SQLiteParameter("@reft", reft),
                new SQLiteParameter("@refn", refn),
                new SQLiteParameter("@cha", chapter)
            });

            cmd.ExecuteNonQuery();
        }

        private void addrefs(string refes, int idp)
        {
            var k = 1;
            var cont = true;
            int pos1, pos2;
            var startindex = 0;

            var pattern1 = @"[References|REFERENCES][ ]+\[[0-9]+\] ";


            if (Regex.IsMatch(refes, pattern1))
                while (cont)
                {
                    pos1 = refes.IndexOf("[" + k + "]", startindex);
                    startindex = pos1;
                    pos2 = refes.IndexOf("[" + (k + 1) + "]", startindex);
                    startindex = pos2;
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
            else
                while (cont)
                {
                    pos1 = refes.IndexOf(k + ". ", startindex);
                    startindex = pos1;
                    if (pos1 < 0)
                    {
                        // If we don't extract the reference we councel the referencing process Look last else
                        pos2 = -1;
                    }
                    else
                    {
                        pos2 = refes.IndexOf(k + 1 + ". ", startindex);
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


        public static Encoding GetEncoding(string filename)
        {
            // Read the BOM
            var bom = new byte[4];
            using (var file = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                file.Read(bom, 0, 4);
            }

            // Analyze the BOM
            if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76) return Encoding.UTF7;
            if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) return Encoding.UTF8;
            if (bom[0] == 0xff && bom[1] == 0xfe) return Encoding.Unicode; //UTF-16LE
            if (bom[0] == 0xfe && bom[1] == 0xff) return Encoding.BigEndianUnicode; //UTF-16BE
            if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff) return Encoding.UTF32;
            return Encoding.ASCII;
        }

        private void loaddata(int pn)
        {
            //SetConnection();
            //sql_con.Open();
            var error = false;
            chapter = 1;
            try
            {
                string text;
                using (var streamReader = new StreamReader(filename, Encoding.ASCII, true))
                {
                    streamReader.Peek();
                    var textt = streamReader.ReadToEnd();
                    var souceSrt = Regex.Split(textt,
                        @"(?:\r?\n)*\d+\r?\n\d{2}:\d{2}:\d{2},\d{3} --> \d{2}:\d{2}:\d{2},\d{3}\r?\n");
                    text = string.Join("\n", souceSrt);

                    //MessageBox.Show(text);

                    var bytes = Encoding.Default.GetBytes(text);
                    text = Encoding.UTF8.GetString(bytes);
                    //  MessageBox.Show(text);
                }

                //PDDocument doc = PDDocument.load(filename);
                //PDFTextStripper stripper = new PDFTextStripper("UTF-8");
                // MessageBox.Show(stripper.getText(doc));
                // string text = stripper.getText(doc);

                //MessageBox.Show(filename2);


                var CommandText = new SQLiteCommand("insert into  paper (title) values ('" + filename + "')", sql_con);
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
                    var dr = DT.Rows[0];
                    idp = Convert.ToInt32(dr[0].ToString());
                    // sql_con.Close();
                }
                else
                {
                    idp = -1;
                }

                var s = text.Replace(Environment.NewLine, " ");

                var sentences = SplitSentences(s);
                var nbs = sentences.Count();

                string s3;
                var i = 0;
                var l = 1;
                var pattern1 = l + ". ";
                var pattern2 = @"[\[1+\]|\[l+\]] ";
                var refe = false; // used to index references
                var refes = ""; // all references 
                var nbletters = 0; // if we counter 5 seccusive line without any reference we start new chapter
                var nbsentences = 0;


                foreach (var sentence in sentences)
                {
                    nbsentences++;
                    s3 = Form2.removes(sentence);
                    // MessageBox.Show("I'm here working/" + s3.Normalize() + "===>" + s3.Length.ToString());
                    i++;
                    setstate(i, nbs, pn);

                    var id = (int) Invoke((aaddphrase) AddPhrase, s3, idp); //AddPhrase(s3);
                    var tokens = TokenizeSentence(s3);

                    if (tokens.Count() >= 2) Invoke((iinsertword) InsertWords, tokens, id);

                    if (sentence.Contains("References") || sentence.Contains("REFERENCES"))
                    {
                        refe = true;
                        l = 1;
                    }


                    if ((Regex.IsMatch(sentence, pattern1) || Regex.IsMatch(sentence, pattern2)) && refe)
                    {
                        nbletters = 0;
                        l++;

                        pattern1 = l + ". ";
                        pattern2 = @"\[" + l + @"+\] ";
                    }
                    else if (refe)
                    {
                        nbletters += sentence.Length;
                    }


                    if ((nbletters >= 500 || nbsentences >= sentences.Length) && refe)
                    {
                        addrefs(refes, idp);

                        nbref++;
                        chapter++;
                        refes = "";

                        refe = false;
                        nbletters = 0;
                    }

                    if (refe) refes += sentence;
                }
            }
            catch (Exception ex)
            {
                error = true;
                DT2.Rows[pn][2] = "Error: " + ex.Message;
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
            var doc = PDDocument.load(input);
            var stripper = new PDFTextStripper();
            return stripper.getText(doc);
        }

        public string[] SplitSentences(string paragraph)
        {
            if (mSentenceDetector == null)
                mSentenceDetector = new EnglishMaximumEntropySentenceDetector(mModelPath + "EnglishSD.nbin");

            return mSentenceDetector.SentenceDetect(paragraph);
        }

        public string[] TokenizeSentence(string sentence)
        {
            if (mTokenizer == null) mTokenizer = new EnglishMaximumEntropyTokenizer(mModelPath + "EnglishTok.nbin");

            return mTokenizer.Tokenize(sentence);
        }
    }
}