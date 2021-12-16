using System;
using System.Xml.Serialization;
using MySql.Data.MySqlClient;

namespace VIS_PR
{
    [XmlRoot]
    public class Operations
    {
        [XmlElement]
        public int OperationId { get; set; }
        [XmlElement]
        public string Name { get; set; }
        [XmlElement]
        public string Info { get; set; }
        [XmlElement]
        public string Date { get; set; }
        public Patients Patient { get; set; }
        [XmlElement]
        public Doctors Doctor { get; set; }

        public Operations()
        {
            
        }

        public static int NextIncrement()
        {
            var ret = -1;
            const string connectionString =
                @"server=bqn9gk2pnpqtwkxlzzud-mysql.services.clever-cloud.com;uid=ux8bviwfjodm5xus;password=YPpxmUi0yXZlQ68Kw1FB;database=bqn9gk2pnpqtwkxlzzud";
            var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                var command =
                    new MySqlCommand(
                        "SELECT AUTO_INCREMENT FROM information_schema.TABLES WHERE TABLE_SCHEMA = 'bqn9gk2pnpqtwkxlzzud' AND TABLE_NAME = 'Operations'",
                        connection);

                var dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    ret= int.Parse(dataReader.GetValue(0).ToString() ?? string.Empty) + 1;
                }
            }
            catch (MySqlException exception)
            {
                Console.Write(exception);

            }
            finally
            {
                connection.Close();
            }

            return ret;
        }

        public static void Insert(Operations operation)
        {
            const string connectionString =
                @"server=bqn9gk2pnpqtwkxlzzud-mysql.services.clever-cloud.com;uid=ux8bviwfjodm5xus;password=YPpxmUi0yXZlQ68Kw1FB;database=bqn9gk2pnpqtwkxlzzud";
            var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                var command =
                    new MySqlCommand(
                        "INSERT INTO Operations(name,info,date,doctorId,pacientId) values (@name,@info,@date,@docId,@pac)",
                        connection);
                command.Parameters.Add(new MySqlParameter("@name", MySqlDbType.VarChar)).Value = operation.Name;
                command.Parameters.Add(new MySqlParameter("@info", MySqlDbType.VarChar)).Value = operation.Info;
                command.Parameters.Add(new MySqlParameter("@date", MySqlDbType.VarChar)).Value = operation.Date;
                command.Parameters.Add(new MySqlParameter("@pac", MySqlDbType.Text)).Value = operation.Patient.PatientId;
                command.Parameters.Add(new MySqlParameter("@docId", MySqlDbType.Text)).Value = operation.Doctor.DoctorId;

                command.ExecuteScalar();
                operation.OperationId = (int)command.LastInsertedId;
            }
            catch (MySqlException exception)
            {
                Console.Write(exception);

            }
            finally
            {
                connection.Close();
            }
        }
    }
}