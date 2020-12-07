using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XTecDigital_Server.Models
{
    public class Documento
    {
        public Documento()
        {
        }
        public int idDocumento { get; set; }
        public string nombre { get; set; }
        public byte[] archivo { get; set; }
        public float tamano { get; set; }
        public DateTime fechaSubido { get; set; }
        public int idCarpeta { get; set; }
        public string tipoArchivo { get; set; }
        public string nombreCarpeta { get; set; }
        public string idGrupo { get; set; }
    }
}
