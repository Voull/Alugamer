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
	public class AlugavelController : Controller
	{
		public IActionResult Index()
		{
			CRUDAlugavel crudAlugavel = new CRUDAlugavel();

			List<Alugavel> listaAlugavel = crudAlugavel.Lista();

			ViewBag.listaAlugavel = listaAlugavel;

			return View();
		}

		[HttpGet]
		public IActionResult Busca(int id)
		{
			CRUDAlugavel crudAlugavel = new CRUDAlugavel();
			Alugavel alugavel = crudAlugavel.Busca(id);

			return Ok(JsonConvert.SerializeObject(alugavel));
		}

		[HttpPost]
		public IActionResult Novo([FromBody] Alugavel alugavel)
		{
			if (alugavel == null) return BadRequest(JsonConvert.SerializeObject("Dados Inválidos!"));

			CRUDAlugavel crudAlugavel = new CRUDAlugavel();
			string erros = crudAlugavel.Novo(alugavel);

			if (!string.IsNullOrEmpty(erros))
			{
				return BadRequest(JsonConvert.SerializeObject(erros));
			}

			return Ok(JsonConvert.SerializeObject(""));
		}

		[HttpPost]
		public IActionResult Edita([FromBody] Alugavel alugavel)
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

		[HttpDelete]
		public IActionResult Remove(int id)
		{
			CRUDAlugavel crudAlugavel = new CRUDAlugavel();
			crudAlugavel.Remove(id);

			return Ok();
		}

		//public IActionResult Index(int id)
		//{
		//	AlugavelDao cliente
		//	return View();
		//}
	}
}