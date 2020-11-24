using Alugamer.Models;
using System;
using Xunit;

namespace Alugamer.Testes
{
	public class UnitTestCliente
	{
		private readonly Cliente cliente = new Cliente();
		[Fact]
		public void TesteClienteVazio()
		{
			Assert.True(cliente.validar().Count > 0);
		}
	}
}
