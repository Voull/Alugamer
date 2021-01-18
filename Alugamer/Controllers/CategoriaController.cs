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
    public class CategoriaController : Controller
    {
        private CRUDCategoria crudCategoria;
        private Erro erro;
        private ErroDatabase erroDatabase;

        public CategoriaController() : base()
        {
            crudCategoria = new CRUDCategoria();
            erro = new Erro();
            erroDatabase = new ErroDatabase();
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (TokenService.GetUserInfo(HttpContext) == null)
                return RedirectToAction("Index", "Login");

            try
            {
                List<Categoria> listaCategoria = crudCategoria.Lista();

                return View(listaCategoria);
            }
            catch (Exception)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return Json(erro.GeraErroGenerico(ERRO.ERRO_GENERICO));
            }
        }

        [HttpGet]
        public IActionResult Cadastro(int id)
        {
            if (TokenService.GetUserInfo(HttpContext) == null)
                return RedirectToAction("Index", "Login");

            Categoria categoria = crudCategoria.Busca(id);
            if (categoria.Id == -1)
            {
                TempData["msg"] = erroDatabase.GeraErroDatabase(ERRO_DATABASE.ERRO_NAO_EXISTE);
                return RedirectToAction("Erro", "Error");
            }

            return View(categoria);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Novo([FromBody] Categoria categoria)
        {
            try
            {
                string erros = crudCategoria.Insere(categoria);
                if (string.IsNullOrEmpty(erros))
                    return NoContent();
                return BadRequest(JsonConvert.SerializeObject(erros));
            }
            catch (SqlException)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return Json(erro.GeraErroGenerico(ERRO.ERRO_GENERICO_DATABASE));
            }
            catch (Exception) {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return Json(erro.GeraErroGenerico(ERRO.ERRO_GENERICO));
            }

        }

        [HttpPost]
        [Authorize]
        public IActionResult Edita([FromBody] Categoria categoria)
        {
            try
            {
                string erros = crudCategoria.Edita(categoria);
                if (string.IsNullOrEmpty(erros))
                    return NoContent();
                return BadRequest(JsonConvert.SerializeObject(erros));
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

        [HttpGet]
        [Authorize]
        public IActionResult Lista()
        {
            try
            {
                List<Categoria> listaCategoria = crudCategoria.Lista();
                return Accepted(JsonConvert.SerializeObject(listaCategoria));
            }
            catch (Exception)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return Json(erro.GeraErroGenerico(ERRO.ERRO_GENERICO));
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult Busca(int id)
        {
            try
            {
                Categoria categoria = crudCategoria.Busca(id);
                if (categoria.Id == -1)
                {
                    Response.StatusCode = StatusCodes.Status404NotFound;
                    return Content(JsonConvert.SerializeObject(erroDatabase.GeraErroDatabase(ERRO_DATABASE.ERRO_NAO_EXISTE)));
                }
                return Ok(JsonConvert.SerializeObject(categoria));
            }
            catch (SqlException)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return Json(erroDatabase.GeraErroGenerico(ERRO.ERRO_GENERICO_DATABASE));
            }
            catch (Exception)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return Json(erroDatabase.GeraErroGenerico(ERRO.ERRO_GENERICO));
            }
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete(int id)
        {
            try
            {
                string erros = crudCategoria.Remove(id);
                if (string.IsNullOrEmpty(erros))
                {
                    Response.StatusCode = StatusCodes.Status410Gone;
                    return Json(erros);
                }
                else
                    return NoContent();
            }
            catch (SqlException ex) when (ex.Number == (int) DatabaseErrorCodes.CONFLICT)
            {
                Response.StatusCode = StatusCodes.Status409Conflict;
                return Json(erroDatabase.GeraErroDatabase(ERRO_DATABASE.ERRO_DELETAR_CONFLITO));
            }
            catch(SqlException)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return Json(erroDatabase.GeraErroGenerico(ERRO.ERRO_GENERICO_DATABASE));
            }
            catch (Exception)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return Json(erroDatabase.GeraErroGenerico(ERRO.ERRO_GENERICO));
            }
        }

        [HttpDelete]
        [Authorize]
        public IActionResult DeleteGrupo([FromBody]List<int> listaId)
        {
            try
            {
                string erros = crudCategoria.Remove(listaId);
                if (string.IsNullOrEmpty(erros))
                    return NoContent();
                else
                    return Ok(JsonConvert.SerializeObject(erros));
            }
            catch (SqlException)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return Json(erroDatabase.GeraErroGenerico(ERRO.ERRO_GENERICO_DATABASE));
            }
            catch (Exception)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return Json(erroDatabase.GeraErroGenerico(ERRO.ERRO_GENERICO));
            }
        }

    }
}
