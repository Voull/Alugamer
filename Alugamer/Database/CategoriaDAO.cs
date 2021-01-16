using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Alugamer.Models;

namespace Alugamer.Database
{
    public class CategoriaDAO : BaseDao
    {
        private Conexao _conn;
        public CategoriaDAO()
        {
            _conn = new Conexao();
        }

        public List<Categoria> ReadAll()
        {
            string sql = "SELECT COD_CATEGORIA, NOME, DESCRICAO FROM CAD_CATEGORIAS";

            DataTable tableCategoria = _conn.dataTable(sql);
            List<Categoria> listaCategoria = new List<Categoria>();

            foreach(DataRow rowCategoria in tableCategoria.Rows)
            {
                listaCategoria.Add(new Categoria
                {
                    Id = Convert.ToInt32(rowCategoria["COD_CATEGORIA"]),
                    Nome = Convert.ToString(rowCategoria["NOME"]),
                    Descricao = Convert.ToString(rowCategoria["DESCRICAO"])
                });
            }

            return listaCategoria;
        }

        public List<Categoria> ReadAllSimples()
        {
            string sql = "SELECT COD_CATEGORIA, NOME FROM CAD_CATEGORIAS";

            DataTable tableCategoria = _conn.dataTable(sql);
            List<Categoria> listaCategoria = new List<Categoria>();

            foreach (DataRow rowCategoria in tableCategoria.Rows)
            {
                listaCategoria.Add(new Categoria
                {
                    Id = Convert.ToInt32(rowCategoria["COD_CATEGORIA"]),
                    Nome = Convert.ToString(rowCategoria["NOME"]),
                });
            }

            return listaCategoria;
        }

        public Categoria Read(int id)
        {
            string sql = $"SELECT NOME, DESCRICAO FROM CAD_CATEGORIAS WHERE COD_CATEGORIA = {id}";

            DataTable tableCategoria = _conn.dataTable(sql);

            if (tableCategoria.Rows.Count == 0)
                return new Categoria();

            return new Categoria
            {
                Id = id,
                Nome = Convert.ToString(tableCategoria.Rows[0]["NOME"]),
                Descricao = Convert.ToString(tableCategoria.Rows[0]["DESCRICAO"])
            };
        }

        public void Insert(Categoria categoria)
        {
            string sql = $"INSERT INTO CAD_CATEGORIAS (NOME, DESCRICAO) VALUES ('{categoria.Nome}', '{categoria.Descricao}')";

            _conn.execute(sql);
        }

        public bool Update(Categoria categoria)
        {
            string sql = $@"IF EXISTS (SELECT 1 FROM CAD_CATEGORIAS WHERE COD_CATEGORIA = {categoria.Id})
                            BEGIN
                                UPDATE CAD_CATEGORIAS SET NOME = '{categoria.Nome}', DESCRICAO = '{categoria.Descricao}' WHERE COD_CATEGORIA = {categoria.Id}
                                SELECT 1
                            END";

            return Convert.ToBoolean(_conn.scalar(sql));
        }

        public bool Remove(int id)
        {
            string sql = $@"IF EXISTS (SELECT 1 FROM CAD_CATEGORIAS WHERE COD_CATEGORIA = {id})
                            BEGIN
                                DELETE FROM CAD_CATEGORIAS WHERE COD_CATEGORIA = {id}
                                SELECT 1
                            END";

            return Convert.ToBoolean(_conn.scalar(sql));
        }
    }
}
