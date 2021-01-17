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
        private Conexao _conn;
        public RelatorioDao()
		{
            _conn = new Conexao();
		}


        public List<Aluguel> ReadAll(DateTime dataIni, DateTime dataFim)
        {
            string sql = $@"SELECT cod_aluguel, cod_funcionario, cod_cliente, valor_total, data_inicial, data_final 
                            from CAD_ALUGUEL WHERE data_final BETWEEN '{dataIni.ToString("MM-dd-yyyy")}' AND '{dataFim.ToString("MM-dd-yyyy")}'";

            DataTable resp = _conn.dataTable(sql);

            List<Aluguel> listaAluguel = new List<Aluguel>();

            foreach (DataRow linhaAluguel in resp.Rows)
            {
                Aluguel aluguel = new Aluguel
                {
                    Id = Convert.ToInt32(linhaAluguel["cod_aluguel"]),
                    Vendedor = Convert.ToInt32(linhaAluguel["cod_funcionario"]),
                    Locatario = Convert.ToInt32(linhaAluguel["cod_cliente"]),
                    Valor_total = Convert.ToDecimal(linhaAluguel["valor_total"]),
                    DataInicial = Convert.ToDateTime(linhaAluguel["data_inicial"], CultureInfo.InvariantCulture),
                    DataFinal = Convert.ToDateTime(linhaAluguel["data_final"], CultureInfo.InvariantCulture)
                };

                listaAluguel.Add(aluguel);
            }

            return listaAluguel;
        }

        public List<Aluguel> ReadAllCliente(DateTime dataIni, DateTime dataFim, int id)
        {
            string sql = $@"SELECT cod_aluguel, cod_funcionario, cod_cliente, valor_total, data_inicial, data_final 
                            from CAD_ALUGUEL WHERE data_final BETWEEN '{dataIni.ToString("MM-dd-yyyy")}' AND '{dataFim.ToString("MM-dd-yyyy")}' AND cod_cliente = '{id}'";

            DataTable resp = _conn.dataTable(sql);

            List<Aluguel> listaAluguel = new List<Aluguel>();

            foreach (DataRow linhaAluguel in resp.Rows)
            {
                Aluguel aluguel = new Aluguel
                {
                    Id = Convert.ToInt32(linhaAluguel["cod_aluguel"]),
                    Vendedor = Convert.ToInt32(linhaAluguel["cod_funcionario"]),
                    Locatario = Convert.ToInt32(linhaAluguel["cod_cliente"]),
                    Valor_total = Convert.ToDecimal(linhaAluguel["valor_total"]),
                    DataInicial = Convert.ToDateTime(linhaAluguel["data_inicial"], CultureInfo.InvariantCulture),
                    DataFinal = Convert.ToDateTime(linhaAluguel["data_final"], CultureInfo.InvariantCulture)
                };

                listaAluguel.Add(aluguel);
            }

            return listaAluguel;
        }
    }
}