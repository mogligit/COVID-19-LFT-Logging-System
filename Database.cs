using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace COVID_19_LFT_Logging_System
{
    class Database
    {
        
        private const string DATABASE_PATH = "C:\\Users\\mogli\\source\\repos\\COVID-19 LFT Logging System\\TestDB.mdf";
        private static SqlConnection conn;

        public static void Connect()
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"" + DATABASE_PATH + "\";Integrated Security=True";
            conn = new SqlConnection(connectionString);
            conn.Open();
        }
        private static SqlDataReader NonQuery(string query, params string[] parameters)
        {
            SqlCommand command = conn.CreateCommand();
            command.CommandText = query;

            for (int i = 0; i < parameters.Length; i++)
            {
                command.Parameters.AddWithValue("@" + i.ToString(), parameters[i]);
            }
            
            return command.ExecuteReader();
        }
        public static List<Patient> GetPatientList()
        {
            using (SqlDataReader reader = NonQuery("SELECT PatientID, GroupID, FirstName, Surname, DateOfBirth FROM Patient;")) {
                while (reader.Read())
                {
                    String.Format("");
                }
            }

            return null;
        }
        public static string GetGender(int id)
        {
            using SqlDataReader reader = NonQuery("SELECT String FROM Gender WHERE Gender.GenderID = @1;", id.ToString());
            if (reader.Read())
            {
                return reader.GetString(0);
            }
            throw new ArgumentOutOfRangeException("Gender ID " + id.ToString() + " was not found in the database.");
        }
        public static string GetEthnicGroup(int id)
        {
            using SqlDataReader reader = NonQuery("SELECT String FROM EthnicGroup WHERE EthnicGroup.EthnicGroupID = @1;", id.ToString());
            if (reader.Read())
            {
                return reader.GetString(0);
            }
            throw new ArgumentOutOfRangeException("EthnicGroup ID " + id.ToString() + " was not found in the database.");
        }
        public static string GetCountry(int id)
        {
            using SqlDataReader reader = NonQuery("SELECT String FROM Country WHERE Country.CountryID = @1;", id.ToString());
            if (reader.Read())
            {
                return reader.GetString(0);
            }
            throw new ArgumentOutOfRangeException("Country ID " + id.ToString() + " was not found in the database.");
        }
        public static string GetAreaOfWork(int id)
        {
            using SqlDataReader reader = NonQuery("SELECT String FROM AreaOfWork WHERE AreaOfWork.AreaOfWorkID = @1;", id.ToString());
            if (reader.Read())
            {
                return reader.GetString(0);
            }
            throw new ArgumentOutOfRangeException("AreaOfWork ID " + id.ToString() + " was not found in the database.");
        }
        public static string GetOccupation(int id)
        {
            using SqlDataReader reader = NonQuery("SELECT String FROM Occupation WHERE Occupation.OccupationID = @1;", id.ToString());
            if (reader.Read())
            {
                return reader.GetString(0);
            }
            throw new ArgumentOutOfRangeException("Occupation ID " + id.ToString() + " was not found in the database.");
        }
        public static string GetPatientGroup(int id)
        {
            using SqlDataReader reader = NonQuery("SELECT PatientGroupName FROM PatientGroup WHERE PatientGroup.PatientGroupID = @1;", id.ToString());
            if (reader.Read())
            {
                return reader.GetString(0);
            }
            throw new ArgumentOutOfRangeException("PatientGroup ID " + id.ToString() + " was not found in the database.");
        }
    }
}
