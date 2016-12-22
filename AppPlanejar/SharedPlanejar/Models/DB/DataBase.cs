using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using SharedPlanejar.Models.DB;

namespace Models.DB
{
    public class DataBase
    {

        private static SqliteConnection CONNECTION;
        private static String DBNAME = "planejar2.db";
        private static int DATABASE_VERSION = 1;


        public DataBase()
        {

            StartConecton();

            CloseConecton();


        }

        public void CloseConecton()
        {
            try
            {
                CONNECTION.Close();
            }
            catch (Exception e) {
                
            }
        }

        private void StartConecton() {

            CloseConecton();
                        
            string dbPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                DBNAME);

            bool exists = File.Exists(dbPath);

            if (!exists)
            {
                Mono.Data.Sqlite.SqliteConnection.CreateFile(dbPath);
                CONNECTION = new SqliteConnection("Data Source=" + dbPath);
                CONNECTION.Open();
                CreateTables(CONNECTION);
               // c.Close();
            }
            else {
                // Open connection to existing database file
                CONNECTION = new SqliteConnection("Data Source=" + dbPath);
                CONNECTION.Open();
            }

            
        }

        public long ExecuteNoQuery(string comandQuery,SqliteParameter[] values)
        {
            int r;
            StartConecton();

            // query the database to prove data was inserted!
            using (var contents = CONNECTION.CreateCommand())
            {
                
              //  InsertQuery = "INSERT INTO TUSUARIOS ( NOME , EMAIL , SENHA , ATIVO , LOGADO ) VALUES ( 'a', 'a', 'a',1, 0)";
                contents.CommandText = comandQuery;
                contents.Parameters.AddRange(values);
                try
                {
                    r = contents.ExecuteNonQuery();
                }
                catch (Exception e) {
                    throw new Exception("Erro ao inserir "+e.Message);
                }
            }
   
            return r;

        }

        public long ExecuteNoQuery(string Query)
        {
            int r;

            StartConecton();

            // query the database to prove data was inserted!
            using (var contents = CONNECTION.CreateCommand())
            {
                contents.CommandText = Query;
                r = contents.ExecuteNonQuery();
            }
            
            return r;
        }

        public SqliteDataReader ExecuteQuery(string Query)
        {
            SqliteDataReader ret;

            StartConecton();

            // query the database to prove data was inserted!
            using (var contents = CONNECTION.CreateCommand())
            {
                contents.CommandText = Query;
                ret = contents.ExecuteReader();
            }
            
            return ret;
        }

        private void CreateTables(SqliteConnection dbConected)
        {
            var list = dbTables.scriptCreate(DATABASE_VERSION);

            foreach (String item in list) {

                using (var c = dbConected.CreateCommand())
                {
                    c.CommandText = item;
                    var rowcount = c.ExecuteNonQuery();

                }
            }
        }
    }
}  