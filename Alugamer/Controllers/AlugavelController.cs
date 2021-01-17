using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Alugamer.CRUD;
using Alugamer.Database;
using Alugamer.Models;
using Alugamer.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Alugamer.Controllers
{
	public class AlugavelController : Controller
	{
		private CRUDAlugavel crudAlugavel;
		private CRUDCategoria crudCategoria;
		private ErroDatabase erroDatabase;

		public AlugavelController()
		{
			crudAlugavel = new CRUDAlugavel();
			crudCategoria = new CRUDCategoria();
			erroDatabase = new ErroDatabase();
		}

		public IActionResult Index()
		{
            try
            {
				List<Alugavel> listaAlugavel = crudAlugavel.Lista();
				List<Categoria> listaCategoria = crudCategoria.ListaSimples();

				ViewBag.ListaCategoria = listaCategoria;

				return View(listaAlugavel);
			}
            catch (Exception)
            {
				TempData["msg"] = erroDatabase.GeraErroGenerico(ERRO.ERRO_GENERICO);
				return RedirectToAction("Erro", "Error");
			}
		}

		public IActionResult Cadastro(int id)
        {
            try
            {
				Alugavel alugavel = crudAlugavel.Busca(id);
				List<Categoria> listaCategoria = crudCategoria.ListaSimples();

				ViewBag.ListaCategoria = listaCategoria;

				return View(alugavel);
			}
			catch (Exception)
			{
				TempData["msg"] = erroDatabase.GeraErroGenerico(ERRO.ERRO_GENERICO);
				return RedirectToAction("Erro", "Error");
			}


		}

		[HttpGet]
		public IActionResult Busca(int id)
		{
            try
            {
				Alugavel alugavel = crudAlugavel.Busca(id);
				if (alugavel.Id == -1)
					return NotFound();

				return Ok(JsonConvert.SerializeObject(alugavel));
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

		[HttpPost]
		public IActionResult Novo([FromBody] Alugavel alugavel)
		{
            try
            {
				if (alugavel == null) return BadRequest(JsonConvert.SerializeObject("Dados Inválidos!"));

				string erros = crudAlugavel.Novo(alugavel);

				if (!string.IsNullOrEmpty(erros))
				{
					return BadRequest(JsonConvert.SerializeObject(erros));
				}

				return NoContent();
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

		[HttpPost]
		public IActionResult Edita([FromBody] Alugavel alugavel)
		{
            try
            {
				if (alugavel == null) return BadRequest();

				CRUDAlugavel crudAlugavel = new CRUDAlugavel();
				string erros = crudAlugavel.Edita(alugavel);

				if (!string.IsNullOrEmpty(erros))
				{
					return BadRequest(JsonConvert.SerializeObject(erros));
				}

				return Ok(JsonConvert.SerializeObject(""));
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
		public IActionResult Remove(int id)
		{
            try
            {
				string erros = crudAlugavel.Remove(id);
				if (string.IsNullOrEmpty(erros))
				{
					Response.StatusCode = StatusCodes.Status410Gone;
					return Json(erros);
				}

				return NoContent();

			}
			catch (SqlException ex) when (ex.Number == (int)DatabaseErrorCodes.CONFLICT)
			{
				Response.StatusCode = StatusCodes.Status409Conflict;
				return Json(erroDatabase.GeraErroDatabase(ERRO_DATABASE.ERRO_DELETAR_CONFLITO));
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
		public IActionResult RemoveGrupo(List<int> listaId)
		{
			try
			{
				string erros = crudAlugavel.Remove(listaId);
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

		//public IActionResult Index(int id)
		//{
		//	AlugavelDao cliente
		//	return View();
		//}
	}
}