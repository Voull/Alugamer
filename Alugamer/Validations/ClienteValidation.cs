using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Alugamer.Models;
using Alugamer.Utils;

namespace Alugamer.Validations
{
    public class ClienteValidation
    {
		private ErroModel erroModel;
		private readonly Regex regexTelefone = new Regex(@"^\([0-9]{2}\) [0-9]{4,5}-[0-9]{4}$");
		private readonly Regex regexCpf = new Regex(@"^([0-9]{3}\.){2}[0-9]{3}-[0-9]{2}$");
		private readonly Regex regexEmail = new Regex(@"^[^@]+@[a-zA-Z0-9]+\.\w+");
		public ClienteValidation()
        {
			erroModel = new ErroModel();
		}

		public List<String> validar(Cliente cliente)
		{
			List<string> listaErros = new List<string>();

			if (cliente.Id < 0)
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Código"));

			if (string.IsNullOrEmpty(cliente.Nome))
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Nome"));
			else if (cliente.Nome.Length > 100)
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_TAMANHO_MAX, "Nome"));

			if (string.IsNullOrEmpty(cliente.Email))
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "E-mail"));
			else if(!regexEmail.IsMatch(cliente.Email))
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_INVALIDO, "E-mail"));
			else if (cliente.Email.Length > 200)
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_TAMANHO_MAX, "E-mail"));

			if (string.IsNullOrEmpty(cliente.Telefone))
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Telefone"));
			
			else if (!regexTelefone.IsMatch(cliente.Telefone))
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_INVALIDO, "Telefone"));

			if (string.IsNullOrEmpty(cliente.Endereco))
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Endereço"));
			else if (cliente.Endereco.Length > 200)
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_TAMANHO_MAX, "Endereço"));

			if (cliente.DataNascimento == null)
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Data de Nascimento"));
			else if (cliente.DataNascimento.Date >= DateTime.Today)
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_INVALIDO, "Data de Nascimento"));

			if (string.IsNullOrEmpty(cliente.Sexo))
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Sexo"));
			else if (!cliente.Sexo.Equals("M") && !cliente.Sexo.Equals("F"))
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_INVALIDO, "Sexo"));
			
			if (string.IsNullOrEmpty(cliente.Cpf))
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "CPF"));
			else if (!regexCpf.IsMatch(cliente.Cpf))
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_INVALIDO, "CPF"));

			return listaErros;
		}
	}
}
