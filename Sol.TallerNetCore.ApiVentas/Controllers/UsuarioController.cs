using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sol.TallerNetCore.ApiVentas.Dominio.UsuarioService;
using Sol.TallerNetCore.ApiVentas.DTO;
using Sol.TallerNetCore.ApiVentas.Transporte.Request;
using Sol.TallerNetCore.ApiVentas.Utiles;

namespace Sol.TallerNetCore.ApiVentas.Controllers
{
    
    public class UsuarioController : BaseControladora
    {
        private IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(_usuarioService.Listar());
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Insertar([FromBody]UsuarioInsertarRequest usuarioInsertarRequest)
        {
            if (string.IsNullOrEmpty(usuarioInsertarRequest.Password))
            {
                return BadRequest("Debe enviar la contraseña");
            }
            Usuario usuario = new Usuario();
            usuario.Nombres = usuarioInsertarRequest.Nombres;
            usuario.Email = usuarioInsertarRequest.Correo;
            usuario.Password = 
                EncriptadorHelper.EncryptToByte(usuarioInsertarRequest.Password);

            return Ok(_usuarioService.Insertar(usuario));
        }

        [HttpPut]
        public IActionResult ActualizarFoto(int id, IFormFile foto)
        {
            if (foto == null || foto.Length == 0)
            {
                return BadRequest("Debe adjuntar un archivo");
            }

            Usuario u = _usuarioService.Recuperar(id);
            if (u == null)
            {
                return BadRequest("El usuario no es valido");
            }

            string nuevoNombre = Guid.NewGuid().ToString();
            FileInfo f = new FileInfo(foto.FileName);
            string ext = f.Extension;
            string nombreArchivo = nuevoNombre + ext;

            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "img",
                "usuario",
                nombreArchivo//foto.FileName
                );

            

            using (var stream = new FileStream(path, FileMode.Create))
            {
                foto.CopyTo(stream);
            }
           
            u.Imagen = nombreArchivo;

           return Ok( _usuarioService.Actualizar(u));

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult VerFoto(string ruta) {

            if (string.IsNullOrEmpty(ruta))
            {
                return BadRequest("Debe ingresar la imagen");
            }

            var path = Path.Combine(
               Directory.GetCurrentDirectory(),
               "wwwroot",
               "img",
               "usuario",
               ruta//foto.FileName
               );

            if (!System.IO.File.Exists(path))
            {
                return BadRequest("El archivo no existe en el Site");
            }

            MemoryStream ms = new MemoryStream();

            using (var stream = new FileStream(path, FileMode.Open))
            {
                stream.CopyTo(ms);
            }

            ms.Position = 0;

            return File(ms, GetContentType(path), Path.GetFileName(path));

            //return Ok();
        }

        [HttpPut]
        public IActionResult Actualizar(int id, [FromBody] Usuario usuario)
        {
            return Ok();
        }



        [NonAction]
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }



        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }


    }
}