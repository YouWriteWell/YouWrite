using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.IO;


namespace YouWrite
{
    class Databases
    {
        private SQLiteConnection source; // model.db
        private SQLiteConnection source2; // categories.db


        private SQLiteConnection sql_con;

        public Databases(string appDir,string databasesDir)
        {

            // Creat first databases in appdata folder
            if (!Directory.Exists(databasesDir)) Directory.CreateDirectory(databasesDir);

            if (!File.Exists(Path.Combine(databasesDir, "categories.db")))
                File.Copy(Path.Combine(appDir, "categories.db"), Path.Combine(databasesDir, "categories.db"), true);

            if (!File.Exists(Path.Combine(databasesDir, "model.db")))
                File.Copy(Path.Combine(appDir, "model.db"), Path.Combine(databasesDir, "model.db"), true);

            source2 = new SQLiteConnection
                ("Data Source=" + Path.Combine(appDir, "categories.db") + ";Version=3;New=False;Compress=True;");

            source = new SQLiteConnection
                ("Data Source=" + Path.Combine(appDir, "model.db") + ";Version=3;New=False;Compress=True;");

        }

    




    }


}
