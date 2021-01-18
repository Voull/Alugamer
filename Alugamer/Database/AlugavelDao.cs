using Alugamer.Models;
using Alugamer.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Alugamer.Database
{
    public class AlugavelDao : BaseDao
    {

        public string Insert(Alugavel alugavel)
		{
            string sql = $@"INSERT INTO CAD_ALUGAVEIS (nome, descricao, quantidade, valor_compra, valor_aluguel, cod_categoria)
                            VALUES ('{alugavel.Nome}', '{alugavel.Descricao}', '{alugavel.Quantidade}', '{alugavel.Valor_compra}',
                                    '{alugavel.Valor_aluguel}' , '{alugavel.IdCategoria}')";
            try
            {
                _conn.execute(sql);
                return string.Empty;
            }
            catch (Exception)
            {
                return erroDatabase.GeraErroGenerico(ERRO.ERRO_GENERICO);
            }
        }

        public Alugavel Read(int id)
        {
            string sql = $@"SELECT cod_alugavel, nome, descricao, quantidade, valor_compra, valor_aluguel , cod_categoria
                            from CAD_ALUGAVEIS where cod_alugavel =({id})";

            DataTable resp = _conn.dataTable(sql);

            if (resp.Rows.Count == 0) return null;

            return new Alugavel
            {
                Id = id,
                Nome = Convert.ToString(resp.Rows[0]["nome"]),
                Descricao = Convert.ToString(resp.Rows[0]["descricao"]),
                Quantidade = Convert.ToInt32(resp.Rows[0]["quantidade"]),
                Valor_compra = Convert.ToDecimal(resp.Rows[0]["valor_compra"]),
                Valor_aluguel = Convert.ToDecimal(resp.Rows[0]["valor_aluguel"]),
                IdCategoria = Convert.ToInt32(resp.Rows[0]["cod_categoria"])
            };
        }

        public List<Alugavel> ReadAll()
		{
            string sql = $@"cod_alugavel, nome, descricao, quantidade, valor_compra, valor_aluguel 
                            from CAD_ALUGAVEIS";

            DataTable resp = _conn.dataTable(sql);

            List<Alugavel> listaAlugavel = new List<Alugavel>();

            foreach(DataRow linhaAlugavel in resp.Rows)
			{
                Alugavel alugavel = new Alugavel
                {
                    Id = Convert.ToInt32(linhaAlugavel["cod_alugavel"]),
                    Nome = Convert.ToString(linhaAlugavel["nome"]),
                    Descricao = Convert.ToString(linhaAlugavel["descricao"]),
                    Quantidade = Convert.ToInt32(linhaAlugavel["quantidade"]),
                    Valor_compra = Convert.ToDecimal(linhaAlugavel["valor_compra"]),
                    Valor_aluguel = Convert.ToDecimal(linhaAlugavel["data_nascimento"]),
                    IdCategoria = Convert.ToInt32(linhaAlugavel["cod_categoria"])
                };

                listaAlugavel.Add(alugavel);
			}

            return listaAlugavel;
        }

        public List<Alugavel> ReadAllSimples()
        {
            string sql = $@"SELECT cod_alugavel, nome, quantidade, valor_compra, valor_aluguel, cod_categoria
                            from CAD_ALUGAVEIS";

            DataTable resp = _conn.dataTable(sql);

            List<Alugavel> listaAlugavel = new List<Alugavel>();

            foreach (DataRow linhaAlugavel in resp.Rows)
            {
                Alugavel alugavel = new Alugavel
                {
                    Id = Convert.ToInt32(linhaAlugavel["cod_alugavel"]),
                    Nome = Convert.ToString(linhaAlugavel["nome"]),
                    Quantidade = Convert.ToInt32(linhaAlugavel["quantidade"]),
                    Valor_aluguel = Convert.ToDecimal(linhaAlugavel["valor_aluguel"]),
                    Valor_compra = Convert.ToDecimal(linhaAlugavel["valor_compra"]),
                    IdCategoria = Convert.ToInt32(linhaAlugavel["cod_categoria"])
                };

                listaAlugavel.Add(alugavel);
            }

            return listaAlugavel;
        }

        public string Update(Alugavel alugavel)
        {
            string sql = $@"UPDATE CAD_ALUGAVEIS set nome ='{alugavel.Nome}', descricao = '{alugavel.Descricao}', quantidade = '{alugavel.Quantidade}',
                            valor_compra = '{alugavel.Valor_compra}', valor_aluguel = '{alugavel.Valor_aluguel}', cod_categoria = '{alugavel.IdCategoria}' where cod_alugavel = {alugavel.Id}";

            try
            {
                _conn.execute(sql);
                return string.Empty;
            }
            catch (Exception)
            {
                return erroDatabase.GeraErroGenerico(ERRO.ERRO_GENERICO_DATABASE);
            }
        }

        public bool Delete(int id)
        {
            string sql = $@"IF EXISTS (SELECT 1 FROM CAD_ALUGAVEIS WHERE COD_ALUGAVEL = {id})
                            BEGIN
                                DELETE FROM CAD_ALUGAVEIS WHERE COD_ALUGAVEL = {id}
                                SELECT 1
                            END";

            return Convert.ToBoolean(_conn.scalar(sql));
        }

        public List<Alugavel> ReadAllMaisDados(int Categoria = 0)
        {

            string sql = $@"SELECT cod_alugavel, nome, descricao, quantidade, valor_aluguel, cod_categoria
                        from CAD_ALUGAVEIS {(Categoria == 0 ? "" : $" where cod_categoria ={Categoria}")}";

            DataTable resp = _conn.dataTable(sql);

            List<Alugavel> listaAlugavel = new List<Alugavel>();

            foreach (DataRow linhaAlugavel in resp.Rows)
            {
                Alugavel alugavel = new Alugavel
                {
                    Id = Convert.ToInt32(linhaAlugavel["cod_alugavel"]),
                    Nome = Convert.ToString(linhaAlugavel["nome"]),
                    Descricao = Convert.ToString(linhaAlugavel["descricao"]),
                    Quantidade = Convert.ToInt32(linhaAlugavel["quantidade"]),
                    Valor_aluguel = Convert.ToDecimal(linhaAlugavel["valor_aluguel"]),
                    IdCategoria = Convert.ToInt32(linhaAlugavel["cod_categoria"])
                };

                listaAlugavel.Add(alugavel);
            }

            return listaAlugavel;
        }

    }
}