using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Text.RegularExpressions;


namespace YouWrite
{
    class RefsExtractor
    {
        private Databases _database;
        public RefsExtractor(Databases database)
        {
            _database = database;
        }

        

        public void addrefs(string refes, int idp,int chapter)
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
                        addref(idp, refes.Substring(pos1, pos2 - pos1), k,chapter);
                    }
                    else if (pos1 >= 1)
                    {
                        addref(idp, refes.Substring(pos1), k, chapter);
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
                        addref(idp, refes.Substring(pos1, pos2 - pos1), k, chapter);
                    }
                    else if (pos1 >= 1)
                    {
                        addref(idp, refes.Substring(pos1), k, chapter);
                        cont = false;
                    }
                    else
                    {
                        cont = false;
                    }

                    k++;
                }
        }
        private void addref(int idp, string reft, int refn,int chapter)
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

    }
}
