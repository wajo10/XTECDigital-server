using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using XTecDigital_Server.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Diagnostics;

namespace XTecDigital_Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CarpetaController : Controller
    {
        private string serverKey = Startup.getKey();

        [Route("crearCarpeta")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void crearCarpeta(Carpeta semestre)
        {
            SqlConnection conn = new SqlConnection(serverKey);
            conn.Open();
            string insertQuery = "crearCarpeta";
            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nombre", semestre.nombre);
            cmd.Parameters.AddWithValue("@idGrupo", semestre.idGrupo);
            try
            {
                cmd.ExecuteNonQuery();
                Debug.WriteLine("Carpeta creado exitosamente");
            }
            catch
            {
                Debug.WriteLine("Error al crear carpeta");
            }
            conn.Close();
        }


        [Route("eliminarCarpetaAdmin")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void eliminarCarpetaAdmin(Carpeta semestre)
        {
            SqlConnection conn = new SqlConnection(serverKey);
            conn.Open();
            string insertQuery = "eliminarCarpetaAdmin";
            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nombre", semestre.nombre);
            cmd.Parameters.AddWithValue("@idGrupo", semestre.idGrupo);
            try
            {
                cmd.ExecuteNonQuery();
                Debug.WriteLine("Carpeta eliminada exitosamente");
            }
            catch
            {
                Debug.WriteLine("Error al eliminar carpeta");
            }
            conn.Close();
        }
    }
}
