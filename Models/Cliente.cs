using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Alugamer.Models
{
	[DataContract]
	public class Cliente
	{
		[DataMember]
		public int Id;
		[DataMember]
		public string Nome;
		[DataMember]
		public string Email;
		[DataMember]
		public string Telefone;
		[DataMember]
		public string Endereco;
		[DataMember]
		public DateTime DataNascimento;
		[DataMember]
		public string Sexo;
		[DataMember]
		public string Cpf;

		public List<String> validar()
		{
			return new List<string>();
		}
	}
}
