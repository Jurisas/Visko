using System;
using MySql.Data.MySqlClient;

namespace VIS_PR
{
    public class PatientMapper
    {
        public static Patients Find(int id)
        {
            Patients ret = null;
            const string connectionString = @"server=bqn9gk2pnpqtwkxlzzud-mysql.services.clever-cloud.com;uid=ux8bviwfjodm5xus;password=YPpxmUi0yXZlQ68Kw1FB;database=bqn9gk2pnpqtwkxlzzud";
            var connection = new MySqlConnection(connectionString);
            try{
                connection.Open();
                var command =
                    new MySqlCommand("SELECT * FROM Patients where patientId = @id", connection);
                command.Parameters.Add(new MySqlParameter("@id", MySqlDbType.VarChar)).Value = id;
                var result = command.ExecuteReader();
                while (result.Read()) 
                {
                    ret = new Patients 
                    {
                        PatientId = Convert.ToInt32(result["patientId"]),
                        Firstname = Convert.ToString(result["firstname"]),
                        Lastname = Convert.ToString(result["lastname"]),
                        Info = Convert.ToString(result["info"]),
                        DateBirth = Convert.ToDateTime(result["dateBirth"]).ToString("yyyy-MM-dd"),
                        Doctor = DoctorMapper.Find(Convert.ToInt32(result["practical"]))
                    };
                }
            }
            catch (MySqlException exception) {
                Console.Write(exception);
            }
            finally {
                connection.Close();
            }
            return ret;
        }
        
        public static void Insert(Patients patient)
        {
            const string connectionString =
                @"server=bqn9gk2pnpqtwkxlzzud-mysql.services.clever-cloud.com;uid=ux8bviwfjodm5xus;password=YPpxmUi0yXZlQ68Kw1FB;database=bqn9gk2pnpqtwkxlzzud";
            var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                var command =
                    new MySqlCommand(
                        "INSERT INTO Patients(firstname,lastname,dateBirth,info,practical) values (@firstname,@lastname,@dateBirth,@info,@docId)",
                        connection);
                command.Parameters.Add(new MySqlParameter("@firstname", MySqlDbType.VarChar)).Value = patient.Firstname;
                command.Parameters.Add(new MySqlParameter("@lastname", MySqlDbType.VarChar)).Value = patient.Lastname;
                command.Parameters.Add(new MySqlParameter("@dateBirth", MySqlDbType.VarChar)).Value = patient.DateBirth;
                command.Parameters.Add(new MySqlParameter("@info", MySqlDbType.Text)).Value = patient.Info;
                command.Parameters.Add(new MySqlParameter("@docId", MySqlDbType.Text)).Value = patient.Doctor.DoctorId;

                command.ExecuteScalar();
                patient.PatientId = (int)command.LastInsertedId;
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

        
        public static void Update(Patients patient)
        {
            const string connectionString =
                @"server=bqn9gk2pnpqtwkxlzzud-mysql.services.clever-cloud.com;uid=ux8bviwfjodm5xus;password=YPpxmUi0yXZlQ68Kw1FB;database=bqn9gk2pnpqtwkxlzzud";
            var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                var command =
                    new MySqlCommand(
                        "UPDATE Patients SET firstname=@firstname, lastname=@lastname, dateBirth=@dateBirth, info=@info WHERE patientId = @id",
                        connection);
                command.Parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32)).Value = patient.PatientId;
                command.Parameters.Add(new MySqlParameter("@firstname", MySqlDbType.VarChar)).Value = patient.Firstname;
                command.Parameters.Add(new MySqlParameter("@lastname", MySqlDbType.VarChar)).Value = patient.Lastname;
                command.Parameters.Add(new MySqlParameter("@dateBirth", MySqlDbType.VarChar)).Value = patient.DateBirth;
                command.Parameters.Add(new MySqlParameter("@info", MySqlDbType.Text)).Value = patient.Info;
                command.Parameters.Add(new MySqlParameter("@docId", MySqlDbType.Text)).Value = patient.Doctor.DoctorId;
                command.ExecuteScalar();
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

        public static bool CanFind(int id)
        {
            var ret = false;
            const string connectionString =
                @"server=bqn9gk2pnpqtwkxlzzud-mysql.services.clever-cloud.com;uid=ux8bviwfjodm5xus;password=YPpxmUi0yXZlQ68Kw1FB;database=bqn9gk2pnpqtwkxlzzud";
            var connection = new MySqlConnection(connectionString);
            try{
                connection.Open();
                var command =
                    new MySqlCommand(
                        "SELECT * FROM Patients where patientId = @id",
                        connection);
                command.Parameters.Add(new MySqlParameter("@id", MySqlDbType.VarChar)).Value = id;
                var result = command.ExecuteReader();
                if (result.HasRows)
                {
                    ret= true;
                }
            }
            catch (MySqlException exception) {
                Console.Write(exception);
            }
            finally
            {
                connection.Close();
            }

            return ret;
        }


        public static int NextIncrement()
        {
            int ret = -1;
            const string connectionString =
                @"server=bqn9gk2pnpqtwkxlzzud-mysql.services.clever-cloud.com;uid=ux8bviwfjodm5xus;password=YPpxmUi0yXZlQ68Kw1FB;database=bqn9gk2pnpqtwkxlzzud";
            var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                var command =
                    new MySqlCommand(
                        "SELECT AUTO_INCREMENT FROM information_schema.TABLES WHERE TABLE_SCHEMA = 'bqn9gk2pnpqtwkxlzzud' AND TABLE_NAME = 'Patients'",
                        connection);

                var dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    ret = int.Parse(dataReader.GetValue(0).ToString() ?? string.Empty) + 1;
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
        
        public static void FindByName(string info)
        {
            const string connectionString =
                @"server=bqn9gk2pnpqtwkxlzzud-mysql.services.clever-cloud.com;uid=ux8bviwfjodm5xus;password=YPpxmUi0yXZlQ68Kw1FB;database=bqn9gk2pnpqtwkxlzzud";
            var connection = new MySqlConnection(connectionString);
            connection.Open();
            var command =
                new MySqlCommand(
                    "SELECT firstname,lastname,dateBirth,info FROM Patients where lastname = @info",
                    connection);
            command.Parameters.Add(new MySqlParameter("@info", MySqlDbType.VarChar)).Value = info;
            var result = command.ExecuteReader();
            while (result.Read())
            {
                Console.WriteLine("ID: " + result.GetValue(0) + " Name: " + result.GetValue(1) + " " +
                                  result.GetValue(2)  + " Date: " + result.GetValue(3) + " Info:" + result.GetValue(4)); 
            }
            
            connection.Close();

        }
    }
}