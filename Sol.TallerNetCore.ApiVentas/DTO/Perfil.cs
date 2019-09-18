using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Sol.TallerNetCore.ApiVentas.DTO
{
    public class Perfil
    {
        [Key]
        public int IdPerfil { get; set; }
        public string NombrePerfil { get; set; }

    }
}
