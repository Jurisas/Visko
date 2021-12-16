using System;
using MySql.Data.MySqlClient;

namespace VIS_PR
{
    public class DoctorMapper
    {
        
        public static Doctors Find(int id)
        {
            Doctors ret = null;  
            const string connectionString =
                @"server=bqn9gk2pnpqtwkxlzzud-mysql.services.clever-cloud.com;uid=ux8bviwfjodm5xus;password=YPpxmUi0yXZlQ68Kw1FB;database=bqn9gk2pnpqtwkxlzzud";
            var connection = new MySqlConnection(connectionString);
            try{
                connection.Open();
                var command =
                    new MySqlCommand(
                        "SELECT * FROM Doctors where doctorId = @id",
                        connection);
                command.Parameters.Add(new MySqlParameter("@id", MySqlDbType.VarChar)).Value = id;
                var result = command.ExecuteReader();
                
                while(result.Read())
                {
                    ret = new Doctors
                    {
                        DoctorId = Convert.ToInt32(result["doctorId"]),
                        Firstname = Convert.ToString(result["firstname"]),
                        Lastname = Convert.ToString(result["lastname"]),
                        DateBirth = Convert.ToDateTime(result["dateBirth"]).ToString("yyyy-MM-dd"),
                        Department = DepartmentMapper.Find(Convert.ToInt32(result["departmentId"]))
                    };
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
            var ret = -1;
            const string connectionString =
                @"server=bqn9gk2pnpqtwkxlzzud-mysql.services.clever-cloud.com;uid=ux8bviwfjodm5xus;password=YPpxmUi0yXZlQ68Kw1FB;database=bqn9gk2pnpqtwkxlzzud";
            var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                var command =
                    new MySqlCommand(
                        "SELECT AUTO_INCREMENT FROM information_schema.TABLES WHERE TABLE_SCHEMA = 'bqn9gk2pnpqtwkxlzzud' AND TABLE_NAME = 'Doctors'",
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

        public static void Insert(Doctors doctor)
        {
            const string connectionString =
                @"server=bqn9gk2pnpqtwkxlzzud-mysql.services.clever-cloud.com;uid=ux8bviwfjodm5xus;password=YPpxmUi0yXZlQ68Kw1FB;database=bqn9gk2pnpqtwkxlzzud";
            var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                var command =
                    new MySqlCommand(
                        "INSERT INTO Doctors(firstname,lastname,dateBirth,departmentId) values (@firstname,@lastname,@dateBirth,@depId)",
                        connection);
                command.Parameters.Add(new MySqlParameter("@firstname", MySqlDbType.VarChar)).Value = doctor.Firstname;
                command.Parameters.Add(new MySqlParameter("@lastname", MySqlDbType.VarChar)).Value = doctor.Lastname;
                command.Parameters.Add(new MySqlParameter("@dateBirth", MySqlDbType.VarChar)).Value = doctor.DateBirth;
                command.Parameters.Add(new MySqlParameter("@depId", MySqlDbType.Text)).Value = doctor.Department.DepartmentId;

                var dataReader = command.ExecuteScalar();
                doctor.DoctorId = (int)command.LastInsertedId;
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

        public static void Update(Doctors doctor)
        {
            const string connectionString =
                @"server=bqn9gk2pnpqtwkxlzzud-mysql.services.clever-cloud.com;uid=ux8bviwfjodm5xus;password=YPpxmUi0yXZlQ68Kw1FB;database=bqn9gk2pnpqtwkxlzzud";
            var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                var command =
                    new MySqlCommand(
                        "UPDATE Doctors SET firstname=@firstname, lastname=@lastname, dateBirth=@dateBirth, departmentId=@depId WHERE doctorId = @id",
                        connection);
                command.Parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32)).Value = doctor.DoctorId;
                command.Parameters.Add(new MySqlParameter("@firstname", MySqlDbType.VarChar)).Value = doctor.Firstname;
                command.Parameters.Add(new MySqlParameter("@lastname", MySqlDbType.VarChar)).Value = doctor.Lastname;
                command.Parameters.Add(new MySqlParameter("@dateBirth", MySqlDbType.VarChar)).Value = doctor.DateBirth;
                command.Parameters.Add(new MySqlParameter("@depId", MySqlDbType.Text)).Value = doctor.Department.DepartmentId;
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
                        "SELECT * FROM Doctors where doctorId = @id",
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
        public static void FindByName(string info)
        {
            const string connectionString =
                @"server=bqn9gk2pnpqtwkxlzzud-mysql.services.clever-cloud.com;uid=ux8bviwfjodm5xus;password=YPpxmUi0yXZlQ68Kw1FB;database=bqn9gk2pnpqtwkxlzzud";
            var connection = new MySqlConnection(connectionString);
            connection.Open();
            var command =
                new MySqlCommand(
                    "SELECT doctorId,firstname,lastname,dateBirth,(SELECT D.name FROM Departments D where Doctors.departmentId=D.departmentId) FROM Doctors where lastname = @info",
                    connection);
            command.Parameters.Add(new MySqlParameter("@info", MySqlDbType.VarChar)).Value = info;
            var result = command.ExecuteReader();
            while (result.Read())
            {
                Console.WriteLine("ID: " + result.GetValue(0) + " Firstname: " + result.GetValue(1) + " " +
                                  result.GetValue(2)  + " Date: " + result.GetValue(3) + " Department: " + result.GetValue(4)); 
            }
            
            connection.Close();

        }
    }
}