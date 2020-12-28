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
	public class AlugavelController : Controller
	{
		public IActionResult Index()
		{
			AlugavelDao alugavelDao = new AlugavelDao();

			List<Alugavel> listaAlugaveis = alugavelDao.ReadAllSimples();

			ViewBag.listaAlugaveis = listaAlugaveis;

			return View();
		}

		[HttpGet]
		public IActionResult Busca(int id)
		{
			AlugavelDao alugavelDao = new AlugavelDao();

			Alugavel alugavel = alugavelDao.Read(id);

			return Ok(JsonConvert.SerializeObject(alugavel));
		}

		[HttpPost]
		public IActionResult Novo([FromBody] Alugavel alugavel)
		{
			AlugavelDao alugavelDao = new AlugavelDao();
			alugavelDao.Insert(alugavel);

			return Ok();
		}

		[HttpPost]
		public IActionResult Edita([FromBody] Alugavel alugavel)
		{
			AlugavelDao alugavelDao = new AlugavelDao();
			alugavelDao.Update(alugavel);

			return Ok();
		}

		[HttpDelete]
		public IActionResult Remove(int id)
		{
			AlugavelDao alugavelDao = new AlugavelDao();
			alugavelDao.Delete(id);

			return Ok();
		}
	}
}