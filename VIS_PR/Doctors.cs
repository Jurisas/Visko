using System.Xml.Serialization;

namespace VIS_PR
{
    [XmlRoot]
    public class Doctors
    {
        [XmlElement]
        public int DoctorId { get; set; }
        [XmlElement]
        public string Firstname { get; set; }
        [XmlElement]
        public string Lastname { get; set; }
        [XmlElement]
        public string DateBirth { get; set; }
        public Departments Department { get; set; }

        public Doctors(int doctorId, string firstname, string lastname, string dateBirth, Departments departments)
        {
            DoctorId = doctorId;
            Firstname = firstname;
            Lastname = lastname;
            DateBirth = dateBirth;
        }

        public Doctors()
        {
            
        }

    }
}