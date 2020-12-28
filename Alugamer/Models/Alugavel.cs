using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Alugamer.Models
{
	[DataContract]
	public class Alugavel
	{
		[DataMember]
		public int Id;
		[DataMember]
		public string Nome;
		[DataMember]
		public string Descricao;
		[DataMember]
		public int Quantidade;
		[DataMember]
		public Decimal valor_compra;
		[DataMember]
		public Decimal valor_aluguel;

		public List<String> validar()
		{
			return new List<string>();
		}
	}
}