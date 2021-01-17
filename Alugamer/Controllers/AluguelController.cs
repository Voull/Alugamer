﻿using System;
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
	public class AluguelController : Controller
	{
		public IActionResult Index()
		{

			CRUDCategoria crudCategoria = new CRUDCategoria();

			List<Categoria> listaCategorias = crudCategoria.Lista();

			ViewBag.listaCategorias = listaCategorias;

			CRUDClientes crudClientes = new CRUDClientes();

			List<Cliente> listaClientes = crudClientes.Lista();

			ViewBag.listaClientes = listaClientes;

			return View();
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