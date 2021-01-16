using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Xunit;
using Xunit.Sdk;
using waitHelpers = SeleniumExtras.WaitHelpers;

namespace Alugamer.Testes.AutomatedUITests
{
    public class AutomatedUIAlugavel : AutomatedUIBase
    {
        [Fact]
        public void NovoAlugavel()
        {
            driver.Url = "https://localhost:5001/alugavel";
            driver.Navigate();
            try
            {
                Assert.Equal("Gestão de Estoque - Alugamer", driver.Title);
                driver.FindElementById("btnNovoCli").Click();

                driver.FindElementById("nomeAluga").SendKeys("Playstation 5");
                driver.FindElementById("descricaoAluga").SendKeys("Console Playstation 5 da Sony");

                var dropDownSexo = driver.FindElementById("categoriaAluga");
                var selectSexo = new SelectElement(dropDownSexo);
                selectSexo.SelectByText("Console");

                driver.FindElementById("valorCompraAluga").SendKeys("5000,00");
                driver.FindElementById("valorAluga").SendKeys("100,00");
                driver.FindElementById("qtdAluga").SendKeys("20");

                driver.FindElementById("btnSalvar").Click();

                Assert.Equal("Item salvo com sucesso!", new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(d => d.SwitchTo().Alert().Text));

            }
            catch (EqualException e)
            {
                browserStackStatus.UpdateStatus(driver.SessionId.ToString(), false, $"Esperado: {e.Expected}\nEncontrado:{e.Actual}");

                throw e;
            }
            catch (TrueException e)
            {
                browserStackStatus.UpdateStatus(driver.SessionId.ToString(), false, $"Esperado: {e.Expected}\nEncontrado:{e.Actual}");

                throw e;
            }
            catch (WebDriverException e)
            {
                browserStackStatus.UpdateStatus(driver.SessionId.ToString(), false, $"{e.Message}");

                throw e;
            }

            browserStackStatus.UpdateStatus(driver.SessionId.ToString(), true, "Sucesso!");

        }

        [Fact]
        public void EditaAlugavel()
        {
            driver.Url = "https://localhost:5001/cliente";
            driver.Navigate();
            try
            {
                Assert.Equal("Gestão de Estoque - Alugamer", driver.Title);

                var dropDownBusca = driver.FindElementById("AlugavelAlu");
                dropDownBusca.Click();

                var selectBusca = new SelectElement(dropDownBusca);
                selectBusca.SelectByText("Nintendo Switch", true);

                driver.FindElementById("nomeAluga").SendKeys("Playstation 5");
                driver.FindElementById("descricaoAluga").SendKeys("Console Playstation 5 da Sony");

                var dropDownSexo = driver.FindElementById("categoriaAluga");
                var selectSexo = new SelectElement(dropDownSexo);
                selectSexo.SelectByText("Console");

                driver.FindElementById("valorCompraAluga").SendKeys("5000,00");
                driver.FindElementById("valorAluga").SendKeys("100,00");
                driver.FindElementById("qtdAluga").SendKeys("20");

                driver.FindElementById("btnSalvar").Click();

                Assert.Equal("Item salvo com sucesso!", new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(d => d.SwitchTo().Alert().Text));

            }
            catch (EqualException e)
            {
                browserStackStatus.UpdateStatus(driver.SessionId.ToString(), false, $"Esperado: {e.Expected}\nEncontrado:{e.Actual}");

                throw e;
            }
            catch (TrueException e)
            {
                browserStackStatus.UpdateStatus(driver.SessionId.ToString(), false, $"Esperado: {e.Expected}\nEncontrado:{e.Actual}");

                throw e;
            }
            catch (WebDriverException e)
            {
                browserStackStatus.UpdateStatus(driver.SessionId.ToString(), false, $"{e.Message}");

                throw e;
            }

            browserStackStatus.UpdateStatus(driver.SessionId.ToString(), true, "Sucesso!");


        }

        [Fact]
        public void DeletaAlugavel()
        {
            driver.Url = "https://localhost:5001/cliente";
            driver.Navigate();
            try
            {
                Assert.Equal("Gestão de Estoque - Alugamer", driver.Title);

                var dropDownBusca = driver.FindElementById("AlugavelAlu");
                dropDownBusca.Click();

                var selectBusca = new SelectElement(dropDownBusca);
                selectBusca.SelectByText("Nintendo Switch", true);

                driver.FindElementById("btnExcluirAluga").Click();

                Assert.Equal("Item removido com sucesso!", new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(d => d.SwitchTo().Alert().Text));
            }
            catch (EqualException e)
            {
                browserStackStatus.UpdateStatus(driver.SessionId.ToString(), false, $"Esperado: {e.Expected}\nEncontrado:{e.Actual}");

                throw e;
            }
            catch (TrueException e)
            {
                browserStackStatus.UpdateStatus(driver.SessionId.ToString(), false, $"Esperado: {e.Expected}\nEncontrado:{e.Actual}");

                throw e;
            }
            catch (WebDriverException e)
            {
                browserStackStatus.UpdateStatus(driver.SessionId.ToString(), false, $"{e.Message}");

                throw e;
            }

            browserStackStatus.UpdateStatus(driver.SessionId.ToString(), true, "Sucesso!");

        }

    }

}

