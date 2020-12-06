using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Runners;

namespace Alugamer.Testes.AutomatedUITests
{
    public class AutomatedUIProgram : IDisposable
    {
        Process pr;

        public AutomatedUIProgram()
        {
            StartupApplication();    
        }

        private void StartupApplication()
        {
            DirectoryInfo pasta = new DirectoryInfo(Environment.CurrentDirectory);

            while (pasta != null && !pasta.Name.Equals("Alugamer"))
                pasta = pasta.Parent;
            if (pasta != null && Directory.Exists(pasta.FullName + "\\Alugamer"))
                pasta = new DirectoryInfo(pasta.FullName + "\\Alugamer");
            else
                Environment.FailFast("Pasta do Projeto não encontrada!");

            ProcessStartInfo prStartInfo = new ProcessStartInfo("dotnet")
            {
                Arguments = $"run --project {pasta.FullName}",
                UseShellExecute = false,
                RedirectStandardOutput = true
            };

            pr = Process.Start(prStartInfo);

            Thread.Sleep(10000);
        }

        public void Dispose()
        {
            if (pr != null)
                pr.Kill();
        }
    }
}
