using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sol.TallerNetCore.ApiVentas.DTO;

namespace Sol.TallerNetCore.ApiVentas.Dominio.PerfilService
{
    public class PerfilServiceMemoria : IPerfilService
    {
        List<Perfil> _lista;

        public PerfilServiceMemoria()
        {
            _lista = new List<Perfil>()
            {
                new Perfil(){ IdPerfil = 1, NombrePerfil = "Pruebas"},
                new Perfil(){ IdPerfil = 2, NombrePerfil = "Produccion"}
            };
        }

        public Perfil Actualizar(Perfil perfil)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public Perfil Insertar(Perfil perfil)
        {
            throw new NotImplementedException();
        }

        public List<Perfil> Listar()
        {
            return _lista;
        }

        public Perfil Recuperar(int id)
        {
            //Forma 1
            Perfil p;
            foreach (Perfil item in _lista)
            {
                if (item.IdPerfil == id)
                {
                    p = item;
                }
            }

            //Forma 2: LINQ to Entities
            p = (from x in _lista
                 where x.IdPerfil == id
                 select x).FirstOrDefault();


            //Forma 3: Expresiones Lambda
            p = _lista.FirstOrDefault(t => t.IdPerfil == id);
            p = _lista.Where(t => t.IdPerfil == id).FirstOrDefault();

            return p;

        }
    }
}
