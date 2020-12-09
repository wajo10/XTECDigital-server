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
    public class DocumentosController : ControllerBase
    {
        private string serverKey = Startup.getKey();

        [Route("crearDocumentos")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void crearDocumentos(Documento documento)
        {
            SqlConnection conn = new SqlConnection(serverKey);
            conn.Open();
            string insertQuery = "crearDocumentos";
            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            var prueba = Convert.FromBase64String(documento.archivo);
            string hex = BitConverter.ToString(prueba);
            hex = hex.Replace("-", "");
            hex = "0x" + hex;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nombreDocumento", documento.nombre);
            cmd.Parameters.AddWithValue("@archivo", documento.archivo);
            cmd.Parameters.AddWithValue("@tamano", documento.tamano);
            cmd.Parameters.AddWithValue("@nombreCarpeta", documento.nombreCarpeta);
            cmd.Parameters.AddWithValue("@idGrupo", documento.idGrupo);
            cmd.Parameters.AddWithValue("@tipoArchivo", documento.tipoArchivo);
            cmd.ExecuteNonQuery();
            conn.Close();

        }

        [Route("eliminarDocumentos")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void eliminarDocumentos(Documento documento)
        {
            SqlConnection conn = new SqlConnection(serverKey);
            conn.Open();
            string insertQuery = "eliminarDocumentos";
            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nombre", documento.nombre);
            cmd.Parameters.AddWithValue("@nombreCarpeta", documento.nombreCarpeta);
            cmd.Parameters.AddWithValue("@idGrupo", documento.idGrupo);
            try
            {
                cmd.ExecuteNonQuery();
                Debug.WriteLine("Documento creado exitosamente");
            }
            catch
            {
                Debug.WriteLine("Error al crear carpeta");
            }
            conn.Close();
        }


        [Route("FileOperations")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public object FileOperations([FromBody] FileManager args)
        {
            if (args.Action == "delete" || args.Action == "rename")
            {
                Debug.WriteLine("Pensar que hacer");
            }
            switch (args.Action)
            {
                case "read":
                    List<Object> carpetas = new List<Object>();
                    SqlConnection conn = new SqlConnection(serverKey);
                    conn.Open();
                    string insertQuery = "verCarpetasGrupo";
                    SqlCommand cmd = new SqlCommand(insertQuery, conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codigoCurso", args.Curso);
                    cmd.Parameters.AddWithValue("@numeroGrupo",args.Grupo);
                    SqlDataReader dr = cmd.ExecuteReader();
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
                            /*
                            filterId = args.Grupo,
                            id = args.Grupo,
                            names = args.Grupo,
                            newName = args.Grupo,
                            parentdId = args.Grupo,
                            path = args.Grupo,
                            permission = args.Grupo,
                            previousName = args.Grupo,
                            renameFiles = args.Grupo,
                            searchString = args.Grupo,
                            showHiddenItems = false,
                            targetData = args.Grupo,
                            targetPath = args.Grupo,
                            uploadFiles = args.Grupo,
                            action = args.Grupo,
                            caseSensitive = args.Grupo,
                            data = args.Grupo*/
                        },

                     };
                            carpetas.Add(files);
                        }
                       

                    }
                    catch
                    {
                        Debug.WriteLine("Usuario no encontrado");

                    }

                    List<object> retornar = new List<object>();
                    List<object> tempFiles = new List<object>();
                    for (var x = 0; x < carpetas.Count; x++)
                    {
                        var tempList = (IList<object>)carpetas[x];
                        tempFiles.Add(tempList[0]);
                    }
                    conn.Close();
                    var error = new { code = 0, mesagge = " none" };
                    var details = new {};
                    var cwd = new{
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
                        error = args.Curso,
                        details = args.Curso
                    };
                  
                    Debug.WriteLine(args.getString());
                    return jsonRet;
                    /*
                case "delete":
                    // deletes the selected file(s) or folder(s) from the given path.
                    return this.operation.ToCamelCase(this.operation.Delete(args.Path, args.Names));
                case "copy":
                    // copies the selected file(s) or folder(s) from a path and then pastes them into a given target path.
                    return this.operation.ToCamelCase(this.operation.Copy(args.Path, args.TargetPath, args.Names, args.RenameFiles, args.TargetData));
                case "move":
                    // cuts the selected file(s) or folder(s) from a path and then pastes them into a given target path.
                    return this.operation.ToCamelCase(this.operation.Move(args.Path, args.TargetPath, args.Names, args.RenameFiles, args.TargetData));
                case "details":
                    // gets the details of the selected file(s) or folder(s).
                    return this.operation.ToCamelCase(this.operation.Details(args.Path, args.Names, args.Data));
                case "create":
                    // creates a new folder in a given path.
                    return this.operation.ToCamelCase(this.operation.Create(args.Path, args.Name));
                case "search":
                    // gets the list of file(s) or folder(s) from a given path based on the searched key string.
                    return this.operation.ToCamelCase(this.operation.Search(args.Path, args.SearchString, args.ShowHiddenItems, args.CaseSensitive));
                case "rename":
                    // renames a file or folder.
                    return this.operation.ToCamelCase(this.operation.Rename(args.Path, args.Name, args.NewName));*/
            }
            return null;
        }

    }
}
