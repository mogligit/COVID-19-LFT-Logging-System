using System;

namespace COVID_19_LFT_Logging_System
{
    class Patient
    {
        private int id; // nullable (automatic)
        private string firstName;
        private string surname;
        private DateTime dob;
        private int genderId;
        private int ethnicGroupId;
        private string nhsNumber; // nullable
        private int countryId;
        private string postcode;
        private string address;
        private bool currentlyInWork;
        private int areaOfWorkId; // nullable
        private int occupationId; // nullable
        private string employer; // nullable
        private string emailAddress;
        private string mobileNumber; // nullable
        private int patientGroupId; // nullable (automatic)

        // Constructor
        // has parameters for all the fields that MUST be in the database
        public Patient(string firstName, string surname, DateTime dob, int genderId, int ethnicGroupId, int countryId,
            string postcode, string address, bool currentlyInWork, string emailAddress)
        {
            this.firstName = firstName;
            this.surname = surname;
            this.dob = dob;
            this.genderId = genderId;
            this.ethnicGroupId = ethnicGroupId;
            this.countryId = countryId;
            this.postcode = postcode;
            this.address = address;
            this.currentlyInWork = currentlyInWork;
            this.emailAddress = emailAddress;

        }

        public string FirstName { get => firstName; set => firstName = value; }
        public string Surname { get => surname; set => surname = value; }
        public DateTime Dob { get => dob; set => dob = value; }
        public int GenderId { get => genderId; set => genderId = value; }
        public int EthnicGroup { get => ethnicGroupId; set => ethnicGroupId = value; }
        public string NHSNumber { get => nhsNumber; set => nhsNumber = value; }
        public int CountryId { get => countryId; set => countryId = value; }
        public string Postcode { get => postcode; set => postcode = value; }
        public string Address { get => address; set => address = value; }
        public bool CurrentlyInWork { get => currentlyInWork; set => currentlyInWork = value; }
        public int AreaOfWorkId { get => areaOfWorkId; set => areaOfWorkId = value; }
        public int OccupationId { get => occupationId; set => occupationId = value; }
        public string Employer { get => employer; set => employer = value; }
        public string EmailAddress { get => emailAddress; set => emailAddress = value; }
        public string MobileNumber { get => mobileNumber; set => mobileNumber = value; }
        public int PatientGroupId { get => patientGroupId; set => patientGroupId = value; }

        public string GetGender() => Database.GetGender(this.genderId);
        public string GetEthnicGroup() => Database.GetEthnicGroup(this.ethnicGroupId);
        public string GetCountry() => Database.GetCountry(this.countryId);
        public string GetAreaOfWork() => Database.GetAreaOfWork(this.areaOfWorkId);
        public string GetOccupation() => Database.GetOccupation(this.occupationId);
        public string GetPatientGroup() => Database.GetPatientGroup(this.patientGroupId);

    }
}
