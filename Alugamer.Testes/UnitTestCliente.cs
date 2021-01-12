using Alugamer.Models;
using Alugamer.Utils;
using Alugamer.Validations;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Alugamer.Testes
{
	public class UnitTestCliente
	{
		private readonly ClienteValidation clienteValidation = new ClienteValidation();
		private readonly ErroModel erroModel = new ErroModel();

		public UnitTestCliente()
        {
			CultureInfo.CurrentCulture = new CultureInfo("pt-BR");
        }
		[Fact]
        public void TesteClienteVazio()
		{
			Cliente clienteVazio = new Cliente();
			Assert.True(clienteValidation.validar(clienteVazio).Count > 0);
		}

		[Fact]
		public void TesteClienteValido()
        {
			Cliente clienteValido = new Cliente
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
			Assert.True(clienteValidation.validar(clienteValido).Count == 0);
		}

		[Fact]
		public void TesteClienteCpfInvalido()
        {
			Cliente clienteCpf = new Cliente
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

			List<string> erros = clienteValidation.validar(clienteCpf);

			Assert.True(erros.Count == 1, $"Detectados mais erros do que o esperado! - Qtd: {erros.Count}");
			Assert.Equal(erros[0], erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_INVALIDO, "CPF"));
		}

		[Fact]
		public void TesteClienteCpfAusente()
		{
			Cliente clienteCpf = new Cliente
			{
				DataNascimento = new DateTime(1999, 7, 27),
				Email = "gabriel@usp.br",
				Endereco = "Rua das Laranjas, 444",
				Id = 1,
				Nome = "Gabriel",
				Sexo = "M",
				Telefone = "(11) 11111-1113",
			};

			List<string> erros = clienteValidation.validar(clienteCpf);

			Assert.True(erros.Count == 1, $"Detectados mais erros do que o esperado! - Qtd: {erros.Count}");
			Assert.Equal(erros[0], erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "CPF"));
		}

		[Fact]
		public void TesteClienteDataNascimentoInvalido()
		{
			Cliente clienteDataNascimento = new Cliente
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

			List<string> erros = clienteValidation.validar(clienteDataNascimento);

			Assert.True(erros.Count == 1, $"Detectados mais erros do que o esperado! - Qtd: {erros.Count}");
			Assert.Equal(erros[0], erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_INVALIDO, "Data de Nascimento"));
		}

		[Fact]
		public void TesteClienteDataNascimentoAusente()
		{
			Cliente clienteDataNascimento = new Cliente
			{
				Cpf = "111.111.111-11",
				Email = "gabriel@usp.br",
				Endereco = "Rua das Laranjas, 444",
				Id = 1,
				Nome = "Gabriel",
				Sexo = "M",
				Telefone = "(11) 11111-1113",
			};

			List<string> erros = clienteValidation.validar(clienteDataNascimento);

			Assert.True(erros.Count == 1, $"Detectados mais erros do que o esperado! - Qtd: {erros.Count}");
			Assert.Equal(erros[0], erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_INVALIDO, "Data de Nascimento"));
		}

		[Fact]
		public void TesteClienteEmailInvalido()
		{
			Cliente clienteEmail = new Cliente
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

			List<string> erros = clienteValidation.validar(clienteEmail);

			Assert.True(erros.Count == 1, $"Detectados mais erros do que o esperado! - Qtd: {erros.Count}");
			Assert.Equal(erros[0], erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_INVALIDO, "E-mail"));
		}

		[Fact]
		public void TesteClienteEmailMaxTamanho()
		{
			Cliente clienteEmail = new Cliente
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

			List<string> erros = clienteValidation.validar(clienteEmail);

			Assert.True(erros.Count == 1, $"Detectados mais erros do que o esperado! - Qtd: {erros.Count}");
			Assert.Equal(erros[0], erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_TAMANHO_MAX, "E-mail"));
		}

		[Fact]
		public void TesteClienteEmailAusente()
		{
			Cliente clienteEmail = new Cliente
			{
				Cpf = "111.111.111-11",
				DataNascimento = new DateTime(1999, 7, 27),
				Endereco = "Rua das Laranjas, 444",
				Id = 1,
				Nome = "Gabriel",
				Sexo = "M",
				Telefone = "(11) 11111-1113",
			};

			List<string> erros = clienteValidation.validar(clienteEmail);

			Assert.True(erros.Count == 1, $"Detectados mais erros do que o esperado! - Qtd: {erros.Count}");
			Assert.Equal(erros[0], erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "E-mail"));
		}


		[Fact]
		public void TesteClienteEnderecoMaxTamanho()
		{
			Cliente clienteEndereco = new Cliente
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

			List<string> erros = clienteValidation.validar(clienteEndereco);

			
            Assert.True(erros.Count == 1, $"Detectados mais erros do que o esperado! - Qtd: {erros.Count}");
			Assert.True(string.Compare(erros[0], erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_TAMANHO_MAX, "Endereço"), false, CultureInfo.CurrentCulture) == 0);
		}

		[Fact]
		public void TesteClienteEnderecoAusente()
		{
			Cliente clienteEndereco = new Cliente
			{
				Cpf = "111.111.111-11",
				DataNascimento = new DateTime(1999, 7, 27),
				Email = "gabriel@usp.br",
				Id = 1,
				Nome = "Gabriel",
				Sexo = "M",
				Telefone = "(11) 11111-1113",
			};

			List<string> erros = clienteValidation.validar(clienteEndereco);

			Assert.True(erros.Count == 1, $"Detectados mais erros do que o esperado! - Qtd: {erros.Count}");
			Assert.Equal(erros[0], erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Endereço"));
		}


		[Fact]
		public void TesteClienteNomeMaxTamanho()
		{
			Cliente clienteNome = new Cliente
			{
				Cpf = "111.111.111-11",
				DataNascimento = new DateTime(1999, 7, 27),
				Email = "gabriel@usp.br",
				Endereco = "Rua das Laranjas, 444" ,
				Id = 1,
				Nome = "Gabriel" + new string('a', 100),
				Sexo = "M",
				Telefone = "(11) 11111-1113",
			};

			List<string> erros = clienteValidation.validar(clienteNome);

			Assert.True(erros.Count == 1, $"Detectados mais erros do que o esperado! - Qtd: {erros.Count}");
			Assert.Equal(erros[0], erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_TAMANHO_MAX, "Nome"));
		}

		[Fact]
		public void TesteClienteNomeAusente()
		{
			Cliente clienteNome = new Cliente
			{
				Cpf = "111.111.111-11",
				DataNascimento = new DateTime(1999, 7, 27),
				Email = "gabriel@usp.br",
				Endereco = "Rua das Laranjas, 444",
				Id = 1,
				Sexo = "M",
				Telefone = "(11) 11111-1113",
			};

			List<string> erros = clienteValidation.validar(clienteNome);

			Assert.True(erros.Count == 1, $"Detectados mais erros do que o esperado! - Qtd: {erros.Count}");
			Assert.Equal(erros[0], erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Nome"));
		}

		[Fact]
		public void TesteClienteSexoInvalido()
		{
			Cliente clienteSexo = new Cliente
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

			List<string> erros = clienteValidation.validar(clienteSexo);

			Assert.True(erros.Count == 1, $"Detectados mais erros do que o esperado! - Qtd: {erros.Count}");
			Assert.Equal(erros[0], erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_INVALIDO, "Sexo"));
		}

		[Fact]
		public void TesteClienteSexoAusente()
		{
			Cliente clienteSexo = new Cliente
			{
				Cpf = "111.111.111-11",
				DataNascimento = new DateTime(1999, 7, 27),
				Email = "gabriel@usp.br",
				Endereco = "Rua das Laranjas, 444",
				Id = 1,
				Nome = "Gabriel",
				Telefone = "(11) 11111-1113",
			};

			List<string> erros = clienteValidation.validar(clienteSexo);

			Assert.True(erros.Count == 1, $"Detectados mais erros do que o esperado! - Qtd: {erros.Count}");
			Assert.Equal(erros[0], erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Sexo"));
		}


		[Fact]
		public void TesteClienteTelefoneInvalido()
		{
			Cliente clienteTelefone = new Cliente
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

			List<string> erros = clienteValidation.validar(clienteTelefone);

			Assert.True(erros.Count == 1, $"Detectados mais erros do que o esperado! - Qtd: {erros.Count}");
			Assert.Equal(erros[0], erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_INVALIDO, "Telefone"));
		}

		[Fact]
		public void TesteClienteTelefoneAusente()
		{
			Cliente clienteTelefone = new Cliente
			{
				Cpf = "111.111.111-11",
				DataNascimento = new DateTime(1999, 7, 27),
				Email = "gabriel@usp.br",
				Endereco = "Rua das Laranjas, 444",
				Id = 1,
				Nome = "Gabriel",
				Sexo = "M"
			};

			List<string> erros = clienteValidation.validar(clienteTelefone);

			Assert.True(erros.Count == 1, $"Detectados mais erros do que o esperado! - Qtd: {erros.Count}");
			Assert.Equal(erros[0], erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Telefone"));
		}
	}
}
