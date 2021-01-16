using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Alugamer.CRUD;
using Alugamer.Models;
using Alugamer.Utils;
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
            Categoria categoria = crudCategoria.Busca(id);
            if (categoria.Id == -1)
            {
                TempData["msg"] = erroDatabase.GeraErroDatabase(ERRO_DATABASE.ERRO_NAO_EXISTE);
                return RedirectToAction("Erro404", "Error");
            }
                
            return View(categoria);
        }

        [HttpPost]
        public IActionResult Novo(Categoria categoria)
        {
            try
            {
                string erros = crudCategoria.Insere(categoria);
                if (string.IsNullOrEmpty(erros))
                    return NoContent();
                return BadRequest(erros);
            }
            catch (SqlException)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return Json(erro.GeraErroGenerico(ERRO.ERRO_GENERICO_DATABASE));
            }
            catch (Exception){
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return Json(erro.GeraErroGenerico(ERRO.ERRO_GENERICO));
            }

        }
        [HttpGet]
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
        public IActionResult Busca(int id)
        {
            try
            {
                Categoria categoria = crudCategoria.Busca(id);
                if (categoria.Id == -1)
                    return NotFound();
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

        public IActionResult Delete(int id)
        {
            try
            {
                string erros = crudCategoria.Remove(id);
                if (string.IsNullOrEmpty(erros))
                {
                    Response.StatusCode = StatusCodes.Status410Gone;
                    return Content(erros);
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
        public IActionResult DeleteGrupo([FromBody]List<int> listaId)
        {
            try
            {
                string erros = crudCategoria.RemoveVarios(listaId);
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
