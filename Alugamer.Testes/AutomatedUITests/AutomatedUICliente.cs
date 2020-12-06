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
    public class AutomatedUICliente : IDisposable
    {
        //BrowserStackLocal localConfig;
        //RemoteWebDriver driver;
        IWebDriver driver;
        AutomatedUIProgram startup;

        public AutomatedUICliente()
        {
            startup = new AutomatedUIProgram();
            //localConfig = new BrowserStackLocal();

            //driver = new RemoteWebDriver(new Uri("https://localhost:5001"), localConfig.capabilities);
            var AAAA = new FirefoxOptions
            {
                AcceptInsecureCertificates = true
            };
            AAAA.AddArgument("-headless");
            driver = new FirefoxDriver(AAAA);
        }

        public void Dispose()
        {
            startup.Dispose();
            driver.Close();
            driver.Dispose();
        }


        [Fact]
        public void TesteNovo()
        {
            driver.Url = "https://localhost:5001/cliente";
            driver.Navigate();
            
            Assert.Equal("Gestão de Clientes - Alugamer", driver.Title);
        }
    }
}
