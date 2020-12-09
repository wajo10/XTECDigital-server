using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XTecDigital_Server.Models;

namespace XTecDigital_Server.Controllers
{
    public class GrupoController : Controller
    {
        private string serverKey = Startup.getKey();

        [Route("crearGrupo")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void crearGrupo(Grupo grupo)
        {
            SqlConnection conn = new SqlConnection(serverKey);
            conn.Open();
            string insertQuery = "crearGrupo";
            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@codigoCurso", grupo.codigoCurso);
            cmd.Parameters.AddWithValue("@numeroGrupo", grupo.numeroGrupo);
            cmd.ExecuteNonQuery();
            conn.Close();
        }


        [Route("eliminarEvaluacion")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void eliminarEvaluacion(Evaluacion evaluacion)
        {
            SqlConnection conn = new SqlConnection(serverKey);
            conn.Open();
            string insertQuery = "eliminarEvaluacion";
            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nombre", evaluacion.nombre);
            cmd.Parameters.AddWithValue("@rubro", evaluacion.rubro);
            cmd.Parameters.AddWithValue("@codigoCurso", evaluacion.codigoCurso);
            cmd.Parameters.AddWithValue("@numeroGrupo", evaluacion.numeroGrupo);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
