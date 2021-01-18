using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Alugamer.Auth;
using Alugamer.CRUD;
using Alugamer.Models;
using Alugamer.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Alugamer.Controllers
{
	public class AluguelController : Controller
	{

		private CRUDCategoria crudCategoria;
		private CRUDClientes crudClientes;
		private CRUDAlugavel crudAlugavel;
		private CRUDAluguel crudAluguel;
		private Erro erro;
		private ErroDatabase erroDatabase;
		private CRUDFuncionario crudFuncionario;

		public AluguelController() : base()
		{
			crudCategoria = new CRUDCategoria();
			erro = new Erro();
			erroDatabase = new ErroDatabase();
			crudAlugavel = new CRUDAlugavel();
			crudClientes = new CRUDClientes();
			crudAluguel = new CRUDAluguel();
			crudFuncionario = new CRUDFuncionario();
		}

		[HttpGet]
		public IActionResult Cadastro(int id)
		{
			UserInfo info = TokenService.GetUserInfo(HttpContext);

			if (info == null)
				return RedirectToAction("Index", "Login");

			List<Categoria> listaCategorias = crudCategoria.Lista();
			ViewBag.listaCategorias = listaCategorias;

			List<Cliente> listaClientes = crudClientes.Lista();
			ViewBag.listaClientes = listaClientes;
			
			List<Alugavel> listaAlugaveis = crudAlugavel.ListaCompleta();
			ViewBag.listaAlugavel = listaAlugaveis;

			Aluguel aluguel = crudAluguel.Busca(id);
			if (aluguel.Id == -1)
			{
				TempData["msg"] = erroDatabase.GeraErroDatabase(ERRO_DATABASE.ERRO_NAO_EXISTE);
				return RedirectToAction("Erro", "Error");
			}

			if (id == 0)
			{
				ViewBag.codFuncionario = info.CodFuncionario;
				ViewBag.NomeFuncionario = info.NomeFuncionario;
			}
            else
            {
				Funcionario vendedor = crudFuncionario.Busca(aluguel.Vendedor);

				ViewBag.CodFuncionario = aluguel.Vendedor;
				ViewBag.NomeFuncionario = vendedor.Nome;
			}

			return View(aluguel);
		}

		[HttpGet]
		public IActionResult Finalizacao(int id)
		{
			UserInfo info = TokenService.GetUserInfo(HttpContext);

			if (info == null)
				return RedirectToAction("Index", "Login");


			List<Alugavel> listaAlugaveis = crudAlugavel.ListaCompleta();
			ViewBag.listaAlugavel = listaAlugaveis;

			Aluguel aluguel = crudAluguel.Busca(id);
			if (aluguel.Id == -1)
			{
				TempData["msg"] = erroDatabase.GeraErroDatabase(ERRO_DATABASE.ERRO_NAO_EXISTE);
				return RedirectToAction("Erro", "Error");
			}

			Cliente cliente = crudClientes.Busca(aluguel.Locatario);
			ViewBag.NomeCliente = cliente.Nome;

			Funcionario vendedor = crudFuncionario.Busca(aluguel.Vendedor);

            ViewBag.CodFuncionario = aluguel.Vendedor;
            ViewBag.NomeFuncionario = vendedor.Nome;

            return View(aluguel);
		}


		[HttpGet]
		public IActionResult Index()
		{
			if (TokenService.GetUserInfo(HttpContext) == null)
				return RedirectToAction("Index", "Login");

			CRUDCategoria crudCategoria = new CRUDCategoria();

			List<Categoria> listaCategorias = crudCategoria.Lista();

			ViewBag.listaCategorias = listaCategorias;

			CRUDClientes crudClientes = new CRUDClientes();

			List<Cliente> listaClientes = crudClientes.Lista();

			ViewBag.listaClientes = listaClientes;

			CRUDAluguel crudAluguel = new CRUDAluguel();

			try
			{
				List<Aluguel> listaaluguel = crudAluguel.Lista();

				return View(listaaluguel);
			}
			catch (Exception)
			{
				Response.StatusCode = StatusCodes.Status500InternalServerError;
				return Json(erro.GeraErroGenerico(ERRO.ERRO_GENERICO));
			}
		}

		[HttpGet]
		[Authorize]
		public IActionResult CarregaItens(int categoria)
		{
			CRUDAlugavel crudAlugavel = new CRUDAlugavel();

			List<Alugavel> listaAlugavel = crudAlugavel.ListaCompleta(categoria);

			return Ok(JsonConvert.SerializeObject(listaAlugavel));
		}

		[HttpPost]
		[Authorize]
		public IActionResult Novo([FromBody] Aluguel aluguel)
		{
			if (aluguel == null) return BadRequest(JsonConvert.SerializeObject("Dados Inválidos!"));

			CRUDAluguel crudAluguel = new CRUDAluguel();
			string erros = crudAluguel.Novo(aluguel);

            if (!string.IsNullOrEmpty(erros))
            {
                return BadRequest(JsonConvert.SerializeObject(erros));
            }

            return Ok(JsonConvert.SerializeObject(""));
		}

		[HttpPost]
		[Authorize]
		public IActionResult FinalizaAluguel([FromBody] Aluguel aluguel)
		{
			if (aluguel == null) return BadRequest(JsonConvert.SerializeObject("Dados Inválidos!"));

			CRUDAluguel crudAluguel = new CRUDAluguel();
			crudAluguel.Finaliza(aluguel);

			return Ok(JsonConvert.SerializeObject(""));
		}

	}
}