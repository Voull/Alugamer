using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alugamer.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Alugamer.Controllers
{
    public class ErrorController : Controller
    {
        private Erro erro;
        public ErrorController()
        {
            erro = new Erro();
        }

        [Route("/404")]
        [Route("/500")]
        public IActionResult Erro()
        {
            if (string.IsNullOrEmpty(Convert.ToString(TempData["msg"])))
                ViewBag.Msg = erro.GeraErroGenerico(ERRO.ERRO_404);
            else
                ViewBag.Msg = TempData["msg"];

            return View();
        }
    }
}
