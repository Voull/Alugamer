using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alugamer.Database;
using Alugamer.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Alugamer.Controllers
{
	public class ClienteController : Controller
	{
		public IActionResult Index()
		{
			ClienteDao clienteDao = new ClienteDao();

			List<Cliente> listaClientes = clienteDao.ReadAllSimples();

			ViewBag.listaClientes = listaClientes;

			return View();
		}

		[HttpGet]
		public IActionResult Busca(int id)
		{
			ClienteDao clienteDao = new ClienteDao();

			Cliente cliente = clienteDao.Read(id);

			return Ok();
		}

		[HttpPost]
		public IActionResult Novo([FromBody] Cliente cliente)
		{
			if (cliente == null) return BadRequest();

			List<String> errosCliente = cliente.validar();
			if (errosCliente.Count > 0) return BadRequest(JsonConvert.SerializeObject(errosCliente));

			ClienteDao clienteDao = new ClienteDao();
			string resposta = clienteDao.Insert(cliente);

			return Ok(resposta);
		}

		[HttpPost]
		public IActionResult Edita([FromBody]Cliente cliente)
		{
			if (cliente == null) return BadRequest();

			List<String> errosCliente = cliente.validar();
			if (errosCliente.Count > 0) return BadRequest(JsonConvert.SerializeObject(errosCliente));

			ClienteDao clienteDao = new ClienteDao();
			string resposta = clienteDao.Update(cliente);

			return Ok(resposta);
		}

		[HttpDelete]
		public IActionResult Remove(int id)
		{
			ClienteDao clienteDao = new ClienteDao();
			clienteDao.Delete(id);

			return Ok();
		}

		//public IActionResult Index(int id)
		//{
		//	ClienteDao cliente
		//	return View();
		//}
	}
}
