using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Alugamer.CRUD;
using Alugamer.Database;
using Alugamer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Alugamer.Controllers
{
	public class RelatorioController : Controller
	{
		public IActionResult Index()
		{
			CRUDClientes crudClientes = new CRUDClientes();

			List<Cliente> listaClientes = crudClientes.Lista();

			ViewBag.listaClientes = listaClientes;

			return View();
		}


		[HttpGet]
		public IActionResult BuscaTodos(DateTime DataInicial, DateTime DataFinal)
		{
			CRUDRelatorio crudRelatorio = new CRUDRelatorio();

			List<Aluguel> listaAluguel = crudRelatorio.buscaTodos(DataInicial,DataFinal);

			return Ok(JsonConvert.SerializeObject(listaAluguel));
		}

		[HttpGet]
		public IActionResult BuscaCliente(DateTime DataInicial,DateTime DataFinal,int Id)
		{
			CRUDRelatorio crudRelatorio = new CRUDRelatorio();

			List<Aluguel> listaAluguel = crudRelatorio.BuscaCliente(DataInicial, DataFinal, Id);

			return Ok(JsonConvert.SerializeObject(listaAluguel));
		}

	}
}