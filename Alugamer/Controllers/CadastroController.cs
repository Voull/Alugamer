using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Alugamer.CRUD;
using Alugamer.Models;
using Alugamer.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Alugamer.Controllers
{
	public class CadastroController : Controller
	{

		private CRUDCategoria crudCategoria;
		private Erro erro;
		private ErroDatabase erroDatabase;

		public CadastroController() : base()
		{
			crudCategoria = new CRUDCategoria();
			erro = new Erro();
			erroDatabase = new ErroDatabase();
		}

		[HttpGet]
		public IActionResult CarregaItens(int categoria)
		{
			CRUDAlugavel crudAlugavel = new CRUDAlugavel();

			List<Alugavel> listaAlugavel = crudAlugavel.ListaCompleta(categoria);

			return Ok(JsonConvert.SerializeObject(listaAlugavel));
		}

		[HttpPost]
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
	}
}