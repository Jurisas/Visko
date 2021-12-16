using System;
using MySql.Data.MySqlClient;

namespace VIS_PR
{
    public class DepartmentMapper
    {
        
        public static Departments Find(int id)
        {
            Departments ret = null;
            const string connectionString = @"server=bqn9gk2pnpqtwkxlzzud-mysql.services.clever-cloud.com;uid=ux8bviwfjodm5xus;password=YPpxmUi0yXZlQ68Kw1FB;database=bqn9gk2pnpqtwkxlzzud";
            var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                var command = new MySqlCommand("SELECT * FROM Departments where departmentId=@id", connection);
                command.Parameters.Add(new MySqlParameter("@id", MySqlDbType.VarChar)).Value = id;
                var result = command.ExecuteReader();
                while (result.Read())
                {
                    ret = new Departments 
                    {
                        DepartmentId = Convert.ToInt32(result["departmentId"]), 
                        Name = Convert.ToString(result["name"])
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

        public static void Insert(Departments department)
        {
            const string connectionString =
                @"server=bqn9gk2pnpqtwkxlzzud-mysql.services.clever-cloud.com;uid=ux8bviwfjodm5xus;password=YPpxmUi0yXZlQ68Kw1FB;database=bqn9gk2pnpqtwkxlzzud";
            var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                var command =
                    new MySqlCommand(
                        "INSERT INTO Departments(name) values (@name)",
                        connection);
                command.Parameters.Add(new MySqlParameter("@name", MySqlDbType.VarChar)).Value = department.Name;
                var dataReader = command.ExecuteScalar();
                department.DepartmentId = (int)command.LastInsertedId;
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
        
        public static void Update(Departments department)
        {
            const string connectionString =
                @"server=bqn9gk2pnpqtwkxlzzud-mysql.services.clever-cloud.com;uid=ux8bviwfjodm5xus;password=YPpxmUi0yXZlQ68Kw1FB;database=bqn9gk2pnpqtwkxlzzud";
            var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                var command =
                    new MySqlCommand(
                        "UPDATE Departments SET name=@name WHERE departmentId=@id",
                        connection);
                command.Parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32)).Value = department.DepartmentId;
                command.Parameters.Add(new MySqlParameter("@name", MySqlDbType.VarChar)).Value = department.Name;
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
                    "SELECT departmentId,name FROM Departments where name = @info",
                    connection);
            command.Parameters.Add(new MySqlParameter("@info", MySqlDbType.VarChar)).Value = info;
            var result = command.ExecuteReader();
            while (result.Read())
            {
                Console.WriteLine("ID: " + result.GetValue(0) + " Name: " + result.GetValue(1));
            }
            connection.Close();

        }

        public static bool CanFind(int id)
        {
            var ret = false; 
            const string connectionString =
                @"server=bqn9gk2pnpqtwkxlzzud-mysql.services.clever-cloud.com;uid=ux8bviwfjodm5xus;password=YPpxmUi0yXZlQ68Kw1FB;database=bqn9gk2pnpqtwkxlzzud";
            var connection = new MySqlConnection(connectionString);
            try
            {
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
    }
}