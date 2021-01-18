using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alugamer.Models
{
    public class RelatorioRow
    {
		public int Id { get; set; }
		public string Locatario { get; set; }
		public string Vendedor { get; set; }
		public Decimal Valor_total { get; set; }
		public Decimal Valor_desconto { get; set; }
		public Decimal Valor_multa { get; set; }
		public DateTime DataInicial { get; set; }
		public DateTime DataDevolucao { get; set; }
		public DateTime DataFinal { get; set; }
		public List<ItemAluguel> Itens { get; set; }
	}
}
