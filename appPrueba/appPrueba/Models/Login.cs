using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace appPrueba.Models
{
    public class Login
    {
        [PrimaryKey]
        public string email { get; set; }
        public string Pass { get; set; }
        public bool Remember { get; set; }
        public string authToken { get; set; }
    }
}
