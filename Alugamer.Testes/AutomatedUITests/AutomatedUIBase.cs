using System;
using System.Collections.Generic;
using System.Text;
using Alugamer.Testes.Utils;
using BrowserStack;
using OpenQA.Selenium.Remote;


namespace Alugamer.Testes.AutomatedUITests
{
    public abstract class AutomatedUIBase : IDisposable
    {
        protected BrowserStackLocal localConfig;
        protected RemoteWebDriver driver;
        //IWebDriver driver;

#if !TRAVIS
        protected AutomatedUIProgram startup;
#endif
        protected Local local;
        protected BrowserStackStatus browserStackStatus;

        public AutomatedUIBase()
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
            catch (Exception e)
            {
                Dispose();
                throw e;
            }
        }

        public void Dispose()
        {

            if (driver != null)
            {
                driver.Close();
                driver.Dispose();
            }
#if !TRAVIS
            startup.Dispose();
            local.stop();
#endif
        }

    }
}

