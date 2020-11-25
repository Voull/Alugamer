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
		private ErroModel erroModel;
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
			erroModel = new ErroModel();
        }

		public List<String> validar()
		{
			List<string> listaErros = new List<string>();

			if (Id < 0)
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Código"));

			if (string.IsNullOrEmpty(Nome))
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Nome"));
			else if (Nome.Length > 200)
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_TAMANHO_MAX, "Nome"));

			if (string.IsNullOrEmpty(Email))
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "E-mail"));

			else if (Email.Length > 200)
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_TAMANHO_MAX, "E-mail"));

			if (string.IsNullOrEmpty(Telefone))
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Telefone"));

			if (string.IsNullOrEmpty(Endereco))
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Endereço"));
			else if (Endereco.Length > 200)
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_TAMANHO_MAX, "Endereço"));

			if(DataNascimento == null)
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Data de Nascimento"));
			else if (DataNascimento.Date >= DateTime.Today)
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_INVALIDO, "Data de Nascimento"));

			if(string.IsNullOrEmpty(Sexo))
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Sexo"));
			else if(!Sexo.Equals("M") || !Sexo.Equals("F"))
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_INVALIDO, "Sexo"));

			return listaErros;
		}
	}
}
