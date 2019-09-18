using Microsoft.EntityFrameworkCore;
using Sol.TallerNetCore.ApiVentas.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sol.TallerNetCore.ApiVentas.Contexto
{
    public class BDPedidosContext : DbContext
    {
        public BDPedidosContext
            (DbContextOptions<BDPedidosContext> opcions) 
            : base(opcions)
        {

        }

        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    }
}
