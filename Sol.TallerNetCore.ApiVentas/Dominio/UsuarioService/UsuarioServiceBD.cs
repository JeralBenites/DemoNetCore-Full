using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sol.TallerNetCore.ApiVentas.Contexto;
using Sol.TallerNetCore.ApiVentas.DTO;

namespace Sol.TallerNetCore.ApiVentas.Dominio.UsuarioService
{
    public class UsuarioServiceBD : IUsuarioService
    {
        BDPedidosContext _context;
        public UsuarioServiceBD(BDPedidosContext context)
        {
            this._context = context;
        }
        public Usuario Actualizar(Usuario usuario)
        {
            _context.Usuario.Update(usuario);
            _context.SaveChanges();
            return usuario;
        }

        public Usuario Insertar(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            _context.SaveChanges();
            return usuario;
        }

        public List<Usuario> Listar()
        {
            return _context.Usuario.ToList();
        }

        public Usuario Recuperar(int id)
        {
            return _context.Usuario.AsNoTracking()
                .FirstOrDefault(t => t.IdUsuario == id);
        }

        public Usuario RecuperarPorCorreo(string correo)
        {
            return _context.Usuario.AsNoTracking()
                .FirstOrDefault(t => t.Email == correo);
        }
    }
}
