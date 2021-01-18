using Alugamer.Models;
using Alugamer.Utils;
using Alugamer.Validations;
using System;
using System.Collections.Generic;
using Xunit;

namespace Alugamer.Testes.UnitTests
{
	public class UnitTestFuncionario
	{
		private readonly FuncionarioValidation funcionarioValidation = new FuncionarioValidation();
		private readonly ErroModel erroModel = new ErroModel();

		[Fact]
		public void TesteFuncionarioVazio()
		{
			Funcionario funcionarioVazio = new Funcionario();
			Assert.True(funcionarioValidation.validar(funcionarioVazio).Count > 0);
		}

		[Fact]
		public void TesteFuncionarioValido()
		{
			Funcionario funcionarioValido = new Funcionario
			{
				Cpf = "111.111.111-11",
				DataNascimento = new DateTime(1999, 7, 27),
				Email = "gabriel@usp.br",
				Endereco = "Rua das Laranjas, 444",
				Id = 1,
				Nome = "Gabriel",
				Sexo = "M",
				Telefone = "(11) 11111-1113",
			};
			Assert.True(funcionarioValidation.validar(funcionarioValido).Count == 0);
		}

		[Fact]
		public void TesteFuncionarioCpfInvalido()
		{
			Funcionario funcionarioCpf = new Funcionario
			{
				Cpf = "111.111.111-1",
				DataNascimento = new DateTime(1999, 7, 27),
				Email = "gabriel@usp.br",
				Endereco = "Rua das Laranjas, 444",
				Id = 1,
				Nome = "Gabriel",
				Sexo = "M",
				Telefone = "(11) 11111-1113",
			};

			List<string> erros = funcionarioValidation.validar(funcionarioCpf);

			Assert.True(erros.Count == 1, $"Detectados mais erros do que o esperado! - Qtd: {erros.Count}");
			Assert.Equal(erros[0], erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_INVALIDO, "CPF"));
		}

		[Fact]
		public void TesteFuncionarioCpfAusente()
		{
			Funcionario funcionarioCpf = new Funcionario
			{
				DataNascimento = new DateTime(1999, 7, 27),
				Email = "gabriel@usp.br",
				Endereco = "Rua das Laranjas, 444",
				Id = 1,
				Nome = "Gabriel",
				Sexo = "M",
				Telefone = "(11) 11111-1113",
			};

			List<string> erros = funcionarioValidation.validar(funcionarioCpf);

			Assert.True(erros.Count == 1, $"Detectados mais erros do que o esperado! - Qtd: {erros.Count}");
			Assert.Equal(erros[0], erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "CPF"));
		}

		[Fact]
		public void TesteFuncionarioDataNascimentoInvalido()
		{
			Funcionario funcionarioDataNascimento = new Funcionario
			{
				Cpf = "111.111.111-11",
				DataNascimento = DateTime.Today.AddDays(1),
				Email = "gabriel@usp.br",
				Endereco = "Rua das Laranjas, 444",
				Id = 1,
				Nome = "Gabriel",
				Sexo = "M",
				Telefone = "(11) 11111-1113",
			};

			List<string> erros = funcionarioValidation.validar(funcionarioDataNascimento);

			Assert.True(erros.Count == 1, $"Detectados mais erros do que o esperado! - Qtd: {erros.Count}");
			Assert.Equal(erros[0], erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_INVALIDO, "Data de Nascimento"));
		}

		[Fact]
		public void TesteFuncionarioDataNascimentoAusente()
		{
			Funcionario funcionarioDataNascimento = new Funcionario
			{
				Cpf = "111.111.111-11",
				Email = "gabriel@usp.br",
				Endereco = "Rua das Laranjas, 444",
				Id = 1,
				Nome = "Gabriel",
				Sexo = "M",
				Telefone = "(11) 11111-1113",
			};

			List<string> erros = funcionarioValidation.validar(funcionarioDataNascimento);

			Assert.True(erros.Count == 1, $"Detectados mais erros do que o esperado! - Qtd: {erros.Count}");
			Assert.Equal(erros[0], erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_INVALIDO, "Data de Nascimento"));
		}

		[Fact]
		public void TesteFuncionarioEmailInvalido()
		{
			Funcionario funcionarioEmail = new Funcionario
			{
				Cpf = "111.111.111-11",
				DataNascimento = new DateTime(1999, 7, 27),
				Email = "gabriel.com",
				Endereco = "Rua das Laranjas, 444",
				Id = 1,
				Nome = "Gabriel",
				Sexo = "M",
				Telefone = "(11) 11111-1113",
			};

			List<string> erros = funcionarioValidation.validar(funcionarioEmail);

			Assert.True(erros.Count == 1, $"Detectados mais erros do que o esperado! - Qtd: {erros.Count}");
			Assert.Equal(erros[0], erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_INVALIDO, "E-mail"));
		}

		[Fact]
		public void TesteFuncionarioEmailMaxTamanho()
		{
			Funcionario funcionarioEmail = new Funcionario
			{
				Cpf = "111.111.111-11",
				DataNascimento = new DateTime(1999, 7, 27),
				Email = new string('a', 198) + "@a.com",
				Endereco = "Rua das Laranjas, 444",
				Id = 1,
				Nome = "Gabriel",
				Sexo = "M",
				Telefone = "(11) 11111-1113",
			};

			List<string> erros = funcionarioValidation.validar(funcionarioEmail);

			Assert.True(erros.Count == 1, $"Detectados mais erros do que o esperado! - Qtd: {erros.Count}");
			Assert.Equal(erros[0], erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_TAMANHO_MAX, "E-mail"));
		}

		[Fact]
		public void TesteFuncionarioEmailAusente()
		{
			Funcionario funcionarioEmail = new Funcionario
			{
				Cpf = "111.111.111-11",
				DataNascimento = new DateTime(1999, 7, 27),
				Endereco = "Rua das Laranjas, 444",
				Id = 1,
				Nome = "Gabriel",
				Sexo = "M",
				Telefone = "(11) 11111-1113",
			};

			List<string> erros = funcionarioValidation.validar(funcionarioEmail);

			Assert.True(erros.Count == 1, $"Detectados mais erros do que o esperado! - Qtd: {erros.Count}");
			Assert.Equal(erros[0], erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "E-mail"));
		}


		[Fact]
		public void TesteFuncionarioEnderecoMaxTamanho()
		{
			Funcionario funcionarioEndereco = new Funcionario
			{
				Cpf = "111.111.111-11",
				DataNascimento = new DateTime(1999, 7, 27),
				Email = "gabriel@usp.br",
				Endereco = "Rua das Laranjas, 444" + new string('a', 200),
				Id = 1,
				Nome = "Gabriel",
				Sexo = "M",
				Telefone = "(11) 11111-1113",
			};

			List<string> erros = funcionarioValidation.validar(funcionarioEndereco);

			Assert.True(erros.Count == 1, $"Detectados mais erros do que o esperado! - Qtd: {erros.Count}");
			Assert.Equal(erros[0], erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_TAMANHO_MAX, "Endereco"));
		}

		[Fact]
		public void TesteFuncionarioEnderecoAusente()
		{
			Funcionario funcionarioEndereco = new Funcionario
			{
				Cpf = "111.111.111-11",
				DataNascimento = new DateTime(1999, 7, 27),
				Email = "gabriel@usp.br",
				Id = 1,
				Nome = "Gabriel",
				Sexo = "M",
				Telefone = "(11) 11111-1113",
			};

			List<string> erros = funcionarioValidation.validar(funcionarioEndereco);

			Assert.True(erros.Count == 1, $"Detectados mais erros do que o esperado! - Qtd: {erros.Count}");
			Assert.Equal(erros[0], erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Endereco"));
		}


		[Fact]
		public void TesteFuncionarioNomeMaxTamanho()
		{
			Funcionario funcionarioNome = new Funcionario
			{
				Cpf = "111.111.111-11",
				DataNascimento = new DateTime(1999, 7, 27),
				Email = "gabriel@usp.br",
				Endereco = "Rua das Laranjas, 444",
				Id = 1,
				Nome = "Gabriel" + new string('a', 100),
				Sexo = "M",
				Telefone = "(11) 11111-1113",
			};

			List<string> erros = funcionarioValidation.validar(funcionarioNome);

			Assert.True(erros.Count == 1, $"Detectados mais erros do que o esperado! - Qtd: {erros.Count}");
			Assert.Equal(erros[0], erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_TAMANHO_MAX, "Nome"));
		}

		[Fact]
		public void TesteFuncionarioNomeAusente()
		{
			Funcionario funcionarioNome = new Funcionario
			{
				Cpf = "111.111.111-11",
				DataNascimento = new DateTime(1999, 7, 27),
				Email = "gabriel@usp.br",
				Endereco = "Rua das Laranjas, 444",
				Id = 1,
				Sexo = "M",
				Telefone = "(11) 11111-1113",
			};

			List<string> erros = funcionarioValidation.validar(funcionarioNome);

			Assert.True(erros.Count == 1, $"Detectados mais erros do que o esperado! - Qtd: {erros.Count}");
			Assert.Equal(erros[0], erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Nome"));
		}

		[Fact]
		public void TesteFuncionarioSexoInvalido()
		{
			Funcionario funcionarioSexo = new Funcionario
			{
				Cpf = "111.111.111-11",
				DataNascimento = new DateTime(1999, 7, 27),
				Email = "gabriel@usp.br",
				Endereco = "Rua das Laranjas, 444",
				Id = 1,
				Nome = "Gabriel",
				Sexo = "A",
				Telefone = "(11) 11111-1113",
			};

			List<string> erros = funcionarioValidation.validar(funcionarioSexo);

			Assert.True(erros.Count == 1, $"Detectados mais erros do que o esperado! - Qtd: {erros.Count}");
			Assert.Equal(erros[0], erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_INVALIDO, "Sexo"));
		}

		[Fact]
		public void TesteFuncionarioSexoAusente()
		{
			Funcionario funcionarioSexo = new Funcionario
			{
				Cpf = "111.111.111-11",
				DataNascimento = new DateTime(1999, 7, 27),
				Email = "gabriel@usp.br",
				Endereco = "Rua das Laranjas, 444",
				Id = 1,
				Nome = "Gabriel",
				Telefone = "(11) 11111-1113",
			};

			List<string> erros = funcionarioValidation.validar(funcionarioSexo);

			Assert.True(erros.Count == 1, $"Detectados mais erros do que o esperado! - Qtd: {erros.Count}");
			Assert.Equal(erros[0], erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Sexo"));
		}


		[Fact]
		public void TesteFuncionarioTelefoneInvalido()
		{
			Funcionario funcionarioTelefone = new Funcionario
			{
				Cpf = "111.111.111-11",
				DataNascimento = new DateTime(1999, 7, 27),
				Email = "gabriel@usp.br",
				Endereco = "Rua das Laranjas, 444",
				Id = 1,
				Nome = "Gabriel",
				Sexo = "M",
				Telefone = "(11) 11111-11188",
			};

			List<string> erros = funcionarioValidation.validar(funcionarioTelefone);

			Assert.True(erros.Count == 1, $"Detectados mais erros do que o esperado! - Qtd: {erros.Count}");
			Assert.Equal(erros[0], erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_INVALIDO, "Telefone"));
		}

		[Fact]
		public void TesteFuncionarioTelefoneAusente()
		{
			Funcionario funcionarioTelefone = new Funcionario
			{
				Cpf = "111.111.111-11",
				DataNascimento = new DateTime(1999, 7, 27),
				Email = "gabriel@usp.br",
				Endereco = "Rua das Laranjas, 444",
				Id = 1,
				Nome = "Gabriel",
				Sexo = "M"
			};

			List<string> erros = funcionarioValidation.validar(funcionarioTelefone);

			Assert.True(erros.Count == 1, $"Detectados mais erros do que o esperado! - Qtd: {erros.Count}");
			Assert.Equal(erros[0], erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Telefone"));
		}
	}
}
