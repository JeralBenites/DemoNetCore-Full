using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sol.TallerNetCore.ApiVentas.Contexto;
using Sol.TallerNetCore.ApiVentas.DTO;

namespace Sol.TallerNetCore.ApiVentas.Dominio.PerfilService
{
    public class PerfilServiceBD : IPerfilService
    {
        BDPedidosContext _context;

        public PerfilServiceBD(BDPedidosContext contexto)
        {
            this._context = contexto;
        }

        public Perfil Actualizar(Perfil perfil)
        {
            _context.Perfil.Update(perfil);
            _context.SaveChanges();
            return perfil;
        }



        public bool Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        
        public Perfil Insertar(Perfil perfil)
        {
            _context.Perfil.Add(perfil);
            _context.SaveChanges();
            return perfil;
        }

        public List<Perfil> Listar()
        {
            return _context.Perfil.ToList();
        }

        public Perfil Recuperar(int id)
        {
            Perfil p;

            //Forma 1; LINQ
            //p = (from x in _context.Perfil
            //     where x.IdPerfil == id
            //     select x).FirstOrDefault();

            p = _context.Perfil.AsNoTracking()
                .FirstOrDefault(t => t.IdPerfil == id);


            return p;
        }
    }
}
