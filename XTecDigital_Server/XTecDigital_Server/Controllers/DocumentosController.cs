using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
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
        [Route("Download")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public FileStreamResult Descargar([FromForm] FileManager args)
        {

            var defaultAvatarAsBase64 = "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";

            var imageStream = new MemoryStream();
            var bytes = Convert.FromBase64String(defaultAvatarAsBase64);
            imageStream = new MemoryStream(bytes);

            var result = new FileStreamResult(imageStream, "APPLICATION/octet-stream");

            result.FileDownloadName = "Holaa.jpg";

            return result;
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
                    return args.read(serverKey);
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
