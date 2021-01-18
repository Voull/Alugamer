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
			Aluguel aluguelValido = new Aluguel
			{

			};
			Assert.True(aluguelValidation.validar(aluguelValido).Count == 0);
		}
	}
}
