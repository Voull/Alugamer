using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Alugamer.Auth;
using Alugamer.CRUD;
using Alugamer.Database;
using Alugamer.Models;
using Alugamer.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Alugamer.Controllers
{
	public class FuncionarioController : Controller
	{
		private CRUDFuncionario crudFuncionario;
		private CRUDUsuario crudUsuario;
		private ErroDatabase erroDatabase;
		private ErroLogin erroLogin;

		public FuncionarioController()
		{
			crudFuncionario = new CRUDFuncionario();
			crudUsuario = new CRUDUsuario();
			erroDatabase = new ErroDatabase();
			erroLogin = new ErroLogin();
		}

		[HttpGet]
		public IActionResult Index()
		{
			if (TokenService.GetUserInfo(HttpContext) == null)
				return RedirectToAction("Index", "Login");
			try
			{
				List<Funcionario> listaFuncionarios = crudFuncionario.Lista();
				ViewBag.listaFuncionarios = listaFuncionarios;

				return View(listaFuncionarios);
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
			if (TokenService.GetUserInfo(HttpContext) == null)
				return RedirectToAction("Index", "Login");

			try
			{
				Funcionario funcionario = crudFuncionario.Busca(id);
				if (funcionario.Id == -1)
				{
					Response.StatusCode = StatusCodes.Status404NotFound;
					return Content(JsonConvert.SerializeObject(erroDatabase.GeraErroDatabase(ERRO_DATABASE.ERRO_NAO_EXISTE)));
				}
				return Ok(JsonConvert.SerializeObject(funcionario));
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
			if (TokenService.GetUserInfo(HttpContext) == null)
				return RedirectToAction("Index", "Login");

			Funcionario funcionario = crudFuncionario.Busca(id);
			if (funcionario.Id == -1)
			{
				TempData["msg"] = erroDatabase.GeraErroDatabase(ERRO_DATABASE.ERRO_NAO_EXISTE);
				return RedirectToAction("Erro", "Error");
			}

			return View(funcionario);
		}

		[HttpGet]
		public IActionResult Usuario(int id)
		{
			UserInfo user = TokenService.GetUserInfo(HttpContext);

			if (user != null)
            {
				if (!user.Admin)
				{
					TempData["msg"] = erroLogin.GeraErroLogin(ERRO_LOGIN.ERRO_PERMISSAO);
					return RedirectToAction("Erro", "Error");
				}
			}
            else
				return RedirectToAction("Index", "Login");
			



			UserInfo info = crudUsuario.BuscaUsuario(id);
			if (info.CodFuncionario == -1)
			{
				TempData["msg"] = erroDatabase.GeraErroDatabase(ERRO_DATABASE.ERRO_NAO_EXISTE);
				return RedirectToAction("Erro", "Error");
			}

			return View(info);
		}


		[HttpPost]
		[Authorize(Roles = "ADMIN")]
		public IActionResult SalvaUsuario(UserInfo perfil, string senhaNova)
		{
			try
			{
				string erros = crudUsuario.SalvaUsuario(perfil, senhaNova);
				if (!string.IsNullOrEmpty(erros))
					return BadRequest(JsonConvert.SerializeObject(erros));

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
		[Authorize]
		public IActionResult Novo([FromBody] Funcionario funcionario)
		{
			try
			{
				if (funcionario == null) return BadRequest(JsonConvert.SerializeObject("Dados Inválidos!"));

				string erros = crudFuncionario.Insere(funcionario);
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
		[Authorize]
		public IActionResult Edita([FromBody] Funcionario funcionario)
		{
			try
			{
				if (funcionario == null) return BadRequest("Dados Inválidos!");

				string erros = crudFuncionario.Edita(funcionario);
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
		[Authorize]
		public IActionResult Remove(int id)
		{
			try
			{
				string erros = crudFuncionario.Remove(id);
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
		[Authorize]
		public IActionResult DeleteGrupo([FromBody] List<int> listaId)
		{
			try
			{
				string erros = crudFuncionario.Remove(listaId);
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
