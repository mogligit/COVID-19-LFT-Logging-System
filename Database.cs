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
            List<Patient> patients = new List<Patient>();

            using (SqlDataReader r = NonQuery("SELECT PatientID, FirstName, Surname, DateOfBirth, GenderID, EthnicGroupID, NHSNumber, CountryID, Postcode, Address, CurrentlyInWork, AreaOfWorkID, OccupationID, Employer, EmailAddress, MobileNumber, PatientGroupID FROM Patient;")) {
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
        public static string GetGender(int id)
        {
            using SqlDataReader reader = NonQuery("SELECT String FROM Gender WHERE Gender.GenderID = @0;", id.ToString());
            if (reader.Read())
            {
                return reader.GetString(0);
            }
            throw new ArgumentOutOfRangeException("Gender ID " + id.ToString() + " was not found in the database.");
        }
        public static string GetEthnicGroup(int id)
        {
            using SqlDataReader reader = NonQuery("SELECT String FROM EthnicGroup WHERE EthnicGroup.EthnicGroupID = @0;", id.ToString());
            if (reader.Read())
            {
                return reader.GetString(0);
            }
            throw new ArgumentOutOfRangeException("EthnicGroup ID " + id.ToString() + " was not found in the database.");
        }
        public static string GetCountry(int id)
        {
            using SqlDataReader reader = NonQuery("SELECT String FROM Country WHERE Country.CountryID = @0;", id.ToString());
            if (reader.Read())
            {
                return reader.GetString(0);
            }
            throw new ArgumentOutOfRangeException("Country ID " + id.ToString() + " was not found in the database.");
        }
        public static string GetAreaOfWork(int id)
        {
            using SqlDataReader reader = NonQuery("SELECT String FROM AreaOfWork WHERE AreaOfWork.AreaOfWorkID = @0;", id.ToString());
            if (reader.Read())
            {
                return reader.GetString(0);
            }
            throw new ArgumentOutOfRangeException("AreaOfWork ID " + id.ToString() + " was not found in the database.");
        }
        public static string GetOccupation(int id)
        {
            using SqlDataReader reader = NonQuery("SELECT String FROM Occupation WHERE Occupation.OccupationID = @0;", id.ToString());
            if (reader.Read())
            {
                return reader.GetString(0);
            }
            throw new ArgumentOutOfRangeException("Occupation ID " + id.ToString() + " was not found in the database.");
        }
        public static string GetPatientGroup(int id)
        {
            using SqlDataReader reader = NonQuery("SELECT PatientGroupName FROM PatientGroup WHERE PatientGroup.PatientGroupID = @0;", id.ToString());
            if (reader.Read())
            {
                return reader.GetString(0);
            }
            throw new ArgumentOutOfRangeException("PatientGroup ID " + id.ToString() + " was not found in the database.");
        }
    }
}
