using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Alugamer.Auth;
using Alugamer.CRUD;
using Alugamer.Models;
using Alugamer.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Alugamer.Controllers
{
    public class UsuarioController : Controller
    {
        private CRUDUsuario crudUsuario;
        private Erro erro;

        public UsuarioController()
        {
            crudUsuario = new CRUDUsuario();
            erro = new Erro();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Perfil()
        {
            UserInfo info = TokenService.GetUserInfo(HttpContext);
            if (info == null)
                return RedirectToAction("Index", "Login");

            return View("Index", info);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Salva(UserInfo perfil, string senhaAtual, string senhaNova)
        {
            try
            {

                string erros = crudUsuario.SalvaPerfil(TokenService.GetUserInfo(HttpContext), perfil, senhaAtual, senhaNova);
                if (!string.IsNullOrEmpty(erros))
                    return BadRequest(JsonConvert.SerializeObject(erros));

                return NoContent();
            }
            catch (SqlException)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return Json(erro.GeraErroGenerico(ERRO.ERRO_GENERICO_DATABASE));
            }
            catch (Exception)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return Json(erro.GeraErroGenerico(ERRO.ERRO_GENERICO));
            }
        }
    }
}
