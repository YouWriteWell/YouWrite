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

namespace YouWrite
{
    internal class  Import
    {
        public delegate int aaddphrase(string valeur, int idp);

        public delegate void iinsertword(string[] phrase, int id);

        private string _appDir;
        private string _databasesDir;
        private Databases _database;
        private int _categoryID;

        private readonly string cat;
        private int chapter;

        private readonly string mModelPath;
        private MaximumEntropySentenceDetector mSentenceDetector;
        private EnglishMaximumEntropyTokenizer mTokenizer;
        private int nbcorr = 0;

        private int pp;
        private DialogResult result;
        private readonly SQLiteConnection source;
        private SQLiteCommand sql_cmd;

        private SQLiteConnection sql_con;

        public delegate void CallbackEventHandler(int curr, int max, int papern, string msg);
        public event CallbackEventHandler setstate;
       
        public Import(int id)
        {
            _databasesDir = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData), "YouWrite");
            _appDir = Directory.GetCurrentDirectory();
            _categoryID = id;

            var dbName = Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.ApplicationData), "YouWrite", _categoryID + ".db");

            source = new SQLiteConnection
                ("Data Source=" + dbName + ";Version=3;New=False;Compress=True;");
            _database= new Databases(_appDir, _databasesDir);
            _database.SetConnection(source);

            mModelPath = _appDir + @"\";
        }

     

        // To chuck if this b-gram existes 
        private int seqexist2gram(string word1, string word2)
        {

            var sql_cmd= new SQLiteCommand();
            sql_cmd.CommandText = "select id from bgram where word1=@word1 and word2 =@word2";
            sql_cmd.Parameters.AddRange(new[]
            {
                new SQLiteParameter("@word1", word1),
                new SQLiteParameter("@word2", word2)
            });
     
            var DT=_database.ExecuteSelect(sql_cmd);

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

        // To check if this 3-gram existes 
        private int seqexist3gram(string word1, string word2, string word3)
        {
            
            var sql_cmd= new SQLiteCommand();
            sql_cmd.CommandText = "select id from tgram where word1=@word1 and word2 =@word2 and word3 = @word3";
            sql_cmd.Parameters.AddRange(new[]
            {
                new SQLiteParameter("@word1", word1),
                new SQLiteParameter("@word2", word2),
                new SQLiteParameter("@word3", word3)
            });
           
            var DT=_database.ExecuteSelect(sql_cmd);

            if (DT.Rows.Count > 0)
            {
                var dr = DT.Rows[0];
                var id = Convert.ToInt32(dr[0].ToString());
                return id;
            }
            return -1;
        }


        private int Add(string word1, string word2)
        {
            var CommandText =
                new SQLiteCommand("insert into  bgram (word1,word2,freq) values (@word1,@word2,0)", sql_con);
            CommandText.Parameters.AddRange(new[]
            {
                new SQLiteParameter("@word1", word1),
                new SQLiteParameter("@word2", word2)
            });

            _database.ExecuteQuery(CommandText);
            var sql_cmd= new SQLiteCommand();
            sql_cmd.CommandText = @"select last_insert_rowid() as rowid";
            var DT=_database.ExecuteSelect(sql_cmd);

            if (DT.Rows.Count > 0)
            {
                var dr = DT.Rows[0];
                var id = Convert.ToInt32(dr[0].ToString());
                return id;
            }
            return -1;
        }

        //add 3-gram
        private int Add(string word1, string word2, string word3)
        {

            var CommandText =
                new SQLiteCommand("insert into  tgram (word1,word2,word3,freq) values (@word1,@word2,@word3,0)",
                    sql_con);
            CommandText.Parameters.AddRange(new[]
            {
                new SQLiteParameter("@word1", word1),
                new SQLiteParameter("@word2", word2),
                new SQLiteParameter("@word3", word3)
            });

            _database.ExecuteQuery(CommandText);

            var sql_cmd= new SQLiteCommand();
            sql_cmd.CommandText = @"select last_insert_rowid() as rowid";
           
            var DT=_database.ExecuteSelect(sql_cmd);


            var dr = DT.Rows[0];
            var id = Convert.ToInt32(dr[0].ToString());
            
            return id;
        }

        private void Update2gram(int id)
        {
            var txtSQLQuery = "update bgram set freq =(freq+1) where id = " + id;
            _database.ExecuteQuery(txtSQLQuery);
        }

        private void Update3gram(int id)
        {
            var txtSQLQuery = "update tgram set freq =(freq+1) where id = " + id;
            _database.ExecuteQuery(txtSQLQuery);
        }

        //add phrase
        private int AddPhrase(string phrase, int idp)
        {
           
            var CommandText =
                new SQLiteCommand("insert into  phrase (phrase,idp,cha) values (@phrase,@idp,@cha)", sql_con);
            CommandText.Parameters.AddRange(new[]
            {
                new SQLiteParameter("@phrase", phrase),
                new SQLiteParameter("@idp", idp),
                new SQLiteParameter("@cha", chapter)
            });
            try
            {
                CommandText.ExecuteNonQuery();
                var sql_cmd= new SQLiteCommand();
                sql_cmd.CommandText = @"select last_insert_rowid() as rowid";
                var DT=_database.ExecuteSelect(sql_cmd);

                if (DT.Rows.Count > 0)
                {
                    var dr = DT.Rows[0];
                    var id = Convert.ToInt32(dr[0].ToString());
                    return id;
                }
            }
            catch
            {
            }
            return -1;
        }

        //add phrasegramrelation
        private void AddPhraseGram(int idp, int idg, int type)
        {
            var CommandText =
                new SQLiteCommand("insert into  grampaper (idp,idg,type) values (" + idp + "," + idg + "," + type + ")",
                    sql_con);
            _database.ExecuteQuery(CommandText);
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


        private void addref(int idp, string reft, int refn)
        {

            var cmd = new SQLiteCommand("insert into  ref (idp,reft,refn,cha) values (@idp,@reft,@refn,@cha)");
            cmd.Parameters.AddRange(new[]
            {
                new SQLiteParameter("@idp", idp),
                new SQLiteParameter("@reft", reft),
                new SQLiteParameter("@refn", refn),
                new SQLiteParameter("@cha", chapter)
            });

           _database.ExecuteQuery(cmd);
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

        public static string removes(string s)
        {
            string[] chars = { "/", "!", "@", "#", "$", "%", "^", "&", "*", "%", "\"", "|" };
            if (s.Contains("'")) s = s.Replace("'", "''");

            for (var i = 0; i < chars.Length; i++)
                if (s.Contains(chars[i]))
                    s = s.Replace(chars[i], " ");
            return s;
        }

        public void importPDF(int pn,string filename)
        {
           
            var error = false;
            var nberr = 0;
            chapter = 1;
         //   try
          //  {
                var nbref = 0;
                var doc = PDDocument.load(filename);
                var stripper = new PDFTextStripper("UTF-8");
                var text = stripper.getText(doc);

                //insert paper information
                var Command = new SQLiteCommand("insert into  paper (title) values ('" + filename + "')", sql_con);
                _database.ExecuteQuery(Command);
                var sql_cmd = new SQLiteCommand();

                // Get the id of the paper
                sql_cmd.CommandText = @"select last_insert_rowid() as rowid";
                var DT = _database.ExecuteSelect(sql_cmd);
                int idp;

                if (DT.Rows.Count > 0)
                {
                    var dr = DT.Rows[0];
                    idp = Convert.ToInt32(dr[0].ToString());
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
                    s3 = removes(sentence);
                    i++;
                    setstate(i, nbs, pn,"");
                    var id =  AddPhrase(s3, idp);
                    var tokens = TokenizeSentence(s3);

                    if (tokens.Count() >= 2) InsertWords(tokens, id);

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
         /*   }
            catch (Exception ex)
            {
                error = true;
                setstate(-1,-1, pn, "Error: " + ex.Message);
                nberr++;
            }*/
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
