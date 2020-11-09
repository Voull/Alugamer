using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Alugamer.Models
{
	public class Cliente
	{
		public int Id {get; set;}
		public string Nome {get; set;}
		public string Email {get; set;}
		public string Telefone {get; set;}
		public string Endereco {get; set;}
		public DateTime DataNascimento {get; set;}
		public string Sexo {get; set;}
		public string Cpf {get; set;}

		public List<String> validar()
		{
			return new List<string>();
		}
	}
}
