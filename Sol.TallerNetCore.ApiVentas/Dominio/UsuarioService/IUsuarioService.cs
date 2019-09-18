using Sol.TallerNetCore.ApiVentas.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sol.TallerNetCore.ApiVentas.Dominio.UsuarioService
{
    public interface IUsuarioService
    {

        List<Usuario> Listar();
        Usuario Recuperar(int id);

        Usuario Actualizar(Usuario usuario);

        Usuario Insertar(Usuario usuario);

        Usuario RecuperarPorCorreo(string correo);


    }
}
