using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Alugamer.Database;
using Alugamer.Models;
using Alugamer.Utils;
using Alugamer.Validations;

namespace Alugamer.CRUD
{
    public class CRUDCategoria
    {
        private CategoriaDAO categoriaDAO;
        private CategoriaValidation categoriaValidation;
        private ErroDatabase erroDatabase;

        public CRUDCategoria()
        {
            categoriaDAO = new CategoriaDAO();
            categoriaValidation = new CategoriaValidation();
            erroDatabase = new ErroDatabase();
        }

        public List<Categoria> Lista()
        {
            List<Categoria> listaCategoria = categoriaDAO.ReadAll();

            return listaCategoria;
        }

        public Categoria Busca(int id)
        {
            if (id == 0)
                return new Categoria()
                {
                    Id = 0
                };

            Categoria categoria = categoriaDAO.Read(id);

            return categoria;
        }

        public string Insere(Categoria categoria)
        {
            List<String> errosCategoria = categoriaValidation.valida(categoria);
            if (errosCategoria.Count > 0) return string.Join(Environment.NewLine, errosCategoria);

            categoriaDAO.Insert(categoria);

            return string.Empty;
        }

        public string Edita(Categoria categoria)
        {
            List<String> errosCategoria = categoriaValidation.valida(categoria);
            if (errosCategoria.Count > 0) return string.Join(Environment.NewLine, errosCategoria);

            if (!categoriaDAO.Update(categoria))
                return erroDatabase.GeraErroDatabase(ERRO_DATABASE.ERRO_NAO_EXISTE);

            return string.Empty;
        }

        public string Remove(int id)
        {

                if (!categoriaDAO.Remove(id))
                    return erroDatabase.GeraErroDatabase(ERRO_DATABASE.ERRO_DELETAR_NAO_EXISTE);
                
                return string.Empty;

        }

        public string RemoveVarios(List<int> listaId)
        {
            bool completo = true;
            foreach(int id in listaId)
            {
                try
                {
                    if (!categoriaDAO.Remove(id))
                        completo = false;
                }
                catch(SqlException ex) when (ex.Number == (int)DatabaseErrorCodes.CONFLICT)
                {
                    completo = false;
                }
            }

            if (completo)
                return string.Empty;
            else 
                return erroDatabase.GeraErroDatabase(ERRO_DATABASE.ERRO_DELETAR_MULTIPLO);
        }
    }
}
