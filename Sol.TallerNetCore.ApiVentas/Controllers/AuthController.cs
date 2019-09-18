using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Sol.TallerNetCore.ApiVentas.Dominio.UsuarioService;
using Sol.TallerNetCore.ApiVentas.DTO;
using Sol.TallerNetCore.ApiVentas.Transporte.Request;
using Sol.TallerNetCore.ApiVentas.Utiles;

namespace Sol.TallerNetCore.ApiVentas.Controllers
{
    public class AuthController : BaseControladora
    {
        private IUsuarioService _usuarioService;
        public IConfiguration Configuration { get; }

        public AuthController(IUsuarioService usuarioService, 
            IConfiguration configuration)
        {
            this._usuarioService = usuarioService;
            Configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult PedirToken(
            [FromBody] PedirTokenRequest request)
        {

            Usuario usuario = 
                _usuarioService.RecuperarPorCorreo(request.codigo);

            //Validar que haya recuperado
            if (usuario == null)
            {
                return BadRequest("Usuario invalido: correo");
            }

            //Validar la clave
            if (EncriptadorHelper.Decrypt(usuario.Password) != request.clave)
            {
                return BadRequest("Usuario invalido: clave");
            }

            //Metadata para el token
            Claim[] misClaims = new[] {

                new Claim("correo", usuario.Email),
                new Claim("id", usuario.IdUsuario.ToString())
        };
            //Generar el JWT  JwtSecurityToken

            var cred = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(
                                    Configuration["SemillaJWT"]
                                    ));

            var firma = new SigningCredentials(cred, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: "midominio.com", 
                audience: "midominio.com",
                claims: misClaims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials : firma
                )  ;

            //Devolver el hash
            string ruta = new JwtSecurityTokenHandler().WriteToken(jwt);

            return Ok(
                new {
                    usuario = usuario,
                    token = ruta
                }
                );
        }
    }
}