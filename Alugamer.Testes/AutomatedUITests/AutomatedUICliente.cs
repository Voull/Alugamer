using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using Xunit;

namespace Alugamer.Testes.AutomatedUITests
{
    public class AutomatedUICliente : IDisposable
    {
        private readonly IWebDriver _webDriver;

        public AutomatedUICliente()
        {
            _webDriver = new FirefoxDriver();
        }

        public void Dispose()
        {
            _webDriver.Quit();
            _webDriver.Dispose();
        }

        [Fact]
        public void TesteNovo()
        {
            _webDriver.Navigate()
                .GoToUrl("https://localhost:44392/Cliente");

            Assert.Equal("Cliente", _webDriver.Title);
        }
    }
}
