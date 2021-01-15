using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Alugamer.Models;
using Alugamer.Utils;

namespace Alugamer.Validations
{
    public class FuncionarioValidation
    {
		private ErroModel erroModel;
		private readonly Regex regexTelefone = new Regex(@"^\([0-9]{2}\) [0-9]{4,5}-[0-9]{4}$");
		private readonly Regex regexCpf = new Regex(@"^([0-9]{3}\.){2}[0-9]{3}-[0-9]{2}$");
		private readonly Regex regexEmail = new Regex(@"^[^@]+@[a-zA-Z0-9]+\.\w+");
		public FuncionarioValidation()
        {
			erroModel = new ErroModel();
		}

		public List<String> validar(Funcionario funcionario)
		{
			List<string> listaErros = new List<string>();

			if (funcionario.Id < 0)
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Código"));

			if (string.IsNullOrEmpty(funcionario.Nome))
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Nome"));
			else if (funcionario.Nome.Length > 100)
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_TAMANHO_MAX, "Nome"));

			if (string.IsNullOrEmpty(funcionario.Email))
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "E-mail"));
			else if(!regexEmail.IsMatch(funcionario.Email))
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_INVALIDO, "E-mail"));
			else if (funcionario.Email.Length > 200)
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_TAMANHO_MAX, "E-mail"));

			if (string.IsNullOrEmpty(funcionario.Telefone))
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Telefone"));
			
			else if (!regexTelefone.IsMatch(funcionario.Telefone))
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_INVALIDO, "Telefone"));

			if (string.IsNullOrEmpty(funcionario.Endereco))
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Endereço"));
			else if (funcionario.Endereco.Length > 200)
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_TAMANHO_MAX, "Endereço"));

			if (funcionario.DataNascimento == null)
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Data de Nascimento"));
			else if (funcionario.DataNascimento.Date >= DateTime.Today)
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_INVALIDO, "Data de Nascimento"));

			if (string.IsNullOrEmpty(funcionario.Sexo))
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Sexo"));
			else if (!funcionario.Sexo.Equals("M") && !funcionario.Sexo.Equals("F"))
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_INVALIDO, "Sexo"));
			
			if (string.IsNullOrEmpty(funcionario.Cpf))
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "CPF"));
			else if (!regexCpf.IsMatch(funcionario.Cpf))
				listaErros.Add(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_INVALIDO, "CPF"));

			return listaErros;
		}
	}
}
