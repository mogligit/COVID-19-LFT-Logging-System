using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace COVID_19_LFT_Logging_System
{
    class Database
    {
        
        private const string DATABASE_PATH = "C:\\Users\\mogli\\source\\repos\\COVID-19 LFT Logging System\\TestDB.mdf";
        public static void Connect()
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"" + DATABASE_PATH + "\";Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
        }
    }
}
