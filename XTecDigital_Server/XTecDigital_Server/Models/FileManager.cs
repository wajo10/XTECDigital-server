using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

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
            string insertQuery = "verCarpetasGrupo";
            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@codigoCurso", args.Curso);
            cmd.Parameters.AddWithValue("@numeroGrupo", args.Grupo);
            SqlDataReader dr = cmd.ExecuteReader();
            var error = new { code = 0 , mesagge = " none" };
            var slash = '/';
            try
            {
                while (dr.Read())
                {

                    var files = new[]
                    {
                        new {
                            name = dr[1].ToString(),
                            dateCreated = (DateTime) dr[3],
                            dateModified = (DateTime)dr[3],
                            isFile = false,
                            size = (int)dr[2],
                            hasChild = false,
                            type = "",
                            filterPath = slash
                        },

                     };
                    carpetas.Add(files);
                    error = new { code = 20, mesagge = "Error encontrando los archivos" };
                }


            }
            catch
            {
                error = new { code = 20, mesagge = "Error encontrando los archivos" };

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
