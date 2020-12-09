using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;

namespace XTecDigital_Server.Models
{
    public class FileManager
    {
        public string Action { get; set; }
        public string path { get; set; }
        public int Grupo { get; set; }
        public string Curso { get; set; }
        public string Details { get; set; }
        public string getString()
        {
            String str = "Action: "+ Action + " Path: " + path + " Grupo: " + Grupo.ToString() + " Curso: " + Curso;
            return str;
        }
        public object read(String serverKey)
        {
            var args = this;
            List<Object> carpetas = new List<Object>();
            SqlConnection conn = new SqlConnection(serverKey);
            conn.Open();
            SqlCommand cmd;
            var type = "";
            var isFile = false;
            if (args.path == "/")
            {
                Debug.WriteLine("Inicial");
                string insertQuery = "verCarpetasGrupo";
                cmd = new SqlCommand(insertQuery, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codigoCurso", args.Curso);
                cmd.Parameters.AddWithValue("@numeroGrupo", args.Grupo);
            }

            else
            {
                Debug.WriteLine("Entra a carpeta");
                args.path = args.path.Replace("/", "");
                Debug.WriteLine(args.path);
                string insertQuery = "verDocumentosGrupo";
                cmd = new SqlCommand(insertQuery, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombreCarpeta", args.path);
                cmd.Parameters.AddWithValue("@codigoCurso", args.Curso);
                cmd.Parameters.AddWithValue("@numeroGrupo", args.Grupo);
                isFile = true;
            }
            SqlDataReader dr = cmd.ExecuteReader();
            var error = new { code = 0 , mesagge = " none" };
            var slash = '/';
            try
            {
                while (dr.Read())
                {
                    var dateCreated = new DateTime();
                    var size = new decimal();
                    if (args.path == "/")
                    {
                        dateCreated = (DateTime)dr[3];
                        // size = (decimal)dr[2];
                        size = 25;
                    }
                    else
                    {
                        dateCreated = (DateTime)dr[5];
                        size = (decimal)dr[4];
                        type = dr[3].ToString();
                    }

                    var files = new[]
                    {
                        new {
                            name = dr[1].ToString(),
                            dateCreated = dateCreated,
                            dateModified = dateCreated,
                            isFile = isFile,
                            size = size,
                            hasChild = false,
                            type = type,
                            filterPath = slash
                        },

                     };
                    carpetas.Add(files);
                    error = new { code = 20, mesagge = "Error encontrando los archivos" };
                }


            }
            catch(ArgumentException e)
            {
                error = new { code = 20, mesagge = "Error encontrando los archivos" };
                Debug.WriteLine(e);

            }

            List<object> retornar = new List<object>();
            List<object> tempFiles = new List<object>();
            for (var x = 0; x < carpetas.Count; x++)
            {
                var tempList = (IList<object>)carpetas[x];
                tempFiles.Add(tempList[0]);
            }
            conn.Close();
            
            var details = new { };
            var cwd = new
            {
                name = "Root",
                size = 0,
                dateModified = "2019-02-28T03:48:19.8319708+00:00",
                dateCreated = "2019-02-27T17:36:15.812193+00:00",
                hasChild = false,
                isFile = true,
                type = " ",
                filterPath = slash,
            }
            ;
            var jsonRet = new
            {
                cwd = cwd,
                files = tempFiles,
                error = error,
                details = args.Details
            };
            return jsonRet;
        }

    }

   
}
