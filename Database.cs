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
