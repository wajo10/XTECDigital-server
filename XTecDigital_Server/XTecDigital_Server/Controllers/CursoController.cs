using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using XTecDigital_Server.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace XTecDigital_Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private string serverKey = Startup.getKey();

        [Route("crearCurso")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void crearCurso(Curso curso)
        {
            SqlConnection conn = new SqlConnection(serverKey);
            conn.Open();
            string insertQuery = "crearCurso";
            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Codigo", curso.codigo);
            cmd.Parameters.AddWithValue("@nombre", curso.nombre);
            cmd.Parameters.AddWithValue("@carrera", curso.carrera);
            cmd.Parameters.AddWithValue("@creditos", curso.creditos);
            cmd.Parameters.AddWithValue("@idSemestre", curso.idSemestre);
            try
            {
                cmd.ExecuteNonQuery();
                Debug.WriteLine("Curso creado exitosamente");
            }
            catch
            {
                Debug.WriteLine("Error al crear curso");
            }
            conn.Close();
        }

        [Route("eliminarCurso")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void eliminarCurso(Curso curso)
        {
            SqlConnection conn = new SqlConnection(serverKey);
            conn.Open();
            string insertQuery = "eliminarCurso";
            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Codigo", curso.codigo);
            try
            {
                cmd.ExecuteNonQuery();
                Debug.WriteLine("Curso eliminado exitosamente");
            }
            catch
            {
                Debug.WriteLine("Error al eliminar curso");
            }
            conn.Close();
        }

        [Route("verCursos")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public List<Object> verCursos()
        {
            List<Object> cursos = new List<Object>();
            Curso usuarioCarrera = new Curso();
            //Connect to database
            SqlConnection conn = new SqlConnection(serverKey);
            conn.Open();
            string insertQuery = "verCursos";
            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader    dr = cmd.ExecuteReader();

            try
            {
                while (dr.Read())
                {
                    var jsons = new[]
                    {
                        new {
                            codigo = dr[0].ToString(),
                            nombre = dr[1].ToString(),
                            carrera = dr[2].ToString(),
                            creditos = (int)dr[3],
                            idSemestre = (int)dr[4],
                        }

                     };
                    Console.WriteLine(jsons);
                    cursos.Add(jsons);
                }

            }
            catch
            {
                Debug.WriteLine("Usuario no encontrado");

            }

            List<object> retornar = new List<object>();
            for (var x = 0; x < cursos.Count; x++)
            {
                var tempList = (IList<object>)cursos[x];
                retornar.Add(tempList[0]);
            }
            conn.Close();
            return retornar;

        }

    }
}
