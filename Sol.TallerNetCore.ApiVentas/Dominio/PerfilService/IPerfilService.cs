using Sol.TallerNetCore.ApiVentas.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sol.TallerNetCore.ApiVentas.Dominio.PerfilService
{
    public interface IPerfilService
    {
        List<Perfil> Listar();
        Perfil Recuperar(int id);
        Perfil Insertar(Perfil perfil);

        Perfil Actualizar(Perfil perfil);

        bool Eliminar(int id);
    }
}
