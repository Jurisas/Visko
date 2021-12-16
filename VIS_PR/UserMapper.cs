using System;
using MySql.Data.MySqlClient;

namespace VIS_PR
{
    public class UserMapper
    {
        public static User Find(string username, string password)
        {
            //MySqlConnection.ClearAllPools();
            User ret = new User();
            const string connectionString = @"server=bqn9gk2pnpqtwkxlzzud-mysql.services.clever-cloud.com;uid=ux8bviwfjodm5xus;password=YPpxmUi0yXZlQ68Kw1FB;database=bqn9gk2pnpqtwkxlzzud";
            var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                var command = new MySqlCommand("SELECT * FROM Users where username=@name and password=@password", connection);
                command.Parameters.Add(new MySqlParameter("@name", MySqlDbType.VarChar)).Value = username;
                command.Parameters.Add(new MySqlParameter("@password", MySqlDbType.VarChar)).Value = password;
                var result = command.ExecuteReader();
                while (result.Read())
                {
                    if (DBNull.Value.Equals(result["doctorId"]))
                    {
                        ret= new User
                        {
                            Id = Convert.ToInt32(result["userId"]),
                            Username = Convert.ToString(result["username"]),
                            Password = Convert.ToString(result["password"]),
                            IsAdmin = Convert.ToInt32(result["isAdmin"])

                        };
                    }
                    else
                    {
                        ret= new User
                        {
                            Id = Convert.ToInt32(result["userId"]),
                            Username = Convert.ToString(result["username"]),
                            Password = Convert.ToString(result["password"]),
                            IsAdmin = Convert.ToInt32(result["isAdmin"]),
                            Doctor = DoctorMapper.Find(Convert.ToInt32(result["doctorId"]))

                        };
                    }
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
                        "SELECT AUTO_INCREMENT FROM information_schema.TABLES WHERE TABLE_SCHEMA = 'bqn9gk2pnpqtwkxlzzud' AND TABLE_NAME = 'Users'",
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

        public static void Insert(User user)
        {
            const string connectionString =
                @"server=bqn9gk2pnpqtwkxlzzud-mysql.services.clever-cloud.com;uid=ux8bviwfjodm5xus;password=YPpxmUi0yXZlQ68Kw1FB;database=bqn9gk2pnpqtwkxlzzud";
            var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                var command =
                    new MySqlCommand(
                        "INSERT INTO Users(username, password, isAdmin, doctorId) values (@username,@password,@isAdmin,@id)",
                        connection);
                command.Parameters.Add(new MySqlParameter("@username", MySqlDbType.VarChar)).Value = user.Username;
                command.Parameters.Add(new MySqlParameter("@password", MySqlDbType.VarChar)).Value = user.Password;
                command.Parameters.Add(new MySqlParameter("@isAdmin", MySqlDbType.Int32)).Value = user.IsAdmin;
                command.Parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32)).Value = user.Doctor.DoctorId;
                var dataReader = command.ExecuteScalar();
                user.Id = (int)command.LastInsertedId;
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

        public static void Update(User user)
        {
            const string connectionString =
                @"server=bqn9gk2pnpqtwkxlzzud-mysql.services.clever-cloud.com;uid=ux8bviwfjodm5xus;password=YPpxmUi0yXZlQ68Kw1FB;database=bqn9gk2pnpqtwkxlzzud";
            var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                var command =
                    new MySqlCommand(
                        "UPDATE Users SET password=@password WHERE userId = @id",
                        connection);
                command.Parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32)).Value = user.Id;
                command.Parameters.Add(new MySqlParameter("@password", MySqlDbType.VarChar)).Value = user.Password;
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
    }
}