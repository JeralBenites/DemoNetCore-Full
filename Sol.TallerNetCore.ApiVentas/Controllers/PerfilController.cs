using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sol.TallerNetCore.ApiVentas.Dominio.PerfilService;
using Sol.TallerNetCore.ApiVentas.DTO;

namespace Sol.TallerNetCore.ApiVentas.Controllers
{
 
    public class PerfilController : BaseControladora
    {
        private IPerfilService _perfilService;
        public PerfilController(IPerfilService perfilService)
        {
            _perfilService = perfilService;
        }

        [HttpGet]

        public List<Perfil> ListarPerfiles() {
            return _perfilService.Listar();
        }

        [HttpPost]
        public Perfil Insertar([FromBody] Perfil perfil) {
            //return Created(perfil);
            //return null;
            return _perfilService.Insertar(perfil);

        }

        //[HttpPatch]
        [HttpPut]
        public IActionResult Actualizar(int id, [FromBody] Perfil perfil) {

            Perfil tmp = _perfilService.Recuperar(id);
            //verficar que exista
            if (tmp == null)
            {
                return BadRequest("El codigo no existe");
            }

            perfil.IdPerfil = tmp.IdPerfil;
            return Ok( _perfilService.Actualizar(perfil));

        }
    }
}