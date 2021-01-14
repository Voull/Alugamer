using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Runners;

namespace Alugamer.Testes.AutomatedUITests
{
#if TRAVIS
    public static class AutomatedUIProgram
    {

        private static Process pr;
#endif
#if !TRAVIS
    public class AutomatedUIProgram
    {
        private Process pr;

        public AutomatedUIProgram()
        {
            StartupApplication();
        }
#endif

#if !TRAVIS
        public void StartupApplication()
        {
#endif
#if TRAVIS
        public static void StartupApplication()
        {
            if (pr != null)
            {
                ResetDatabase();
                return;
            }   
#endif
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
                Arguments = $"run --project {pasta.FullName} -c \"TRAVIS\"",
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
#if TRAVIS
        public static void Dispose()
#endif
#if !TRAVIS
        public void Dispose()
#endif
        {
            if (pr != null)
                pr.Kill();
        }
#if TRAVIS
        public static void ResetDatabase()
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("POST"), "https://localhost:5001/Reset");
            var response = client.SendAsync(request).Result;
        }
#endif
    }
}
