using System;
using MySql.Data.MySqlClient;

namespace VIS_PR
{
    public class User
    {
        private static int DefaultId => -1;
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int IsAdmin { get; set; }
        public Doctors Doctor { get; set; }

        public User()
        {
            Id = DefaultId;
        }
        
    }
}