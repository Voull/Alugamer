using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Alugamer.Utils;

namespace Alugamer.Models
{
	public class ItemAluguel
	{

		public int IdAluguel { get; set; }

		public int IdItem { get; set; }

		public int Quantidade { get; set; }

		public Decimal Valor { get; set; }

		public ItemAluguel()
		{
			IdAluguel = -1;
			IdItem = -1;
			Quantidade = -1;
			Valor = -1;
		}
	}
}