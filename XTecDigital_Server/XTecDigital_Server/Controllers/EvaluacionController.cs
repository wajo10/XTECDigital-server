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
    [ApiController]
    [Route("[controller]")]
    public class EvaluacionController : Controller
    {
        private string serverKey = Startup.getKey();

        [Route("crearEvaluacion")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void crearEvaluacion(Evaluacion evaluacion)
        {
            SqlConnection conn = new SqlConnection(serverKey);
            conn.Open();
            string insertQuery = "crearEvaluacion";
            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@grupal", evaluacion.grupal);
            cmd.Parameters.AddWithValue("@nombre", evaluacion.nombre);
            cmd.Parameters.AddWithValue("@porcentaje", evaluacion.porcentaje);
            cmd.Parameters.AddWithValue("@fechaInicio", evaluacion.fechaInicio);
            cmd.Parameters.AddWithValue("@fechaFin", evaluacion.fechaFin);
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
