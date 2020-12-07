using System;
using System.Collections.Generic;

namespace XTecDigital_Server.Models
{
    public class Semestre
    {
        public Semestre()
        {
        }
        public int idSemestre { get; set; }
        public int ano { get; set; }
        public string periodo { get; set; }
        public int cedulaAdmin { get; set; }
    }
}
