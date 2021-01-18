using Alugamer.Models;
using Alugamer.Utils;
using Alugamer.Validations;
using System;
using System.Collections.Generic;
using Xunit;

namespace Alugamer.Testes.UnitTests
{
	public class UnitTestAluguel
	{
		private readonly AluguelValidation aluguelValidation = new AluguelValidation();
		private readonly ErroModel erroModel = new ErroModel();

		[Fact]
		public void TesteAluguelVazio()
		{
			Aluguel alugueloVazio = new Aluguel();
			Assert.True(aluguelValidation.validar(alugueloVazio).Count > 0);
		}

		[Fact]
		public void TesteAluguelValido()
		{
			List<ItemAluguel> lista = new List<ItemAluguel>();

			ItemAluguel item = new ItemAluguel
			{
				IdAluguel = 1,
				IdItem = 1,
				Quantidade = 1,
				Valor = 1
			};
			lista.Add(item);

			Aluguel aluguelValido = new Aluguel
			{
			Id = 1,
			Locatario = 1,
			Vendedor = 1,
			Valor_total = 1200,
			Valor_desconto = 100,
			Valor_multa = 0,
			DataInicial = DateTime.Today,
			DataFinal = DateTime.Today.AddDays(1),
			DataDevolucao = DateTime.Today,
			Itens = lista
			};
			Assert.True(aluguelValidation.validar(aluguelValido).Count == 0);
		}

		[Fact]
		public void TesteClienteAusente()
		{
			List<ItemAluguel> lista = new List<ItemAluguel>();

			ItemAluguel item = new ItemAluguel
			{
				IdAluguel = 1,
				IdItem = 1,
				Quantidade = 1,
				Valor = 1
			};
			lista.Add(item);

			Aluguel aluguelValido = new Aluguel
			{
				Id = 1,
				Locatario = -1,
				Vendedor = 1,
				Valor_total = 1200,
				Valor_desconto = 100,
				Valor_multa = 0,
				DataInicial = DateTime.Today,
				DataFinal = DateTime.Today.AddDays(1),
				DataDevolucao = DateTime.Today,
				Itens = lista
			};
			Assert.True(aluguelValidation.validar(aluguelValido).Count == 1);
		}

		[Fact]
		public void TesteVendedorAusente()
		{
			List<ItemAluguel> lista = new List<ItemAluguel>();

			ItemAluguel item = new ItemAluguel
			{
				IdAluguel = 1,
				IdItem = 1,
				Quantidade = 1,
				Valor = 1
			};
			lista.Add(item);

			Aluguel aluguelValido = new Aluguel
			{
				Id = 1,
				Locatario =1,
				Vendedor = -1,
				Valor_total = 1200,
				Valor_desconto = 100,
				Valor_multa = 0,
				DataInicial = DateTime.Today,
				DataFinal = DateTime.Today.AddDays(1),
				DataDevolucao = DateTime.Today,
				Itens = lista
			};
			Assert.True(aluguelValidation.validar(aluguelValido).Count == 1);
		}

		[Fact]
		public void TesteValorTotalAusente()
		{
			List<ItemAluguel> lista = new List<ItemAluguel>();

			ItemAluguel item = new ItemAluguel
			{
				IdAluguel = 1,
				IdItem = 1,
				Quantidade = 1,
				Valor = 1
			};
			lista.Add(item);

			Aluguel aluguelValido = new Aluguel
			{
				Id = 1,
				Locatario = 1,
				Vendedor = 1,
				Valor_total = -1,
				Valor_desconto = 100,
				Valor_multa = 0,
				DataInicial = DateTime.Today,
				DataFinal = DateTime.Today.AddDays(1),
				DataDevolucao = DateTime.Today,
				Itens = lista
			};
			Assert.True(aluguelValidation.validar(aluguelValido).Count == 1);
		}

		[Fact]
		public void TesteValorDescontoAusente()
		{
			List<ItemAluguel> lista = new List<ItemAluguel>();

			ItemAluguel item = new ItemAluguel
			{
				IdAluguel = 1,
				IdItem = 1,
				Quantidade = 1,
				Valor = 1
			};
			lista.Add(item);

			Aluguel aluguelValido = new Aluguel
			{
				Id = 1,
				Locatario = 1,
				Vendedor = 1,
				Valor_total = 1200,
				Valor_desconto = -1,
				Valor_multa = 0,
				DataInicial = DateTime.Today,
				DataFinal = DateTime.Today.AddDays(1),
				DataDevolucao = DateTime.Today,
				Itens = lista
			};
			Assert.True(aluguelValidation.validar(aluguelValido).Count == 1);
		}

		[Fact]
		public void TesteDataInicialAusente()
		{
			List<ItemAluguel> lista = new List<ItemAluguel>();

			ItemAluguel item = new ItemAluguel
			{
				IdAluguel = 1,
				IdItem = 1,
				Quantidade = 1,
				Valor = 1
			};
			lista.Add(item);

			Aluguel aluguelValido = new Aluguel
			{
				Id = 1,
				Locatario = 1,
				Vendedor = 1,
				Valor_total = 1200,
				Valor_desconto = -1,
				Valor_multa = 0,
				DataFinal = DateTime.Today.AddDays(1),
				DataDevolucao = DateTime.Today,
				Itens = lista
			};
			Assert.True(aluguelValidation.validar(aluguelValido).Count == 1);
		}

		[Fact]
		public void TesteDataInicialInvalida()
		{
			List<ItemAluguel> lista = new List<ItemAluguel>();

			ItemAluguel item = new ItemAluguel
			{
				IdAluguel = 1,
				IdItem = 1,
				Quantidade = 1,
				Valor = 1
			};
			lista.Add(item);

			Aluguel aluguelValido = new Aluguel
			{
				Id = 1,
				Locatario = 1,
				Vendedor = 1,
				Valor_total = 1200,
				Valor_desconto = -1,
				Valor_multa = 0,
				DataInicial = DateTime.Today.AddDays(2),
				DataFinal = DateTime.Today.AddDays(1),
				DataDevolucao = DateTime.Today,
				Itens = lista
			};
			Assert.True(aluguelValidation.validar(aluguelValido).Count > 0);
		}


		[Fact]
		public void TesteDataFinalInvalida()
		{
			List<ItemAluguel> lista = new List<ItemAluguel>();

			ItemAluguel item = new ItemAluguel
			{
				IdAluguel = 1,
				IdItem = 1,
				Quantidade = 1,
				Valor = 1
			};
			lista.Add(item);

			Aluguel aluguelValido = new Aluguel
			{
				Id = 1,
				Locatario = 1,
				Vendedor = 1,
				Valor_total = 1200,
				Valor_desconto = -1,
				Valor_multa = 0,
				DataInicial = DateTime.Today,
				DataFinal = DateTime.Today,
				DataDevolucao = DateTime.Today,
				Itens = lista
			};
			Assert.True(aluguelValidation.validar(aluguelValido).Count == 1);
		}

		[Fact]
		public void TesteItemAluguelIdItemAusente()
		{
			List<ItemAluguel> lista = new List<ItemAluguel>();

			ItemAluguel item = new ItemAluguel
			{
				IdAluguel = 1,
				IdItem = -1,
				Quantidade = 1,
				Valor = 1
			};
			lista.Add(item);

			Aluguel aluguelValido = new Aluguel
			{
				Id = 1,
				Locatario = 1,
				Vendedor = 1,
				Valor_total = 1200,
				Valor_desconto = 100,
				Valor_multa = 0,
				DataInicial = DateTime.Today,
				DataFinal = DateTime.Today.AddDays(1),
				DataDevolucao = DateTime.Today,
				Itens = lista
			};
			Assert.True(aluguelValidation.validar(aluguelValido).Count == 1);
		}

		[Fact]
		public void TesteItemAluguelQtdAusente()
		{
			List<ItemAluguel> lista = new List<ItemAluguel>();

			ItemAluguel item = new ItemAluguel
			{
				IdAluguel = 1,
				IdItem = 1,
				Quantidade = -1,
				Valor = 1
			};
			lista.Add(item);

			Aluguel aluguelValido = new Aluguel
			{
				Id = 1,
				Locatario = 1,
				Vendedor = 1,
				Valor_total = 1200,
				Valor_desconto = 100,
				Valor_multa = 0,
				DataInicial = DateTime.Today,
				DataFinal = DateTime.Today.AddDays(1),
				DataDevolucao = DateTime.Today,
				Itens = lista
			};
			Assert.True(aluguelValidation.validar(aluguelValido).Count == 1);
		}

		[Fact]
		public void TesteItemAluguelVAlorAusente()
		{
			List<ItemAluguel> lista = new List<ItemAluguel>();

			ItemAluguel item = new ItemAluguel
			{
				IdAluguel = 1,
				IdItem = 1,
				Quantidade = 1,
				Valor = -1
			};
			lista.Add(item);

			Aluguel aluguelValido = new Aluguel
			{
				Id = 1,
				Locatario = 1,
				Vendedor = 1,
				Valor_total = 1200,
				Valor_desconto = 100,
				Valor_multa = 0,
				DataInicial = DateTime.Today,
				DataFinal = DateTime.Today.AddDays(1),
				DataDevolucao = DateTime.Today,
				Itens = lista
			};
			Assert.True(aluguelValidation.validar(aluguelValido).Count > 0);
		}
	}
}
