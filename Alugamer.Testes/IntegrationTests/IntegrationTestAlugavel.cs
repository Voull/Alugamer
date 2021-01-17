using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Alugamer.Models;
using Alugamer.Testes.Utils;
using Alugamer.Utils;
using Alugamer.Validations;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace Alugamer.Testes.IntegrationTests
{
    [TestCaseOrderer("Alugamer.Testes.Utils.PriorityOrderer", "Alugamer.Testes")]
    public class IntegrationTestAlugavel : IntegrationTestBase
    {

        private readonly ErroModel erroModel;
        private readonly ErroDatabase erroDatabase;
        private readonly AlugavelValidation alugavelValidation;

        public IntegrationTestAlugavel(WebApplicationFactory<Startup> factory) : base(factory)
        {
            erroModel = new ErroModel();
            erroDatabase = new ErroDatabase();
            alugavelValidation = new AlugavelValidation();
        }

        [Fact, TestPriority(-1)]
        public async Task GetAlugavel()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/Alugavel/Busca/1");

            response.EnsureSuccessStatusCode();
            Alugavel alugavel = JsonConvert.DeserializeObject<Alugavel>(response.Content.ReadAsStringAsync().Result);
            List<String> validacao = alugavelValidation.validar(alugavel);

            Assert.True(validacao.Count == 0);
        }

        [Fact]
        public async Task InsertAlugavel()
        {
            Alugavel alugavel = new Alugavel
            {
                Id = 0,
                Nome = "Nintendo Switch",
                Descricao = "Console Nintendo Switch Versão Azul/Vermelho",
                Quantidade = 10,
                Valor_compra = 2500,
                Valor_aluguel = 150,
                IdCategoria = 1
            };

            var client = _factory.CreateClient();

            var response = await client.PostAsync("/Alugavel/Novo", new StringContent(JsonConvert.SerializeObject(alugavel), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task InsertAlugavelErro()
        {
            Alugavel alugavel = new Alugavel
            {
                Id = 0,
                Descricao = "Console Nintendo Switch Versão Azul/Vermelho",
                Quantidade = 10,
                Valor_compra = 2500,
                Valor_aluguel = 150,
                IdCategoria = 1
            };

            var client = _factory.CreateClient();

            var response = await client.PostAsync("/Alugavel/Novo", new StringContent(JsonConvert.SerializeObject(alugavel), Encoding.UTF8, "application/json"));
            string msg = JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result);

            Assert.True(response.StatusCode == HttpStatusCode.BadRequest);
            Assert.Equal(erroModel.GeraErroModel(ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Nome"), msg);
        }

        [Fact]
        public async Task UpdateAlugavel()
        {
            //Suposição: Há um registro pre-existente.
            Alugavel alugavel = new Alugavel
            {
                Id = 1,
                Nome = "Outro Item",
                Descricao = "Certamente editado",
                Quantidade = 1,
                Valor_compra = 9000,
                Valor_aluguel = 300,
                IdCategoria = 2
            };

            var client = _factory.CreateClient();

            var response = await client.PostAsync("/Alugavel/Edita", new StringContent(JsonConvert.SerializeObject(alugavel), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task UpdateAlugavelErro()
        {
            Alugavel alugavel = new Alugavel
            {
                Id = -1,
                Nome = "Outro Item",
                Descricao = "Certamente editado",
                Quantidade = 1,
                Valor_compra = 9000,
                Valor_aluguel = 300,
                IdCategoria = 2
            };

            var client = _factory.CreateClient();

            var response = await client.PostAsync("/Alugavel/Edita", new StringContent(JsonConvert.SerializeObject(alugavel), Encoding.UTF8, "application/json"));
            string msg = JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result);

            Assert.True(response.StatusCode == HttpStatusCode.BadRequest);
            Assert.Equal(erroModel.GeraErroModel(ERRO_MODEL.ERRO_CAMPO_OBRIGATORIO, "Código"), msg);
        }

        [Fact]
        public async Task DeleteAlugavel()
        {
            //Suposição: Há um registro pre-existente

            var client = _factory.CreateClient();

            var response = await client.DeleteAsync("/Alugavel/Remove/1");

            response.EnsureSuccessStatusCode();
        }

        //[Fact]
        //public async Task DeleteAlugavelErro()
        //{
        //    var client = _factory.CreateClient();

        //    var response = await client.DeleteAsync("/Alugavel/Remove/0");

        //    string msg = JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result);


        //    Assert.True(response.StatusCode == HttpStatusCode.Gone);
        //    Assert.Equal(erroDatabase.GeraErroDatabase(ERRO_DATABASE.ERRO_DELETAR_NAO_EXISTE), msg);
        //}
    }
}
