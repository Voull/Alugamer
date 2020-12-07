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
        BrowserStackLocal localConfig;
        RemoteWebDriver driver;
        //IWebDriver driver;
        AutomatedUIProgram startup;
        Local local;

        public AutomatedUICliente()
        {
            startup = new AutomatedUIProgram();
            localConfig = new BrowserStackLocal();

            local = new Local();
            List<KeyValuePair<string, string>> bsLocalArgs = new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("key", Environment.GetEnvironmentVariable("BROWSERSTACK_KEY")),
                new KeyValuePair<string, string>("forcelocal", "true")
            };

            try
            {
                local.start(bsLocalArgs);
                driver = new RemoteWebDriver(new Uri("https://hub-cloud.browserstack.com/wd/hub/"), localConfig.capabilities);
            }
            catch(Exception e)
            {
                Dispose();
                throw e;
            }
        }

        public void Dispose()
        {
            startup.Dispose();
            
            if(driver != null)
            {
                driver.Close();
                driver.Dispose();
            }

            if (local.isRunning())
            {
                local.stop();
            }
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
