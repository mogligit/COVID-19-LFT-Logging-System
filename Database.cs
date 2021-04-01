using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace COVID_19_LFT_Logging_System
{
    class Database
    {
        private static string DATABASE_NAME;
        private static string DATABASE_PATH;
        private static string DATABASE_LOG_PATH;
        private static SqlConnection conn;

        /// <summary>
        /// Attempts to connect to the database in the directory specified.
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="dbName"></param>
        /// <returns>Returns True if successful, False if not successful.</returns>
        public static bool TryConnect(string dbName)
        {
            string connectionString = String.Format("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={0};Integrated Security=True", dbName + ".mdf");
            conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                DATABASE_PATH = dbName + ".mdf";
                DATABASE_LOG_PATH = dbName + "_log.ldf";
                return true;
            }
            catch
            {
                return false;
            }
            
        }
        public static void CreateNewDatabase(string dbName)
        {
            string path = Directory.GetCurrentDirectory() + "\\" + dbName + ".mdf";
            string pathLog = Directory.GetCurrentDirectory() + "\\" + dbName + "_log.ldf";

            string query;
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);

            query = String.Format("CREATE DATABASE {0} ON PRIMARY (NAME = {0}, FILENAME = '{1}')" +
             "LOG ON (NAME = {0}_log, FILENAME = '{2}')", dbName, path, pathLog);

            SqlCommand command = new SqlCommand(query, connection);
   
            connection.Open();
            command.ExecuteNonQuery();

            connection.ChangeDatabase(dbName);

            // Build Script
            string buildString = BuildScript.DATABASE_BUILD_SCRIPT;
            command = new SqlCommand(buildString, connection);
            command.ExecuteNonQuery();
        }
        private static SqlDataReader Query(string query, params string[] parameters)
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
            List<Patient> patients = new List<Patient>();

            using (SqlDataReader r = Query("SELECT PatientID, FirstName, Surname, DateOfBirth, GenderID, EthnicGroupID, NHSNumber, CountryID, Postcode, Address, CurrentlyInWork, AreaOfWorkID, OccupationID, Employer, EmailAddress, MobileNumber, PatientGroupID FROM Patient;")) {
                while (r.Read())
                {
                    Patient newOne = new Patient(r.GetString(1), r.GetString(2), r.GetDateTime(3), r.GetInt32(4), r.GetInt32(5), r.GetInt32(7), r.GetString(8), r.GetString(9), r.GetBoolean(10), r.GetString(14));
                    newOne.Id = r.GetInt32(0);
                    newOne.PatientGroupId = r.GetInt32(16);

                    if (!r.IsDBNull(6)) { newOne.NHSNumber = r.GetString(6); }
                    if (!r.IsDBNull(11)) { newOne.AreaOfWorkId = r.GetInt32(11); }
                    if (!r.IsDBNull(12)) { newOne.OccupationId = r.GetInt32(12); }
                    if (!r.IsDBNull(13)) { newOne.Employer = r.GetString(13); }
                    if (!r.IsDBNull(15)) { newOne.MobileNumber = r.GetString(15); }

                    patients.Add(newOne);
                }
            }

            return patients;
        }
        public static List<string> GetGenderList()
        {
            List<string> values = new List<string>();

            using SqlDataReader reader = Query("SELECT GenderID, String FROM Gender ORDER BY GenderID ASC;");
            while (reader.Read())
            {
                // ID becomes index, String becomes value
                values.Insert(reader.GetInt32(0), reader.GetString(1));
            }

            return values;
        }
        public static List<string> GetEthnicGroupList()
        {
            List<string> values = new List<string>();

            using SqlDataReader reader = Query("SELECT EthnicGroupID, String FROM EthnicGroup ORDER BY EthnicGroupID ASC;");
            while (reader.Read())
            {
                // ID becomes index, String becomes value
                values.Insert(reader.GetInt32(0), reader.GetString(1));
            }

            return values;
        }
        public static List<string> GetCountryList()
        {
            List<string> values = new List<string>();

            using SqlDataReader reader = Query("SELECT CountryID, String FROM Country ORDER BY CountryID ASC;");
            while (reader.Read())
            {
                // ID becomes index, String becomes value
                values.Insert(reader.GetInt32(0), reader.GetString(1));
            }

            return values;
        }
        public static List<string> GetAreaOfWorkList()
        {
            List<string> values = new List<string>();

            using SqlDataReader reader = Query("SELECT AreaOfWorkID, String FROM AreaOfWork ORDER BY AreaOfWorkID ASC;");
            while (reader.Read())
            {
                // ID becomes index, String becomes value
                values.Insert(reader.GetInt32(0), reader.GetString(1));
            }

            return values;
        }
        public static List<string> GetOccupationList()
        {
            List<string> values = new List<string>();

            using SqlDataReader reader = Query("SELECT OccupationID, String FROM Occupation ORDER BY OccupationID ASC;");
            while (reader.Read())
            {
                // ID becomes index, String becomes value
                values.Insert(reader.GetInt32(0), reader.GetString(1));
            }

            return values;
        }
        public static List<string> GetPatientGroupList()
        {
            List<string> values = new List<string>();

            using SqlDataReader reader = Query("SELECT PatientGroupID, PatientGroupName FROM PatientGroup ORDER BY PatientGroupID ASC;");
            while (reader.Read())
            {
                // ID becomes index, String becomes value
                values.Insert(reader.GetInt32(0), reader.GetString(1));
            }

            return values;
        }



        public static string GetGenderFromId(int id)
        {
            return GetGenderList()[id];
        }
        public static string GetEthnicGroupFromId(int id)
        {
            return GetEthnicGroupList()[id];
        }
        public static string GetCountryFromId(int id)
        {
            return GetCountryList()[id];
        }
        public static string GetAreaOfWorkFromId(int id)
        {
            return GetAreaOfWorkList()[id];
        }
        public static string GetOccupationFromId(int id)
        {
            return GetOccupationList()[id];
        }
        public static string GetPatientGroupFromId(int id)
        {
            return GetPatientGroupList()[id];
        }
    }
}
