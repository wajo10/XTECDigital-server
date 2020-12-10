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
        public Object crearCurso(Curso curso)
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

        [Route("eliminarCurso")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public object eliminarCurso(Curso curso)
        {
            SqlConnection conn = new SqlConnection(serverKey);
            conn.Open();
            string insertQuery = "eliminarCurso";
            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Codigo", curso.codigo);
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
