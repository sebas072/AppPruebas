using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace appPrueba.Models
{
    public class Log
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        public string descripcion { get; set; }
        public DateTime fecha { get; set; }

        [ForeignKey(typeof(Client))]
        public string id_Client { get; set; }
       
    }
}
