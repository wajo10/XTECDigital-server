using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XTecDigital_Server.Models
{
    public class FileManager
    {
        public string Action { get; set; }
        public string path { get; set; }
        public int Grupo { get; set; }
        public string Curso { get; set; }
        public string getString()
        {
            String str = "Action: "+ Action + " Path: " + path + " Grupo: " + Grupo.ToString() + " Curso: " + Curso;
            return str;
        }
    }

   
}
