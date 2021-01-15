using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XTecDigital_Server.Models
{
    public class Grupo
    {
        public Grupo()
        {
        }
        public int idGrupo { get; set; }
        public string codigoCurso { get; set; }
        public int numeroGrupo { get; set; }
        public string cedulaProfesor { get; set; }
        public string carnet { get; set; }
        public int ano { get; set; }
        public string periodo { get; set; }
        public int grupo { get; set; }
    }
}
