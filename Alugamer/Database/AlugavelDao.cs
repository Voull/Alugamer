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
        private Conexao _conn;
        public AlugavelDao()
		{
            _conn = new Conexao();
		}

        public string Insert(Alugavel alugavel)
		{
            string sql = $@"INSERT INTO CAD_ALUGAVEIS (nome, descricao, quantidade, valor_compra, valor_aluguel, cod_categoria)
                            VALUES ('{alugavel.Nome}', '{alugavel.Descricao}', {alugavel.Quantidade}, {alugavel.Valor_compra.ToString("0.00", CultureInfo.InvariantCulture)},
                                    {alugavel.Valor_aluguel.ToString("0.00", CultureInfo.InvariantCulture)} , {alugavel.IdCategoria})";
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

        public string Update(Alugavel alugavel)
        {
            string sql = $@"UPDATE CAD_ALUGAVEIS set nome ='{alugavel.Nome}', descricao = '{alugavel.Descricao}', quantidade = {alugavel.Quantidade},
                            valor_compra = {alugavel.Valor_compra.ToString("0.00", CultureInfo.InvariantCulture)}, valor_aluguel = {alugavel.Valor_aluguel.ToString("0.00", CultureInfo.InvariantCulture)}, cod_categoria = {alugavel.IdCategoria} where cod_alugavel = {alugavel.Id}";

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

        public void Delete(int id)
        {
            string sql = $@"DELETE FROM CAD_ALUGAVEIS WHERE cod_alugavel = {id}";
            
            _conn.execute(sql);
        }
    }
}