using System;
using System.Collections.Generic;

namespace XTecDigital_Server.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
        }

        public int id { get; set; }
        public int carnet { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }
        public string password { get; set; }
    }
}