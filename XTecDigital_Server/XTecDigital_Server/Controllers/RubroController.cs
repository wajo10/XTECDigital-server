using System.Data.SqlClient;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using XTecDigital_Server.Models;

namespace XTecDigital_Server.Controllers
{
    public class RubroController : Controller
    {
        private string serverKey = Startup.getKey();

        [Route("crearRubro")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void crearRubro(Rubro rubro)
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
            cmd.ExecuteNonQuery();
            conn.Close();
        }


        [Route("eliminarRubro")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void eliminarRubro(Rubro rubro)
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
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
