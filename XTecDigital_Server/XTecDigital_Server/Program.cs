using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Bson;

namespace XTecDigital_Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MongoClient dbClient = new MongoClient("mongodb+srv://admin:admin@usuarios.ozlkz.mongodb.net/<dbname>?retryWrites=true&w=majority");

            var dbList = dbClient.ListDatabases().ToList();
            var database = dbClient.GetDatabase("xTecDigital");
            var collection = database.GetCollection<BsonDocument>("estudiantes");

            Console.WriteLine("holaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            var firstDocument = collection.Find(new BsonDocument()).FirstOrDefault();
            Console.WriteLine(firstDocument.ToString());
            Console.WriteLine("adiossssssssssssssssssssssssssssssss");
            CreateHostBuilder(args).Build().Run();


        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
