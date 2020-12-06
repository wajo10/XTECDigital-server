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
        public void crearSemestre(Semestre semestre)
        {
            SqlConnection conn = new SqlConnection(serverKey);
            conn.Open();
            string insertQuery = "crearSemestre";
            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ano", semestre.ano);
            cmd.Parameters.AddWithValue("@periodo", semestre.periodo);
            cmd.Parameters.AddWithValue("@cedulaAdmin", semestre.cedulaAdmin);
            cmd.ExecuteNonQuery();
            Debug.WriteLine("Semestre creado exitosamente");
            conn.Close();
        }



    }
}
