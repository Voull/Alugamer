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
using System.Net.Http;
using System.Net;
using System.Threading;
using waitHelpers = SeleniumExtras.WaitHelpers;

namespace Alugamer.Testes.AutomatedUITests
{
    public class AutomatedUICliente : IDisposable
    {
        BrowserStackLocal localConfig;
        RemoteWebDriver driver;
        //IWebDriver driver;
#if !TRAVIS
        AutomatedUIProgram startup;
#endif
        Local local;
        BrowserStackStatus browserStackStatus;

        public AutomatedUICliente()
        {
#if !TRAVIS
            startup = new AutomatedUIProgram();
#endif
#if TRAVIS
            AutomatedUIProgram.StartupApplication();
#endif
            localConfig = new BrowserStackLocal();
            browserStackStatus = new BrowserStackStatus();
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

            if(driver != null)
            {
                driver.Close();
                driver.Dispose();
            }
#if !TRAVIS
            startup.Dispose();
            local.stop();
#endif

        }


        [Fact]
        public void NovoCliente()
        {
            driver.Url = "https://localhost:5001/cliente";
            driver.Navigate();
            try
            {
                Assert.Equal("Gestão de Clientes - Alugamer", driver.Title);
                driver.FindElementById("btnNovoCli").Click();

                driver.FindElementById("nomeCli").SendKeys("Gabriel Teste");
                driver.FindElementById("emailCli").SendKeys("UITeste@teste.com");
                foreach(char digito in "11998024793")
                {
                    driver.FindElementById("celCli").SendKeys(digito.ToString());
                }
                //driver.FindElementById("celCli").SendKeys("11998024793");
                driver.FindElementById("endCli").SendKeys("Rua das Laranjas, 444");
                var dropDownSexo = driver.FindElementById("sexoCli");
                var selectSexo = new SelectElement(dropDownSexo);
                selectSexo.SelectByText("Masculino");

                foreach (char digito in "47227056821")
                {
                    driver.FindElementById("cpfCli").SendKeys(digito.ToString());
                }
                driver.FindElementById("dataNascCli").SendKeys("27071999");
                //driver.FindElementById("cpfCli").SendKeys("47227056821");

                driver.FindElementById("btnSalvar").Click();

                Assert.Equal("Cliente salvo com sucesso!", new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(d => d.SwitchTo().Alert().Text));

            }
            catch(EqualException e)
            {
                browserStackStatus.UpdateStatus(driver.SessionId.ToString(), false, $"Esperado: {e.Expected}\nEncontrado:{e.Actual}");

                //((IJavaScriptExecutor)driver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"failed\", \"reason\": \"" + $"Esperado: {e.Expected}\nEncontrado:{e.Actual}" + "\"}}");
                throw e;
            }
            catch(TrueException e)
            {
                browserStackStatus.UpdateStatus(driver.SessionId.ToString(), false, $"Esperado: {e.Expected}\nEncontrado:{e.Actual}");

                //((IJavaScriptExecutor)driver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"failed\", \"reason\": \"" + $"Esperado: {e.Expected}\nEncontrado:{e.Actual}" + "\"}}");
                throw e;
            }
            catch(WebDriverException e)
            {
                browserStackStatus.UpdateStatus(driver.SessionId.ToString(), false, $"{e.Message}");

                //((IJavaScriptExecutor)driver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"failed\", \"reason\": \"" + $"{e.Message}" + "\"}}");
                throw e;
            }

            browserStackStatus.UpdateStatus(driver.SessionId.ToString(), true, "Sucesso!");

            //((IJavaScriptExecutor)driver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"passed\", \"reason\": \" Sucesso!\"}}");

        }

        [Fact]
        public void EditaCliente()
        {
            driver.Url = "https://localhost:5001/cliente";
            driver.Navigate();
            try
            {
                Assert.Equal("Gestão de Clientes - Alugamer", driver.Title);
                var dropDownBusca = driver.FindElementById("clienteCli");
                dropDownBusca.Click();
                var selectBusca = new SelectElement(dropDownBusca);
                selectBusca.SelectByText("Joaquim");

                driver.FindElementById("btnBuscaCli").Click();
                waitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("nomeCli"));

                driver.FindElementById("nomeCli").Clear();
                driver.FindElementById("nomeCli").SendKeys("Gabriel Teste");

                driver.FindElementById("emailCli").Clear();
                driver.FindElementById("emailCli").SendKeys("UITeste@teste.com");

                driver.FindElementById("btnSalvar").Click();

                Assert.Equal("Cliente salvo com sucesso!", new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(d => d.SwitchTo().Alert().Text));

            }
            catch (EqualException e)
            {
                browserStackStatus.UpdateStatus(driver.SessionId.ToString(), false, $"Esperado: {e.Expected}\nEncontrado:{e.Actual}");

                //((IJavaScriptExecutor)driver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"failed\", \"reason\": \"" + $"Esperado: {e.Expected}\nEncontrado:{e.Actual}" + "\"}}");
                throw e;
            }
            catch (TrueException e)
            {
                browserStackStatus.UpdateStatus(driver.SessionId.ToString(), false, $"Esperado: {e.Expected}\nEncontrado:{e.Actual}");

                //((IJavaScriptExecutor)driver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"failed\", \"reason\": \"" + $"Esperado: {e.Expected}\nEncontrado:{e.Actual}" + "\"}}");
                throw e;
            }
            catch (WebDriverException e)
            {
                browserStackStatus.UpdateStatus(driver.SessionId.ToString(), false, $"{e.Message}");

                //((IJavaScriptExecutor)driver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"failed\", \"reason\": \"" + $"{e.Message}" + "\"}}");
                throw e;
            }

            browserStackStatus.UpdateStatus(driver.SessionId.ToString(), true, "Sucesso!");

            //((IJavaScriptExecutor)driver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"passed\", \"reason\": \" Sucesso!\"}}");

        }

        [Fact]
        public void DeletaCliente()
        {
            driver.Url = "https://localhost:5001/cliente";
            driver.Navigate();
            try
            {
                Assert.Equal("Gestão de Clientes - Alugamer", driver.Title);
                var dropDownBusca = driver.FindElementById("clienteCli");
                dropDownBusca.Click();

                var selectBusca = new SelectElement(dropDownBusca);
                selectBusca.SelectByText("Joaquim");

                driver.FindElementById("btnBuscaCli").Click();

                waitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("nomeCli"));
                driver.FindElementById("btnExcluirCli").Click();

                driver.SwitchTo().Alert().Accept();

                Assert.Equal("Cliente removido com sucesso!", new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(d => d.SwitchTo().Alert().Text));
            }
            catch (EqualException e)
            {
                browserStackStatus.UpdateStatus(driver.SessionId.ToString(), false, $"Esperado: {e.Expected}\nEncontrado:{e.Actual}");

                //((IJavaScriptExecutor)driver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"failed\", \"reason\": \"" + $"Esperado: {e.Expected}\nEncontrado:{e.Actual}" + "\"}}");
                throw e;
            }
            catch (TrueException e)
            {
                browserStackStatus.UpdateStatus(driver.SessionId.ToString(), false, $"Esperado: {e.Expected}\nEncontrado:{e.Actual}");

                //((IJavaScriptExecutor)driver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"failed\", \"reason\": \"" + $"Esperado: {e.Expected}\nEncontrado:{e.Actual}" + "\"}}");
                throw e;
            }
            catch (WebDriverException e)
            {
                browserStackStatus.UpdateStatus(driver.SessionId.ToString(), false, $"{e.Message}");

                //((IJavaScriptExecutor)driver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"failed\", \"reason\": \"" + $"{e.Message}" + "\"}}");
                throw e;
            }

            browserStackStatus.UpdateStatus(driver.SessionId.ToString(), true, "Sucesso!");

            //((IJavaScriptExecutor)driver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"passed\", \"reason\": \" Sucesso!\"}}");

        }

    }
    
}

