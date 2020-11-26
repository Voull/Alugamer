using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using Xunit;
using BrowserStack;

namespace Alugamer.Testes.AutomatedUITests
{
    public class AutomatedUICliente /*: IDisposable*/
    {
        //private readonly IWebDriver _webDriver;

        //public AutomatedUICliente()
        //{
        //    _webDriver = new FirefoxDriver();
        //}

        //public void Dispose()
        //{
        //    _webDriver.Quit();
        //    _webDriver.Dispose();
        //}

        [Fact]
        public void TesteNovo()
        {
            Assert.True(Environment.GetEnvironmentVariable("BROWSERSTACK_KEY") != null);
        }
    }
}
