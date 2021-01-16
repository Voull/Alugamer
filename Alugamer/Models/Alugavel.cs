using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Alugamer.Utils;

namespace Alugamer.Models
{
	public class Alugavel
	{

		public int Id { get; set; }

		public string Nome { get; set; }

		public string Descricao { get; set; }

		public int Quantidade { get; set; }

		public Decimal Valor_compra { get; set; }

		public Decimal Valor_aluguel { get; set; }

		public int IdCategoria { get; set; }

		public Alugavel()
		{
			Id = -1;
			Nome = string.Empty;
			Descricao = string.Empty;
			Quantidade = -1;
			Valor_compra = -1;
			Valor_aluguel = -1;
			IdCategoria = -1;
		}
	}
}