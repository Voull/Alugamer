using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Alugamer.Testes.IntegrationTests
{
    public class IntegrationTestsGeneral : IntegrationTestBase
    {
        public IntegrationTestsGeneral(WebApplicationFactory<Startup> factory) : base(factory)
        {

        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Alugavel")]
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
    }
}
