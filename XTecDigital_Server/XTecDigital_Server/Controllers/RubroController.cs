using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using XTecDigital_Server.Models;

namespace XTecDigital_Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RubroController : Controller
    {
        private string serverKey = Startup.getKey();

        [Route("crearRubro")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public Object crearRubro(Rubro rubro)
        {
            SqlConnection conn = new SqlConnection(serverKey);
            conn.Open();
            string insertQuery = "crearRubro";
            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@rubro", rubro.rubro);
            cmd.Parameters.AddWithValue("@porcentaje", rubro.porcentaje);
            cmd.Parameters.AddWithValue("@codigoCurso", rubro.codigoCurso);
            cmd.Parameters.AddWithValue("@numeroGrupo", rubro.numeroGrupo);
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


        [Route("eliminarRubro")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public Object eliminarRubro(Rubro rubro)
        {
            SqlConnection conn = new SqlConnection(serverKey);
            conn.Open();
            string insertQuery = "eliminarRubro";
            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@rubro", rubro.rubro);
            cmd.Parameters.AddWithValue("@porcentaje", rubro.porcentaje);
            cmd.Parameters.AddWithValue("@codigoCurso", rubro.codigoCurso);
            cmd.Parameters.AddWithValue("@numeroGrupo", rubro.numeroGrupo);
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

        [Route("verificarRubros")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public List<object> verificarRubros(Rubro rubro)
        {
            List<Object> respuesta = new List<Object>();
            SqlConnection conn = new SqlConnection(serverKey);
            conn.Open();
            string insertQuery = "verificarRubros";
            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@codigoCurso", rubro.codigoCurso);
            cmd.Parameters.AddWithValue("@numeroGrupo", rubro.numeroGrupo);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            
            try
            {
                while (dr.Read())
                {
                    var jsons = new[]
                    {
                        new {
                            respuesta = dr.Read(),
                        }
                     };
                    respuesta.Add(jsons);
                }
            }
            catch
            {
                Debug.WriteLine("Usuario no encontrado");
            }
            conn.Close();
            return respuesta;
        }


    }
}
