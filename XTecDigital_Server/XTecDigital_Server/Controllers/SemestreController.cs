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
    public class SemestreController : ControllerBase
    {

        private string serverKey = Startup.getKey();

        [Route("crearSemestre")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public object crearSemestre(Semestre semestre)
        {
            SqlConnection conn = new SqlConnection(serverKey);
            conn.Open();
            string insertQuery = "crearSemestre";
            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ano", semestre.ano);
            cmd.Parameters.AddWithValue("@periodo", semestre.periodo);
            cmd.Parameters.AddWithValue("@cedulaAdmin", semestre.cedulaAdmin);
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

        [Route("verSemestres")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public List<Object> verSemestres()
        {
            List<Object> semestres = new List<Object>();
            //Connect to database
            SqlConnection conn = new SqlConnection(serverKey);
            conn.Open();
            string insertQuery = "verSemestres";
            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            try
            {
                var response = new[]
                    {
                        new
                        {
                            respuesta = "200 OK",
                            error = "null"
                        }

                     };
                semestres.Add(response);
                while (dr.Read())
                {
                    var jsons = new[]
                    {
                        new {
                            ano = dr[1].ToString(),
                            periodo = dr[2].ToString(),
                            cedulaAdmin = dr[3].ToString(),
                        }

                     };
                    Console.WriteLine(jsons);
                    semestres.Add(jsons);
                }

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
                semestres.Add(response);

            }

            List<object> retornar = new List<object>();
            for (var x = 0; x < semestres.Count; x++)
            {
                var tempList = (IList<object>)semestres[x];
                retornar.Add(tempList[0]);
            }
            conn.Close();
            return retornar;

        }






    }
}
