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
            capabilities = new ChromeOptions();
            capabilities.BrowserVersion = "latest";
            Dictionary<string, object> browserstackOptions = new Dictionary<string, object>();
            browserstackOptions.Add("os", "Windows");
            browserstackOptions.Add("osVersion", "10");
            browserstackOptions.Add("local", "true");
            browserstackOptions.Add("seleniumVersion", "3.141.0");
            browserstackOptions.Add("userName", "voull1");
            browserstackOptions.Add("accessKey", Environment.GetEnvironmentVariable("BROWSERSTACK_KEY"));
            capabilities.AddAdditionalCapability("bstack:options", browserstackOptions);

        }
    }
}
