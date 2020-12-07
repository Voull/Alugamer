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
            if (pasta != null) {
                foreach (DirectoryInfo dir in pasta.GetDirectories())
                {
                    if (dir.Name.Equals("Alugamer"))
                    {
                        pasta = dir;
                        break;
                    }
                }
            }
            else
            {
                string msg = "";
                foreach(string dir in Directory.GetDirectories(pasta.FullName))
                {
                    msg += dir + "\n";
                }

                Environment.FailFast($"Pasta do Projeto não encontrada! Caminho: {pasta.FullName} \n Dir: {msg}");
            }


            ProcessStartInfo prStartInfo = new ProcessStartInfo("dotnet")
            {
                Arguments = $"run --project {pasta.FullName} -c TRAVIS",
                UseShellExecute = false,
                RedirectStandardOutput = true
            };

            pr = Process.Start(prStartInfo);
            Thread.Sleep(10000);
            if (pr.HasExited)
            {
                Environment.FailFast("Erro na Inicialização do Projeto! \n Log do Console:\n" + pr.StandardOutput.ReadToEnd());
            }
        }
        public void Dispose()
        {
            if (pr != null)
                pr.Kill();
        }
    }
}
