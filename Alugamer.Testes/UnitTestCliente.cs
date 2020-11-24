using Alugamer.Models;
using System;
using Xunit;

namespace Alugamer.Testes
{
	public class UnitTestCliente
	{
		[Fact]
		public void TesteClienteVazio()
		{
			Assert.True(new Cliente().validar().Count > 0);
		}
	}
}
