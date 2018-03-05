using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using sun.io;


namespace YouWrite
{
    class Databases
    {
        private SQLiteConnection source2; // categories.db
        private string _appDir;
        private string _databasesDir;
        private SQLiteConnection _sql_con;
        private SQLiteConnection _sql_con_file;
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

        }

        public void SetConnection(SQLiteConnection sourcep)
        {
            _sql_con_file = sourcep;
            _sql_con_file.Open();
            if (_sql_con != null) _sql_con.Close();

            connectionEstablished = true;
            
            _sql_con = new SQLiteConnection("Data Source=:memory:");
            _sql_con.Open();

            // copy db file to memory

            _sql_con_file.BackupDatabase(_sql_con, "main", "main", -1, null, 0);

            _sql_con_file.Close();
        }

        public void CloseConnection()
        {

            // save the  ram copy to file

            _sql_con_file.Open();

            // save memory db to file
            _sql_con.BackupDatabase(_sql_con_file, "main", "main", -1, null, 0);
            _sql_con_file.Close();
        }
        public DataTable getCategories()
        {
          

           SQLiteCommand sql_cmd = source2.CreateCommand();
           sql_cmd.CommandText = @"select id,name from category";
          
            DataTable DT2 = ExecuteSelect(sql_cmd);

          


           return DT2;

        }

        public DataTable ExecuteSelect(SQLiteCommand cmd)
        {
            bool close= false;
            if (cmd.Connection != null) {
                cmd.Connection.Open();
                close = true;
            }
            else
            {
                cmd.Connection = _sql_con;
            }

            
            var DB = new SQLiteDataAdapter(cmd);
            var DS = new DataSet();
            var DT = new DataTable();
            DB.Fill(DS);
            DT = DS.Tables[0];

            if (close)
            {
                cmd.Connection.Close();
            }

            return DT;
        }


        


        public void CloseAllConnections()
        {
          
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
            if (cmd.Connection == null)
            {  
                cmd.Connection = _sql_con;
                cmd.ExecuteNonQuery();
            }
            else
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();

            }
            
        }





        public int creatDB(string name, string description)
        {

           
            var CommandText = source2.CreateCommand();

            CommandText.CommandText="insert into  category (name,description) values ('" + name + "','" + description + "')";

            ExecuteQuery(CommandText);

            var sql_cmd = source2.CreateCommand();
            sql_cmd.CommandText = @"select MAX(id) as rowid from category";

            DataTable DT = ExecuteSelect(sql_cmd);
            var id = -1;
            if (DT.Rows.Count > 0)
            {
                var dr = DT.Rows[0];
                id = Convert.ToInt32(dr[0].ToString());
                string dbName = Path.Combine(Environment.GetFolderPath(
                        Environment.SpecialFolder.ApplicationData), "YouWrite", id + ".db");
                File.Copy(_databasesDir + @"\model.db", dbName, true);
            }
            return id; 
        }


    }


}
