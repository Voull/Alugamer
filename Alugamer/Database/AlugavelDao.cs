using Alugamer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Alugamer.Database
{
    public class AlugavelDao
    {
        private Conexao _conn;
        public AlugavelDao()
		{
            _conn = new Conexao();
		}

        public void Insert(Alugavel alugavel)
		{
            string sql = $@"INSERT INTO CAD_ALUGAVEIS (nome, descricao, quantidade, valor_compra, valor_aluguel)
                            VALUES ('{alugavel.Nome}', '{alugavel.Descricao}', '{alugavel.Quantidade}', '{alugavel.valor_compra}',
                                    '{alugavel.valor_aluguel}')";
            _conn.execute(sql);
        }

        public Alugavel Read(int id)
        {
            string sql = $@"SELECT cod_alugavel, nome, descricao, quantidade, valor_compra, valor_aluguel 
                            from CAD_ALUGAVEIS where cod_alugavel =({id})";

            DataTable resp = _conn.dataTable(sql);

            if (resp.Rows.Count == 0) return null;

            return new Alugavel
            {
                Id = id,
                Nome = Convert.ToString(resp.Rows[0]["nome"]),
                Descricao = Convert.ToString(resp.Rows[0]["descricao"]),
                Quantidade = Convert.ToInt32(resp.Rows[0]["quantidade"]),
                valor_compra = Convert.ToDecimal(resp.Rows[0]["valor_compra"]),
                valor_aluguel = Convert.ToDecimal(resp.Rows[0]["data_nascimento"])

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
                    valor_compra = Convert.ToDecimal(linhaAlugavel["valor_compra"]),
                    valor_aluguel = Convert.ToDecimal(linhaAlugavel["data_nascimento"])
                };

                listaAlugavel.Add(alugavel);
			}

            return listaAlugavel;
        }

        public List<Alugavel> ReadAllSimples()
        {
            string sql = $@"SELECT cod_alugavel, nome 
                            from CAD_ALUGAVEIS";

            DataTable resp = _conn.dataTable(sql);

            List<Alugavel> listaAlugavel = new List<Alugavel>();

            foreach (DataRow linhaAlugavel in resp.Rows)
            {
                Alugavel alugavel = new Alugavel
                {
                    Id = Convert.ToInt32(linhaAlugavel["cod_alugavel"]),
                    Nome = Convert.ToString(linhaAlugavel["nome"]),
                };

                listaAlugavel.Add(alugavel);
            }

            return listaAlugavel;
        }

        public void Update(Alugavel alugavel)
        {
            string sql = $@"UPDATE CAD_ALUGAVEIS set nome ='{alugavel.Nome}', descricao = '{alugavel.Descricao}', quantidade = '{alugavel.Quantidade}',
                            valor_compra = '{alugavel.valor_compra}', valor_aluguel = '{alugavel.valor_aluguel}' where cod_alugavel = {alugavel.Id}";

            _conn.execute(sql);
        }

        public void Delete(int id)
        {
            string sql = $@"DELETE FROM CAD_ALUGAVEIS WHERE cod_alugavel = {id}";
            
            _conn.execute(sql);
        }
    }
}