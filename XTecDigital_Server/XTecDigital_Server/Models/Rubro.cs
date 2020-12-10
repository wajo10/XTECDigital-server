using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XTecDigital_Server.Models
{
    public class Rubro
    {
        public Rubro()
        {
        }
        public int idRubro { get; set; }
        public string rubro { get; set; }
        public float porcentaje { get; set; }
        public int idGrupo { get; set; }
        public string codigoCurso { get; set; }
        public int numeroGrupo { get; set; }
    }
}
