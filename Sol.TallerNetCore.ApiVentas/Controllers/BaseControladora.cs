using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sol.TallerNetCore.ApiVentas.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class BaseControladora : Controller
    {

    }
}
