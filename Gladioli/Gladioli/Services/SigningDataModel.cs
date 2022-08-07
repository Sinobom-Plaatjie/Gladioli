using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gladioli.Services
{
    public class SigningDataModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Names { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

    }
}
