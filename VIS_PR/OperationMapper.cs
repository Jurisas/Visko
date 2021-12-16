using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using MySql.Data.MySqlClient;

namespace VIS_PR
{
    public static class OperationMapper
    {

        public static void ShowOperationsByPatient(Patients patient)
        {
            const string connectionString =
                @"server=bqn9gk2pnpqtwkxlzzud-mysql.services.clever-cloud.com;uid=ux8bviwfjodm5xus;password=YPpxmUi0yXZlQ68Kw1FB;database=bqn9gk2pnpqtwkxlzzud";
            var connection = new MySqlConnection(connectionString);
            try{
                connection.Open();
                var command =
                    new MySqlCommand(
                        "SELECT * FROM Operations where pacientId = @id",
                        connection);
                command.Parameters.Add(new MySqlParameter("@id", MySqlDbType.VarChar)).Value = patient.PatientId;
                var result = command.ExecuteReader();
                while (result.Read())
                {
                    Console.WriteLine("ID: " + result.GetValue(0) + " Name: " + result.GetValue(1) + " info: " +
                                      result.GetValue(2) + " date: " + result.GetValue(3)); 

                }
            }
            catch (MySqlException exception) {
                Console.Write(exception);
            }
            finally
            {
                connection.Close();
            }

        }
        public static List<Operations> GetOperationsByPatient(Patients patient)
        {
            var resultList = new List<Operations>();
            const string connectionString =
                @"server=bqn9gk2pnpqtwkxlzzud-mysql.services.clever-cloud.com;uid=ux8bviwfjodm5xus;password=YPpxmUi0yXZlQ68Kw1FB;database=bqn9gk2pnpqtwkxlzzud";
            var connection = new MySqlConnection(connectionString);
            try{
                connection.Open();
                var command =
                    new MySqlCommand(
                        "SELECT * FROM Operations where pacientId = @id",
                        connection);
                command.Parameters.Add(new MySqlParameter("@id", MySqlDbType.VarChar)).Value = patient.PatientId;
                var result = command.ExecuteReader();
                while (result.Read())
                {
                    resultList.Add(new Operations()
                    {
                        Patient = PatientMapper.Find(Convert.ToInt32(result["pacientId"])),
                        Name = Convert.ToString(result["name"]),
                        Info = Convert.ToString(result["info"]),
                        Date = Convert.ToDateTime(result["date"]).ToString("yyyy-MM-dd"),
                        Doctor = DoctorMapper.Find(Convert.ToInt32(result["doctorId"]))
                    });
                }

                return resultList;
            }
            catch (MySqlException exception) {
                Console.Write(exception);
            }
            finally
            {
                connection.Close();
            }

            return resultList;
        }
        
        
    }
}