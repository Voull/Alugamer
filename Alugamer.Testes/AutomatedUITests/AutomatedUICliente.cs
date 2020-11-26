using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using Xunit;
using BrowserStack;
using Alugamer.Testes.Utils;
using OpenQA.Selenium.Remote;

namespace Alugamer.Testes.AutomatedUITests
{
    public class AutomatedUICliente
    {
        BrowserStackLocal localConfig;
        RemoteWebDriver driver;

        public AutomatedUICliente()
        {
            localConfig = new BrowserStackLocal();

            driver = new RemoteWebDriver(new Uri("http://voull1.browserstack.com"), localConfig.capabilities);
        }

        [Fact]
        public void TesteNovo()
        {
            Assert.Equal("Home Page - Alugamer", driver.Title);
        }
    }
}
