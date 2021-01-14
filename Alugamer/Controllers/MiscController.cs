using Alugamer.Database;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alugamer.Controllers
{
    public class MiscController : Controller
    {
        [HttpPost]
        [Route("/Reset")]
        public IActionResult Reset()
        {
            TesteDAO testeDAO = new TesteDAO();
            testeDAO.InicializaBDTeste();

            return NoContent();
        }
    }
}
