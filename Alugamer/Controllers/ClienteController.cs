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
	public class ClienteController : Controller
	{
		private CRUDClientes crudClientes;
		private ErroDatabase erroDatabase;

		public ClienteController()
        {
			crudClientes = new CRUDClientes();
			erroDatabase = new ErroDatabase();
		}

		public IActionResult Index()
		{
            try
            {
				List<Cliente> listaClientes = crudClientes.Lista();
				ViewBag.listaClientes = listaClientes;

				return View(listaClientes);
			}
			catch (SqlException)
			{
				TempData["msg"] = erroDatabase.GeraErroGenerico(ERRO.ERRO_GENERICO_DATABASE);
				return RedirectToAction("Erro", "Error");
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
				Cliente cliente = crudClientes.Busca(id);
				if (cliente.Id == -1)
				{
					Response.StatusCode = StatusCodes.Status404NotFound;
					return Content(JsonConvert.SerializeObject(erroDatabase.GeraErroDatabase(ERRO_DATABASE.ERRO_NAO_EXISTE)));
				}
				return Ok(JsonConvert.SerializeObject(cliente));
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

		[HttpGet]
		public IActionResult Cadastro(int id)
		{
			Cliente cliente = crudClientes.Busca(id);
			if (cliente.Id == -1)
			{
				TempData["msg"] = erroDatabase.GeraErroDatabase(ERRO_DATABASE.ERRO_NAO_EXISTE);
				return RedirectToAction("Erro", "Error");
			}

			return View(cliente);
		}

		[HttpPost]
		public IActionResult Novo([FromBody] Cliente cliente)
		{
			try
			{
				if (cliente == null) return BadRequest(JsonConvert.SerializeObject("Dados Inválidos!"));

				string erros = crudClientes.Insere(cliente);
				if (string.IsNullOrEmpty(erros))
					return NoContent();
				return BadRequest(JsonConvert.SerializeObject(erros));
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
		public IActionResult Edita([FromBody]Cliente cliente)
		{
			try
			{
				if (cliente == null) return BadRequest("Dados Inválidos!");

				string erros = crudClientes.Edita(cliente);
				if (string.IsNullOrEmpty(erros))
					return NoContent();
				return BadRequest(JsonConvert.SerializeObject(erros));
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
				string erros = crudClientes.Remove(id);
				if (string.IsNullOrEmpty(erros))
				{
					Response.StatusCode = StatusCodes.Status410Gone;
					return Json(erros);
				}
				else
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
		public IActionResult DeleteGrupo([FromBody] List<int> listaId)
		{
			try
			{
				string erros = crudClientes.Remove(listaId);
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
		//	ClienteDao cliente
		//	return View();
		//}
	}
}
