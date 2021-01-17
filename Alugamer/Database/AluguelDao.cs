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
        private Conexao _conn;
        public AluguelDao()
		{
            _conn = new Conexao();
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
    }
}