using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Alugamer.Utils;

namespace Alugamer.Models
{
	public class Aluguel
	{
		public int Id { get; set;}

		[DisplayName("Cliente")]
		public int Locatario {get; set;}
		
		public int Vendedor {get; set;}

		[DisplayName("Valor Total")]
		public Decimal Valor_total {get; set;}

		[DisplayName("Data Inicial")]
		public DateTime DataInicial {get; set;}

		[DisplayName("Data Final")]
		public DateTime DataFinal { get; set; }
		
		public List<ItemAluguel> Itens { get; set; }

		public Aluguel()
        {
			Id = -1;
			Locatario = -1;
			Vendedor = -1;
			Valor_total = -1;
			DataInicial = DateTime.Today;
			DataFinal = DateTime.Today;
			Itens = new List<ItemAluguel>();
		}
	}
}
