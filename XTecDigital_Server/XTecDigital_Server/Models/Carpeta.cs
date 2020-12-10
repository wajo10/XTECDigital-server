using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XTecDigital_Server.Models
{
    public class Carpeta
    {
        public Carpeta()
        {
        }
        public int idCarpeta { get; set; }
        public string nombre { get; set; }
        public int tamano { get; set; }
        public string fecha { get; set; }
        public int idGrupo { get; set; }
        public string codigoCurso { get; set; }
        public int numeroGrupo { get; set; }
    }
}
