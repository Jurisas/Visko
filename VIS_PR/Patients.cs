using System;
using System.Runtime.Serialization;
using MySql.Data.MySqlClient;

namespace VIS_PR
{
    [DataContract]
    public class Patients
    {
        [DataMember]
        public int PatientId { get; set; }
        [DataMember]
        public string Firstname { get; set; }
        [DataMember]
        public string Lastname { get; set; }
        [DataMember]
        public string Info { get; set; }
        [DataMember]
        public string DateBirth { get; set; }
        [DataMember]
        public Doctors Doctor { get; set; }

        public Patients(int patientId, string firstname, string lastname, string info, string dateBirth, Doctors doctor)
        {
            PatientId = patientId;
            Firstname = firstname;
            Lastname = lastname;
            Info = info;
            DateBirth = dateBirth;
            Doctor = doctor;
        }
        
        public Patients(string firstname, string lastname, string info, string dateBirth, Doctors doctor)
        {
            Firstname = firstname;
            Lastname = lastname;
            Info = info;
            DateBirth = dateBirth;
            Doctor = doctor;
        }

        public Patients()
        {

        }
        
    }
}