using System;
using System.Data;
using System.Data.SqlClient;

namespace MusicLibraryWebApp.Models {
    public class DBAccess {
        private static String ConnectionString = "Data Source=CYBER;User ID=sa;Password=123; Initial Catalog=MusicDB;Integrated Security=True";
        public SqlConnection conn = new SqlConnection(ConnectionString);
        public SqlCommand cmd;
        public SqlDataReader sdr;


        public void ConOpen() {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
        }

        public void ConClose() {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }

        public void IUD(string query) {
            cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
        }



        //public 
        public SqlDataReader GetData(string query) {
            cmd = new SqlCommand(query, conn);
            sdr = cmd.ExecuteReader();
            return sdr;
        }



    }
}