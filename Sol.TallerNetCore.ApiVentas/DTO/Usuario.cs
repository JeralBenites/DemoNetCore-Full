using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Sol.TallerNetCore.ApiVentas.DTO
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public string Nombres { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public string Imagen { get; set; }


    }
}
