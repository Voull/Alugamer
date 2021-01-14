using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Alugamer.Models;
using Alugamer.Testes.Utils;
using Alugamer.Utils;
using Alugamer.Validations;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace Alugamer.Testes
{
    [TestCaseOrderer("Alugamer.Testes.Utils.PriorityOrderer", "Alugamer.Testes")]
    public class IntegrationTestCliente : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly ErroModel erroModel;
        private readonly ErroDatabase erroDatabase;
        private readonly ClienteValidation clienteValidation;
        public IntegrationTestCliente(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            erroModel = new ErroModel();
            erroDatabase = new ErroDatabase();
            clienteValidation = new ClienteValidation();
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Cliente")]
        public async Task GetEndpoints(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
        [Fact, TestPriority(-1)]
        public async Task GetCliente()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/Cliente/Busca/1");

            response.EnsureSuccessStatusCode();
            Cliente cliente = JsonConvert.DeserializeObject<Cliente>(response.Content.ReadAsStringAsync().Result);
            List<String> validacao = clienteValidation.validar(cliente);

            Assert.True(validacao.Count == 0);
        }

        [Fact]
        public async Task InsertCliente()
        {
            Cliente cliente = new Cliente
            {
                Nome = "Marcus Steadfast",
                DataNascimento = new DateTime(1999, 11, 10),
                Email = "marcus.steadfast@hotmail.com",
                Endereco = "Rua dos Melões, 69",
                Cpf = "333.333.333-33",
                Id = 0,
                Sexo = "M",
                Telefone = "(21) 1111-1111"
            };

            var client = _factory.CreateClient();

            var response = await client.PostAsync("/Cliente/Novo", new StringContent(JsonConvert.SerializeObject(cliente), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task InsertClienteErro()
        {
            Cliente cliente = new Cliente
            {
                DataNascimento = new DateTime(1999, 11, 10),
                Email = "marcus.steadfast@hotmail.com",
                Endereco = "Rua dos Melões, 69",
                Cpf = "333.333.333-33",
                Id = 0,
                Sexo = "M",
                Telefone = "(21) 1111-1111"
            };

            var client = _factory.CreateClient();

            var response = await client.PostAsync("/Cliente/Novo", new StringContent(JsonConvert.SerializeObject(cliente), Encoding.UTF8, "application/json"));
            string msg = JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result);

            Assert.True(response.StatusCode == HttpStatusCode.BadRequest);
            Assert.Equal(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Nome"), msg);
        }

        [Fact]
        public async Task UpdateCliente()
        {
            //Suposição: Há um registro pre-existente.
            Cliente cliente = new Cliente
            {
                Nome = "Marcus Steadfast",
                DataNascimento = new DateTime(1999, 11, 10),
                Email = "marcus.steadfast@hotmail.com",
                Endereco = "Rua dos Melões, 69",
                Cpf = "333.333.333-33",
                Id = 1,
                Sexo = "M",
                Telefone = "(21) 1111-1111"
            };

            var client = _factory.CreateClient();

            var response = await client.PostAsync("/Cliente/Edita", new StringContent(JsonConvert.SerializeObject(cliente), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task UpdateClienteErro()
        {
            Cliente cliente = new Cliente
            {
                Nome = "Marcus Steadfast",
                DataNascimento = new DateTime(1999, 11, 10),
                Email = "marcus.steadfast@hotmail.com",
                Endereco = "Rua dos Melões, 69",
                Cpf = "333.333.333-33",
                Id = -1,
                Sexo = "M",
                Telefone = "(21) 1111-1111"
            };

            var client = _factory.CreateClient();

            var response = await client.PostAsync("/Cliente/Edita", new StringContent(JsonConvert.SerializeObject(cliente), Encoding.UTF8, "application/json"));
            string msg = JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result);

            Assert.True(response.StatusCode == HttpStatusCode.BadRequest);
            Assert.Equal(erroModel.GeraErroModel(ErroModel.ERRO_MODEL.ERRO_INVALIDO, "Código"), msg);
        }
        
        [Fact]
        public async Task DeleteCliente()
        {
            //Suposição: Há um registro pre-existente

            var client = _factory.CreateClient();

            var response = await client.DeleteAsync("/Cliente/Remove/1");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task DeleteClienteErro()
        {
            var client = _factory.CreateClient();

            var response = await client.DeleteAsync("/Cliente/Remove/0");

            string msg = JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result);

            Assert.True(response.StatusCode == HttpStatusCode.Gone);
            Assert.Equal(erroDatabase.GeraErroDatabase(ErroDatabase.ERRO_DATABASE.ERRO_DELETAR_NAO_EXISTE), msg);
        }
    }
}
