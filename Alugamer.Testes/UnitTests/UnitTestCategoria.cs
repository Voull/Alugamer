using Alugamer.Models;
using Alugamer.Utils;
using Alugamer.Validations;
using System;
using System.Collections.Generic;
using Xunit;

namespace Alugamer.Testes.UnitTests
{
	public class UnitTestCategoria
	{
		private readonly CategoriaValidation categoriaValidation = new CategoriaValidation();
		private readonly ErroModel erroModel = new ErroModel();

		[Fact]
        public void TesteClienteVazio()
		{
			Categoria categoriaVazio = new Categoria();
			Assert.True(categoriaValidation.valida(categoriaVazio).Count > 0);
		}

		[Fact]
		public void TesteCategoriaValido()
        {
			Categoria categoriaValido = new Categoria
			{
				Id = 1,
				Nome = "nomeTeste",
				Descricao = "Descricao de teste"

            };
			Assert.True(categoriaValidation.valida(categoriaValido).Count == 0);
		}

		[Fact]
		public void TesteIdAusente()
		{
			Categoria categoriaValido = new Categoria
			{
				Id = -1,
				Nome = "nomeTeste",
				Descricao = "Descricao de teste"

			};
			Assert.True(categoriaValidation.valida(categoriaValido).Count == 0);
		}

		[Fact]
		public void TesteNomeAusente()
		{
			Categoria categoriaValido = new Categoria
			{
				Id = 1,
				Nome = string.Empty,
				Descricao = "Descricao de teste"

			};
			Assert.True(categoriaValidation.valida(categoriaValido).Count == 0);
		}

		[Fact]
		public void TesteNomeTamanhoMax()
		{
			Categoria categoriaValido = new Categoria
			{
				Id = 1,
				Nome = "nomeTeste" + new string('a', 100),
				Descricao = "Descricao de teste"

			};
			Assert.True(categoriaValidation.valida(categoriaValido).Count == 0);
		}

		[Fact]
		public void TesteDescricaoAusente()
		{
			Categoria categoriaValido = new Categoria
			{
				Id = 1,
				Nome = "nomeTeste",
				Descricao = string.Empty

			};
			Assert.True(categoriaValidation.valida(categoriaValido).Count == 0);
		}

		[Fact]
		public void TesteDescricaoTamanhoMax()
		{
			Categoria categoriaValido = new Categoria
			{
				Id = 1,
				Nome = "nomeTeste",
				Descricao = "Descricao de teste" + new string('a', 100)

			};
			Assert.True(categoriaValidation.valida(categoriaValido).Count == 0);
		}


	}
}
