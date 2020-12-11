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
    public class GrupoController : Controller
    {
        private string serverKey = Startup.getKey();

        [Route("crearGrupo")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public object crearGrupo(Grupo grupo)
        {
            SqlConnection conn = new SqlConnection(serverKey);
            conn.Open();
            string insertQuery = "crearGrupo";
            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@codigoCurso", grupo.codigoCurso);
            cmd.Parameters.AddWithValue("@numeroGrupo", grupo.numeroGrupo);
            List<Object> respuesta = new List<Object>();
            try
            {
                cmd.ExecuteNonQuery();
                var response = new[]
                    {
                        new
                        {
                            respuesta = "200 OK",
                            error = "null"
                        }

                     };
                respuesta.Add(response);
            }
            catch (Exception e)
            {
                string[] separatingStrings = { "\r" };
                var response = new[]
                    {
                        new
                        {
                            respuesta = "error",
                            error = e.Message.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries)[0]
            }

                     };
                respuesta.Add(response);
            }
            conn.Close();
            return respuesta[0];
        }


        [Route("eliminarGrupo")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void eliminarGrupo(Evaluacion evaluacion)
        {
            SqlConnection conn = new SqlConnection(serverKey);
            conn.Open();
            string insertQuery = "eliminarGrupo";
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
