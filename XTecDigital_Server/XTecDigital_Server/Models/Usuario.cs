using System;
using System.Collections.Generic;

namespace XTecDigital_Server.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
        }
        public int carnet { get; set; }
        public int cedula { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }
        public string password { get; set; }
        public string rol { get; set; }
        public string codigoCurso { get; set; }
        public int numeroGrupo { get; set; }
        public int cedulaProfesor { get; set; }

    }
}