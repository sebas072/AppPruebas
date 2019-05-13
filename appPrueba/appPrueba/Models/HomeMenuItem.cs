using System;
using System.Collections.Generic;
using System.Text;

namespace appPrueba.Models
{
    public enum MenuItemType
    {
        Clientes,
        Salir
    }
    public enum StatusCode
    {
        pending,
        approved,
        accepted,
        rejected,
        disabled
    }
    public class ItemsStatus
    {
        public StatusCode Id { get; set; }

        public string Title { get; set; }
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
