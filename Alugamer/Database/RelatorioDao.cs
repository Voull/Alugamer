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
    public class RelatorioDao : BaseDao
    {

        public List<RelatorioRow> ReadAll(DateTime dataIni, DateTime dataFim)
        {
            string sql = $@"SELECT ca.cod_aluguel, cf.nome as nomeFunc, cc.nome as nomeCli, ca.valor_total, ca.data_inicial, ca.data_final, ca.data_devolucao, ca.valor_desconto, ca.valor_multa
                            from CAD_ALUGUEL ca left join cad_funcionarios cf on ca.cod_funcionario = cf.cod_funcionario left join cad_clientes cc on cc.cod_cliente = ca.cod_cliente 
                            WHERE data_devolucao BETWEEN '{dataIni.ToString("MM-dd-yyyy")}' AND '{dataFim.ToString("MM-dd-yyyy")}'";

            DataTable resp = _conn.dataTable(sql);

            List<RelatorioRow> listaAluguel = new List<RelatorioRow>();

            foreach (DataRow linhaAluguel in resp.Rows)
            {
                RelatorioRow aluguel = new RelatorioRow
                {
                    Id = Convert.ToInt32(linhaAluguel["cod_aluguel"]),
                    Vendedor = Convert.ToString(linhaAluguel["nomeFunc"]),
                    Locatario = Convert.ToString(linhaAluguel["nomeCli"]),
                    Valor_total = Convert.ToDecimal(linhaAluguel["valor_total"]),
                    Valor_desconto = Convert.ToDecimal(linhaAluguel["valor_desconto"]),
                    Valor_multa = Convert.ToDecimal(linhaAluguel["valor_multa"]),
                    DataInicial = Convert.ToDateTime(linhaAluguel["data_inicial"], CultureInfo.InvariantCulture),
                    DataFinal = Convert.ToDateTime(linhaAluguel["data_final"], CultureInfo.InvariantCulture),
                    DataDevolucao = Convert.ToDateTime(linhaAluguel["data_devolucao"], CultureInfo.InvariantCulture)
                };

                listaAluguel.Add(aluguel);
            }

            return listaAluguel;
        }

        public List<RelatorioRow> ReadAllCliente(DateTime dataIni, DateTime dataFim, int id)
        {
            string sql = $@"SELECT ca.cod_aluguel, cf.nome as nomeFunc, cc.nome as nomeCli, ca.valor_total, ca.data_inicial, ca.data_final, ca.data_devolucao, ca.valor_desconto, ca.valor_multa
                            from CAD_ALUGUEL ca left join cad_funcionarios cf on ca.cod_funcionario = cf.cod_funcionario left join cad_clientes cc on cc.cod_cliente = ca.cod_cliente 
                            WHERE data_final BETWEEN '{dataIni.ToString("MM-dd-yyyy")}' AND '{dataFim.ToString("MM-dd-yyyy")}' AND ca.cod_cliente = '{id}'";

            DataTable resp = _conn.dataTable(sql);

            List<RelatorioRow> listaAluguel = new List<RelatorioRow>();

            foreach (DataRow linhaAluguel in resp.Rows)
            {
                RelatorioRow aluguel = new RelatorioRow
                {
                    Id = Convert.ToInt32(linhaAluguel["cod_aluguel"]),
                    Vendedor = Convert.ToString(linhaAluguel["nomeFunc"]),
                    Locatario = Convert.ToString(linhaAluguel["nomeCli"]),
                    Valor_total = Convert.ToDecimal(linhaAluguel["valor_total"]),
                    Valor_desconto = Convert.ToDecimal(linhaAluguel["valor_desconto"]),
                    Valor_multa = Convert.ToDecimal(linhaAluguel["valor_multa"]),
                    DataInicial = Convert.ToDateTime(linhaAluguel["data_inicial"], CultureInfo.InvariantCulture),
                    DataFinal = Convert.ToDateTime(linhaAluguel["data_final"], CultureInfo.InvariantCulture),
                    DataDevolucao = Convert.ToDateTime(linhaAluguel["data_devolucao"], CultureInfo.InvariantCulture)
                };

                listaAluguel.Add(aluguel);
            }

            return listaAluguel;
        }
    }
}