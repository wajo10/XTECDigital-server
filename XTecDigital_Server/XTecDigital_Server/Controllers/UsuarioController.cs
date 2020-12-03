﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XTecDigital_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Cors;
using System.Diagnostics;
using System.Text.Json;
using MongoDB.Driver;
using MongoDB.Bson;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace XTecDigital_Server.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
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
            string FalseValue = "false";
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
    }
}