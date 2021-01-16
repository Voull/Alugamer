using System;
using System.Collections.Generic;
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
    public class CategoriaController : Controller
    {
        private CRUDCategoria crudCategoria;
        private Erro erro;
        private ErroDatabase erroDatabase;

        public CategoriaController() : base()
        {
            crudCategoria = new CRUDCategoria();
            erro = new Erro();
            erroDatabase = new ErroDatabase();
        }
        public IActionResult Index()
        {
            try
            {
                List<Categoria> listaCategoria = crudCategoria.Lista();
                
                return View(listaCategoria);
            }
            catch (Exception)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return Content(erro.GeraErroGenerico(Erro.ERRO.ERRO_GENERICO));
            }
        }

        public IActionResult Cadastro(int id)
        {
            //Categoria categoria = crudCategoria.Busca(id);
            //if(categoria.Id == -1)
            //    return 

            return View(/*categoria*/);
        }

        public IActionResult Lista()
        {
            try
            {
                List<Categoria> listaCategoria = crudCategoria.Lista();
                return Ok(JsonConvert.SerializeObject(listaCategoria));
            }
            catch (Exception)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return Content(erro.GeraErroGenerico(Erro.ERRO.ERRO_GENERICO));
            }
        }

        public IActionResult Busca(int id)
        {
            try
            {
                Categoria categoria = crudCategoria.Busca(id);
                if (categoria.Id == -1)
                    return NotFound();
                return Ok(JsonConvert.SerializeObject(categoria));
            }
            catch (Exception)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return Content(erro.GeraErroGenerico(Erro.ERRO.ERRO_GENERICO));
            }
        }
    }
}
