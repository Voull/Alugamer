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
	public class FuncionarioController : Controller
	{
		public IActionResult Index()
		{
			CRUDFuncionario crudFuncionario = new CRUDFuncionario();

			List<Funcionario> listaFuncionario = crudFuncionario.Lista();

			ViewBag.listaFuncionario = listaFuncionario;

			return View();
		}

		[HttpGet]
		public IActionResult Busca(int id)
		{
			CRUDFuncionario crudFuncionario = new CRUDFuncionario();
			Funcionario funcionario = crudFuncionario.Busca(id);

			return Ok(JsonConvert.SerializeObject(funcionario));
		}

		[HttpPost]
		public IActionResult Novo([FromBody] Funcionario funcionario)
		{
			if (funcionario == null) return BadRequest("Dados Inválidos!");

			CRUDFuncionario crudFuncionario = new CRUDFuncionario();
			string erros = crudFuncionario.Novo(funcionario);

			if (!string.IsNullOrEmpty(erros))
            {
				return BadRequest(JsonConvert.SerializeObject(erros));
            }

			return Ok(JsonConvert.SerializeObject(erros));
		}

		[HttpPost]
		public IActionResult Edita([FromBody] Funcionario funcionario)
		{
			if (funcionario == null) return BadRequest();

			CRUDFuncionario crudFuncionario = new CRUDFuncionario();
			string erros = crudFuncionario.Edita(funcionario);

			if (!string.IsNullOrEmpty(erros))
			{
				return BadRequest(erros);
			}

			return Ok(erros);
		}

        [HttpDelete]
		public IActionResult Remove(int id)
		{
			CRUDFuncionario crudFuncionario = new CRUDFuncionario();
			crudFuncionario.Remove(id);

			return Ok();
		}

		//public IActionResult Index(int id)
		//{
		//	ClienteDao cliente
		//	return View();
		//}
	}
}
