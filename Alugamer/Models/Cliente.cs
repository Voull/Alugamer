using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Alugamer.Utils;

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

		public Cliente()
        {
			Id = -1;
			Nome = string.Empty;
			Email = string.Empty;
			Telefone = string.Empty;
			Endereco = string.Empty;
			DataNascimento = DateTime.Today;
			Sexo = string.Empty;
			Cpf = string.Empty;
        }

	}
}
