using System;
using System.Collections.Generic;
using System.Text;
using BrowserStack;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace Alugamer.Testes.Utils
{
    public class BrowserStackLocal
    {
        public readonly ChromeOptions capabilities;

        public BrowserStackLocal()
        {
            capabilities = new ChromeOptions
            {
                AcceptInsecureCertificates = true
            };
            capabilities.AddAdditionalCapability("os", "Windows", true);
            capabilities.AddAdditionalCapability("os_version", "10", true);
            capabilities.AddAdditionalCapability("browser", "Chrome", true);
            capabilities.AddAdditionalCapability("browser_version", "latest", true);
            //capabilities.AddAdditionalCapability("browserstack.local", "true", true);
            capabilities.AddAdditionalCapability("browserstack.debug", "true", true);
            capabilities.AddAdditionalCapability("browserstack.selenium_version", "3.141.0", true);
            capabilities.AddAdditionalCapability("browserstack.user", "voull1", true);
            capabilities.AddAdditionalCapability("browserstack.key", Environment.GetEnvironmentVariable("BROWSERSTACK_KEY"), true);


        }
    }
}
