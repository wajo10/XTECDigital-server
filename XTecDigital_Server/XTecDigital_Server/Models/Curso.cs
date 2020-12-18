using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XTecDigital_Server.Models
{
    public class Curso
    {
        public Curso()
        {
        }
        public string codigo { get; set; }
        public string codigoCurso { get; set; }
        public string nombre { get; set; }
        public string carrera { get; set; }
        public int creditos { get; set; }
        public int idSemestre { get; set; }
        public bool habilitado { get; set; }
        public string cedulaAdmin { get; set; }
        public int ano { get; set; }
        public string periodo { get; set; }

    }
}
