using Alugamer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Alugamer.Utils;

namespace Alugamer.Database
{
    public class AluguelDao : BaseDao
    {
        public List<Aluguel> Lista()
        {

            string sql = $@" SELECT cod_aluguel, cod_funcionario, cod_cliente, valor_total, data_inicial, data_final from CAD_ALUGUEL";

            DataTable resp = _conn.dataTable(sql);

            List<Aluguel> listaAluguel = new List<Aluguel>();

            foreach (DataRow linhaAluguel in resp.Rows)
            {
                Aluguel aluguel = new Aluguel
                {
                    Id = Convert.ToInt32(linhaAluguel["cod_aluguel"]),
                    Vendedor = Convert.ToInt32(linhaAluguel["cod_funcionario"]),
                    Locatario = Convert.ToInt32(linhaAluguel["cod_cliente"]),
                    Valor_total = Convert.ToInt32(linhaAluguel["Valor_total"]),
                    DataInicial = Convert.ToDateTime(linhaAluguel["data_inicial"], CultureInfo.InvariantCulture),
                    DataFinal = Convert.ToDateTime(linhaAluguel["data_final"], CultureInfo.InvariantCulture),
                };

                listaAluguel.Add(aluguel);
            }

            return listaAluguel;
        }

        public List<ItemAluguel> ListaItens(int idAluguel)
        {

            string sql = $@" SELECT cod_aluguel, cod_alugavel, qtd_alugavel, valor_total from CAD_ITEM_ALUGUEL where cod_aluguel = {idAluguel}";

            DataTable resp = _conn.dataTable(sql);

            List<ItemAluguel> listaItens = new List<ItemAluguel>();

            foreach (DataRow linhaAluguel in resp.Rows)
            {
                ItemAluguel item = new ItemAluguel
                {
                    IdAluguel = Convert.ToInt32(linhaAluguel["cod_aluguel"]),
                    IdItem = Convert.ToInt32(linhaAluguel["cod_alugavel"]),
                    Quantidade = Convert.ToInt32(linhaAluguel["qtd_alugavel"]),
                    Valor = Convert.ToDecimal(linhaAluguel["valor_total"])
                };

                listaItens.Add(item);
            }

            return listaItens;
        }

        public string Insert(Aluguel aluguel)
		{
            string sql = $@"INSERT INTO CAD_ALUGUEL (cod_funcionario, cod_cliente, valor_total, data_inicial, data_final)
                            VALUES ('{aluguel.Vendedor}', '{aluguel.Locatario}', '{aluguel.Valor_total}', '{aluguel.DataInicial.ToString("MM-dd-yyyy")}',
                                    '{aluguel.DataFinal.ToString("MM-dd-yyyy")}')";
            try
            {
                _conn.execute(sql);

                sql = $@"SELECT TOP 1 cod_aluguel FROM CAD_ALUGUEL ORDER BY data_inicial";

                int codAluguel = -1;

                DataTable resp = _conn.dataTable(sql);

                foreach (DataRow linhaAluguel in resp.Rows)
                {
                    codAluguel = Convert.ToInt32(linhaAluguel["cod_aluguel"]);
                }

                foreach (ItemAluguel item in aluguel.Itens)
                {
                    sql = $@"INSERT INTO CAD_ITEM_ALUGUEL (cod_aluguel,cod_alugavel,qtd_alugavel,valor_total)
                            VALUES ('{codAluguel}','{item.IdItem}','{item.Quantidade}','{item.Valor}')";

                    _conn.execute(sql);
                }

                return string.Empty;
            }
            catch (Exception)
            {
                return erroDatabase.GeraErroGenerico(ERRO.ERRO_GENERICO_DATABASE);
            }
        }

        public Aluguel Read(int id)
        {
            string sql = $"SELECT cod_funcionario, cod_cliente, valor_total, data_inicial, data_final FROM CAD_ALUGUEL WHERE cod_aluguel = {id}";

            DataTable tableAluguel = _conn.dataTable(sql);

            if (tableAluguel.Rows.Count == 0)
                return new Aluguel();

            return new Aluguel
            {
                Id = id,
                Vendedor = Convert.ToInt32(tableAluguel.Rows[0]["cod_funcionario"]),
                Locatario = Convert.ToInt32(tableAluguel.Rows[0]["cod_cliente"]),
                Valor_total = Convert.ToInt32(tableAluguel.Rows[0]["Valor_total"]),
                DataInicial = Convert.ToDateTime(tableAluguel.Rows[0]["data_inicial"], CultureInfo.InvariantCulture),
                DataFinal = Convert.ToDateTime(tableAluguel.Rows[0]["data_final"], CultureInfo.InvariantCulture),
            };
        }
    }
}