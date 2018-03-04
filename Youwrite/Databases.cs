using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.IO;
using sun.io;


namespace YouWrite
{
    class Databases
    {
        private SQLiteConnection source; // model.db
        private SQLiteConnection source2; // categories.db
        private string _appDir;
        private string _databasesDir;
        private SQLiteConnection _sql_con;
        public bool connectionEstablished = false; 

        public Databases(string appDir,string databasesDir)
        {

            this._appDir = appDir;
            this._databasesDir = databasesDir;

            // Creat first databases in appdata folder
            if (!Directory.Exists(databasesDir)) Directory.CreateDirectory(databasesDir);

            if (!File.Exists(Path.Combine(databasesDir, "categories.db")))
                File.Copy(Path.Combine(appDir, "categories.db"), Path.Combine(databasesDir, "categories.db"), true);

            if (!File.Exists(Path.Combine(databasesDir, "model.db")))
                File.Copy(Path.Combine(appDir, "model.db"), Path.Combine(databasesDir, "model.db"), true);

            source2 = new SQLiteConnection
                ("Data Source=" + Path.Combine(databasesDir, "categories.db") + ";Version=3;New=False;Compress=True;");

            source = new SQLiteConnection
                ("Data Source=" + Path.Combine(databasesDir, "model.db") + ";Version=3;New=False;Compress=True;");



        }

        public void SetConnection(SQLiteConnection sourcep)
        {
            source = sourcep;
            source.Open();
            if (_sql_con != null) _sql_con.Close();

            connectionEstablished = true;
            _sql_con = new SQLiteConnection("Data Source=:memory:");
            _sql_con.Open();

            // copy db file to memory

            source.BackupDatabase(_sql_con, "main", "main", -1, null, 0);

            source.Close();
        }

        public DataTable getCategories()
        {
           source2.Open();

           SQLiteCommand sql_cmd = source2.CreateCommand();
           sql_cmd.CommandText = @"select id,name from category";
           SQLiteDataAdapter DB = new SQLiteDataAdapter(sql_cmd);
           DataSet DS=new DataSet();
           DB.Fill(DS);
           DataTable DT2 = DS.Tables[0];

           source2.Close();


           return DT2;

        }

        public DataTable ExecuteSelect(SQLiteCommand cmd)
        {
            cmd.Connection = _sql_con;
            var DB = new SQLiteDataAdapter(cmd);
            var DS = new DataSet();
            var DT = new DataTable();
            DB.Fill(DS);
            DT = DS.Tables[0];

            return DT;
        }


        public void CloseConnection()
        {
            source.Open();

            // save memory db to file
            _sql_con.BackupDatabase(source, "main", "main", -1, null, 0);
            source.Close();
        }


        public void CloseAllConnections()
        {
            source.Close();
            source2.Close();
            if (_sql_con != null)
            {
                _sql_con.Close();

            }
        }


        public void ExecuteQuery(string txtQuery)
        {
            var sql_cmd = _sql_con.CreateCommand();
            sql_cmd.CommandText = txtQuery;
            sql_cmd.ExecuteNonQuery();
        }

        public void ExecuteQuery(SQLiteCommand cmd)
        {
            cmd.Connection = _sql_con;
            cmd.ExecuteNonQuery();
        }

        public void createdb(string name, string description)
        {
            var CommandText =
                new SQLiteCommand(
                    "insert into  category (name,description) values ('" + name + "','" + description + "')", _sql_con);
            CommandText.ExecuteNonQuery();
            var sql_cmd = _sql_con.CreateCommand();
            sql_cmd.CommandText = @"select last_insert_rowid() as rowid";
            var DB = new SQLiteDataAdapter(sql_cmd);
            var DS = new DataSet();
            DB.Fill(DS);
            DataTable DT = DS.Tables[0];

            if (DT.Rows.Count > 0)
            {
                var dr = DT.Rows[0];
                var id = Convert.ToInt32(dr[0].ToString());
                // sql_con.Close();
                var dbName = Path.Combine(Environment.GetFolderPath(
                        Environment.SpecialFolder.ApplicationData), "YouWrite", id + ".db");

                File.Copy(_databasesDir + @"\model.db", dbName, true);
            }
        }


    }


}
