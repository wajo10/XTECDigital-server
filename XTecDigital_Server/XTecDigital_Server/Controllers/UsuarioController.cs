using System;
using System.Linq;
using XTecDigital_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Data.SqlClient;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace XTecDigital_Server.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private string serverKey = Startup.getKey();

        [Route("validarUser")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public Object validarUsuario(Usuario usuario)
        {
            var connectionString = "mongodb+srv://admin:admin@usuarios.ozlkz.mongodb.net/Usuarios?retryWrites=true&w=majority";
            var mongoClient = new MongoClient(connectionString);
            var dataBase = mongoClient.GetDatabase("Usuarios");
            var collection = dataBase.GetCollection<BsonDocument>("estudiantes");
            var filter1 = Builders<BsonDocument>.Filter.Eq("carnet", usuario.carnet);
            var filter2 = Builders<BsonDocument>.Filter.Eq("password", usuario.password);
            var projection = Builders<BsonDocument>.Projection.Exclude("_id");
            var document = collection.Find(filter1 & filter2).Project(projection).FirstOrDefault();
            var jsons = new[]
            {
                new BsonDocument {{ "response", "false" }},
                document
            };
            if (jsons[1] == null)
            {
                return jsons[0].ToString();
            }
            else
            {
                return jsons[1].ToString();
            }
        }


        [Route("agregarEstudiante")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void agregarEstudiante(Usuario usuario)
        {
            var connectionString = "mongodb+srv://admin:admin@usuarios.ozlkz.mongodb.net/Usuarios?retryWrites=true&w=majority";
            var mongoClient = new MongoClient(connectionString);
            var dataBase = mongoClient.GetDatabase("Usuarios");
            var collection = dataBase.GetCollection<BsonDocument>("estudiantes");
            var document = new BsonDocument
            {
                { "carnet", usuario.carnet },
                { "nombre", usuario.nombre },
                { "email", usuario.email },
                { "telefono", usuario.telefono },
                { "password", usuario.password },
                { "rol", usuario.rol },
            };
            collection.InsertOne(document);
        }

        [Route("agregarProfesor")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void agregarProfesor(Usuario usuario)
        {
            var connectionString = "mongodb+srv://admin:admin@usuarios.ozlkz.mongodb.net/Usuarios?retryWrites=true&w=majority";
            var mongoClient = new MongoClient(connectionString);
            var dataBase = mongoClient.GetDatabase("Usuarios");
            var collection = dataBase.GetCollection<BsonDocument>("profesores");
            var document = new BsonDocument
            {
                { "carnet", usuario.carnet },
                { "nombre", usuario.nombre },
                { "email", usuario.email },
                { "telefono", usuario.telefono },
                { "password", usuario.password },
                { "rol", usuario.rol },
            };
            collection.InsertOne(document);
        }
        
        
        [Route("agregarEstudianteSQL")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void agregarEstudianteSQL(Usuario usuario)
        {
            SqlConnection conn = new SqlConnection(serverKey);
            conn.Open();
            string insertQuery = "agregarEstudiante";
            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@carnet", usuario.carnet);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        
        [Route("agregarProfesorSQL")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void agregarProfesorSQL(Usuario usuario)
        {
            SqlConnection conn = new SqlConnection(serverKey);
            conn.Open();
            string insertQuery = "agregarProfesor";
            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@cedula", usuario.cedula);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        [Route("agregarAdminSQL")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void agregarAdminSQL(Usuario usuario)
        {
            SqlConnection conn = new SqlConnection(serverKey);
            conn.Open();
            string insertQuery = "agregarAdmin";
            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@cedula", usuario.cedula);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        [Route("asignarProfesorGrupo")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void asignarProfesorGrupo(Usuario usuario)
        {
            SqlConnection conn = new SqlConnection(serverKey);
            conn.Open();
            string insertQuery = "asignarProfesorGrupo";
            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@codigoCurso", usuario.codigoCurso);
            cmd.Parameters.AddWithValue("@numeroGrupo", usuario.numeroGrupo);
            cmd.Parameters.AddWithValue("@cedulaProfesor", usuario.cedulaProfesor);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        [Route("eliminarProfesorGrupo")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void eliminarProfesorGrupo(Usuario usuario)
        {
            SqlConnection conn = new SqlConnection(serverKey);
            conn.Open();
            string insertQuery = "eliminarProfesorGrupo";
            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@codigoCurso", usuario.codigoCurso);
            cmd.Parameters.AddWithValue("@numeroGrupo", usuario.numeroGrupo);
            cmd.Parameters.AddWithValue("@cedulaProfesor", usuario.cedulaProfesor);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        [Route("agregarEstudiantesGrupo")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void agregarEstudiantesGrupo(Usuario usuario)
        {
            SqlConnection conn = new SqlConnection(serverKey);
            conn.Open();
            string insertQuery = "agregarEstudiantesGrupo";
            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@carnet", usuario.carnet);
            cmd.Parameters.AddWithValue("@codigoCurso", usuario.codigoCurso);
            cmd.Parameters.AddWithValue("@numeroGrupo", usuario.numeroGrupo);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
