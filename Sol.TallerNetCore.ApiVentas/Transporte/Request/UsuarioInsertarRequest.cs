using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sol.TallerNetCore.ApiVentas.Transporte.Request
{
    public class UsuarioInsertarRequest
    {
        public string Nombres { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }


    }
}
