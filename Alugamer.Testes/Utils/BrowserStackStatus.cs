using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Alugamer.Testes.Utils
{
    public class BrowserStackStatus
    {

        public void UpdateStatus(string sessionId, bool success, string message)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("PUT"), $"https://api.browserstack.com/automate/sessions/{sessionId}.json"))
                {
                    var base64authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes($"voull1:{Environment.GetEnvironmentVariable("BROWSERSTACK_KEY")}"));
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {base64authorization}");
                    dynamic content = new JObject();
                    content.status = success ? "passed" : "failed";
                    content.reason = message;

                    request.Content = new StringContent(JsonConvert.SerializeObject(content));
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = httpClient.SendAsync(request).Result;
                }
            }
        }
    }
}
