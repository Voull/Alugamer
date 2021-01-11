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
	public class ClienteController : Controller
	{
		public IActionResult Index()
		{
			CRUDClientes crudClientes = new CRUDClientes();

			List<Cliente> listaClientes = crudClientes.Lista();

			ViewBag.listaClientes = listaClientes;

			return View();
		}

		[HttpGet]
		public IActionResult Busca(int id)
		{
			CRUDClientes crudClientes = new CRUDClientes();
			Cliente cliente = crudClientes.Busca(id);

			return Ok(JsonConvert.SerializeObject(cliente));
		}

		[HttpPost]
		public IActionResult Novo([FromBody] Cliente cliente)
		{
			if (cliente == null) return BadRequest(JsonConvert.SerializeObject("Dados Inválidos!"));

			CRUDClientes crudClientes = new CRUDClientes();
			string erros = crudClientes.Novo(cliente);

			if (!string.IsNullOrEmpty(erros))
            {
				return BadRequest(JsonConvert.SerializeObject(erros));
            }

			return Ok(JsonConvert.SerializeObject(""));
		}

		[HttpPost]
		public IActionResult Edita([FromBody]Cliente cliente)
		{
			if (cliente == null) return BadRequest();

			CRUDClientes crudClientes = new CRUDClientes();
			string erros = crudClientes.Edita(cliente);

			if (!string.IsNullOrEmpty(erros))
			{
				return BadRequest(JsonConvert.SerializeObject(erros));
			}

			return Ok(JsonConvert.SerializeObject(""));
		}

        [HttpDelete]
		public IActionResult Remove(int id)
		{
			CRUDClientes crudClientes = new CRUDClientes();
			crudClientes.Remove(id);

			return Ok();
		}

		//public IActionResult Index(int id)
		//{
		//	ClienteDao cliente
		//	return View();
		//}
	}
}
