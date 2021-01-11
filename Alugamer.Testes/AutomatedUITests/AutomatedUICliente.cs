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
using Xunit.Sdk;
using OpenQA.Selenium.Support.UI;

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
            #if !TRAVIS
            local = new Local();
            List<KeyValuePair<string, string>> bsLocalArgs = new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("key", Environment.GetEnvironmentVariable("BROWSERSTACK_KEY")),
                new KeyValuePair<string, string>("forcelocal", "true")
            };
            #endif
            try
            {
                #if !TRAVIS
                local.start(bsLocalArgs);
                #endif
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
           #if !TRAVIS
            local.stop();
          #endif

        }


        [Fact]
        public void TesteNovo()
        {
            driver.Url = "https://localhost:5001/cliente";
            driver.Navigate();
            try
            {
                Assert.Equal("Gestão de Clientes - Alugamer", driver.Title);
                driver.FindElementById("btnNovoCli").Click();

                driver.FindElementById("nomeCli").SendKeys("Gabriel Teste");
                driver.FindElementById("emailCli").SendKeys("UITeste@teste.com");
                driver.FindElementById("celCli").SendKeys("11998024793");
                driver.FindElementById("endCli").SendKeys("Rua das Laranjas, 444");
                var dropDownSexo = driver.FindElementById("sexoCli");
                var selectSexo = new SelectElement(dropDownSexo);
                selectSexo.SelectByText("Masculino");
                driver.FindElementById("cpfCli").SendKeys("47227056821");

                driver.FindElementById("btnSalvar").Click();

                Assert.Equal("Cliente salvo com sucesso!", new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(d => d.SwitchTo().Alert().Text));

            }
            catch(EqualException e)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"failed\", \"reason\": \"" + $"Esperado: {e.Expected}\nEncontrado:{e.Actual}" + "\"}}");
                throw e;
            }
            catch(TrueException e)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"failed\", \"reason\": \"" + $"Esperado: {e.Expected}\nEncontrado:{e.Actual}" + "\"}}");
                throw e;
            }
            catch(WebDriverException e)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"failed\", \"reason\": \"" + $"{e.Message}" + "\"}}");
                throw e;
            }
            
            ((IJavaScriptExecutor)driver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"passed\", \"reason\": \" Sucesso!\"}}");

        }
    }
}
