﻿using System;
using System.Linq;
using XTecDigital_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Data.SqlClient;
using System.Collections.Generic;
using MongoDB.Bson.IO;


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
        public List<Object> validarUsuario(Usuario usuario)
        {
            var connectionString = "mongodb+srv://admin:admin@usuarios.ozlkz.mongodb.net/Usuarios?retryWrites=true&w=majority";
            var mongoClient = new MongoClient(connectionString);
            var dataBase = mongoClient.GetDatabase("Usuarios");
            var collection = dataBase.GetCollection<BsonDocument>("estudiantes");
            var filter1 = Builders<BsonDocument>.Filter.Eq("carnet", usuario.carnet);
            var filter2 = Builders<BsonDocument>.Filter.Eq("password", usuario.password);
            var projection = Builders<BsonDocument>.Projection.Exclude("_id");
            var document = collection.Find(filter1 & filter2).Project(projection).FirstOrDefault();
            List<Object> respuesta = new List<Object>();
            if (document != null)
            {
                var response = new[]
                    {
                        new
                        {
                            respuesta = "200 OK",
                            error = "null"
                        },
                     };
                var estudiante = new[]
                        {
                        new
                        {
                            carnet = document.GetValue("carnet").AsString,
                            nombre = document.GetValue("nombre").AsString,
                            email = document.GetValue("email").AsString,
                            rol = document.GetValue("rol").AsString,
                        },
                     };
                respuesta.Add(response);
                respuesta.Add(estudiante);
                return respuesta;
            }
            else
            {
                return buscarProfesor(usuario);
            }
            
        }

        private List<Object> buscarProfesor(Usuario usuario)
        {
            var connectionString = "mongodb+srv://admin:admin@usuarios.ozlkz.mongodb.net/Usuarios?retryWrites=true&w=majority";
            var mongoClient = new MongoClient(connectionString);
            var dataBase = mongoClient.GetDatabase("Usuarios");
            var collection = dataBase.GetCollection<BsonDocument>("profesores");
            var filter1 = Builders<BsonDocument>.Filter.Eq("cedula", usuario.carnet);
            var filter2 = Builders<BsonDocument>.Filter.Eq("password", usuario.password);
            var projection = Builders<BsonDocument>.Projection.Exclude("_id");
            var document = collection.Find(filter1 & filter2).Project(projection).FirstOrDefault();
            List<Object> respuesta = new List<Object>();
            if (document != null)
            {
                var response = new[]
                    {
                        new
                        {
                            respuesta = "200 OK",
                            error = "null"
                        },
                     };
                var profesor = new[]
                        {
                        new
                        {
                            cedula = document.GetValue("cedula").AsString,
                            nombre = document.GetValue("nombre").AsString,
                            email = document.GetValue("email").AsString,
                            rol = document.GetValue("rol").AsString,
                        },
                     };
                respuesta.Add(response);
                respuesta.Add(profesor);
                return respuesta;
            }
            else
            {
                return buscarAdmin(usuario);
            }
        }

        private List<object> buscarAdmin(Usuario usuario)
        {
            var connectionString = "mongodb+srv://admin:admin@usuarios.ozlkz.mongodb.net/Usuarios?retryWrites=true&w=majority";
            var mongoClient = new MongoClient(connectionString);
            var dataBase = mongoClient.GetDatabase("Usuarios");
            var collection = dataBase.GetCollection<BsonDocument>("admin");
            var filter1 = Builders<BsonDocument>.Filter.Eq("cedula", usuario.carnet);
            var filter2 = Builders<BsonDocument>.Filter.Eq("password", usuario.password);
            var projection = Builders<BsonDocument>.Projection.Exclude("_id");
            var document = collection.Find(filter1 & filter2).Project(projection).FirstOrDefault();
            List<Object> respuesta = new List<Object>();
            if (document != null)
            {
                var response = new[]
                    {
                        new
                        {
                            respuesta = "200 OK",
                            error = "null"
                        },
                     };
                var admin = new[]
                        {
                        new
                        {
                            cedula = document.GetValue("cedula").AsString,
                            rol = document.GetValue("rol").AsString,
                        },
                     };
                respuesta.Add(response);
                respuesta.Add(admin);
                return respuesta;
            }
            else
            {
                var response = new[]
                   {
                        new
                        {
                            respuesta = "200 OK",
                            error = "No existe el usuario"
                        },
                     };
                respuesta.Add(response);
                return respuesta;
            }
        }

        [Route("agregarEstudiante")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public object agregarEstudiante(Usuario usuario)
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
            return agregarEstudianteSQL(usuario);
        }

        [Route("agregarProfesor")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public object agregarProfesor(Usuario usuario)
        {
            var connectionString = "mongodb+srv://admin:admin@usuarios.ozlkz.mongodb.net/Usuarios?retryWrites=true&w=majority";
            var mongoClient = new MongoClient(connectionString);
            var dataBase = mongoClient.GetDatabase("Usuarios");
            var collection = dataBase.GetCollection<BsonDocument>("profesores");
            var document = new BsonDocument
            {
                { "cedula", usuario.carnet },
                { "nombre", usuario.nombre },
                { "email", usuario.email },
                { "telefono", usuario.telefono },
                { "password", usuario.password },
                { "rol", usuario.rol },
            };
            collection.InsertOne(document);
            return agregarProfesorSQL(usuario);
        }

        [Route("agregarAdmin")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public object agregarAdmin(Usuario usuario)
        {
            var connectionString = "mongodb+srv://admin:admin@usuarios.ozlkz.mongodb.net/Usuarios?retryWrites=true&w=majority";
            var mongoClient = new MongoClient(connectionString);
            var dataBase = mongoClient.GetDatabase("Usuarios");
            var collection = dataBase.GetCollection<BsonDocument>("admin");
            var document = new BsonDocument
            {
                { "cedula", usuario.carnet },
                { "rol", usuario.rol },
            };
            collection.InsertOne(document);
            return agregarAdminSQL(usuario);
        }


        [Route("agregarEstudianteSQL")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public object agregarEstudianteSQL(Usuario usuario)
        {
            SqlConnection conn = new SqlConnection(serverKey);
            conn.Open();
            string insertQuery = "agregarEstudiante";
            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@carnet", usuario.carnet);
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

        
        [Route("agregarProfesorSQL")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public object agregarProfesorSQL(Usuario usuario)
        {
            SqlConnection conn = new SqlConnection(serverKey);
            conn.Open();
            string insertQuery = "agregarProfesor";
            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@cedula", usuario.cedula);
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

        [Route("agregarAdminSQL")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public object agregarAdminSQL(Usuario usuario)
        {
            SqlConnection conn = new SqlConnection(serverKey);
            conn.Open();
            string insertQuery = "agregarAdmin";
            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@cedula", usuario.cedula);
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
