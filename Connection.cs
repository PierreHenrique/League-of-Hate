using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// https://github.com/molenzwiebel/Mimic/blob/master/conduit

namespace League_of_Hate
{
    public class Connection
    {
        private static HttpClient _httpClient;
        private static string _clientPort;
        private static bool _connected = false;

        public Connection()
        {
            try
            {
                _httpClient = new HttpClient(new HttpClientHandler
                {
                    SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls,
                    ServerCertificateCustomValidationCallback = (a, b, c, d) => true
                });
            }
            catch
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                _httpClient = new HttpClient(new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (a, b, c, d) => true
                });
            }
        }

        public async Task<bool> TryConnect()
        {
            if (!_connected)
            {
                Debug.Write("Aguardando o client inicializar", true);

                var token = Token();

                if (token != null)
                {
                    var byteArray = Encoding.ASCII.GetBytes($"riot:{token.Item1}");
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                    _clientPort = token.Item2;
                    _connected = true;

                    return true;
                }

                var response = await Task.Delay(2000).ContinueWith(a => TryConnect());

                return response.Result;
            }

            return false;
        }
        
        private Tuple<string, string> Token()
        {
            foreach (var p in Process.GetProcessesByName("LeagueClientUx"))
            {
                using (var mos = new ManagementObjectSearcher($"SELECT CommandLine FROM Win32_Process WHERE ProcessId = {p.Id}"))
                using (var moc = mos.Get())
                {
                    var commandLine = (string)moc.OfType<ManagementObject>().First()["CommandLine"];

                    if (commandLine == null)
                    {
                        if (!Administrator.IsAdmin())
                        {
                            Console.WriteLine("O aplicativo tem que ser executado como administrador");
                            Administrator.Elevate();
                        }
                    }

                    try
                    {
                        var authToken = new Regex("\"--remoting-auth-token=(.+?)\"").Match(commandLine).Groups[1].Value;
                        var port = new Regex("\"--app-port=(\\d+?)\"").Match(commandLine).Groups[1].Value;
                        
                        return new Tuple<string, string>
                        (
                            authToken,
                            port
                        );
                    }
                    catch (Exception e)
                    {
                        Debug.Write($"[Error] - {e.Message}");
                    }
                }
            }

            return null;
        }

        public async Task<dynamic> Get(string url)
        {
            var res = await _httpClient.GetAsync($"https://127.0.0.1:{_clientPort}/{url}");
            var stringContent = await res.Content.ReadAsStringAsync();

            if (res.StatusCode == HttpStatusCode.NotFound) return null;

            return (JObject)JsonConvert.DeserializeObject(stringContent);
        }

        public async Task<dynamic> Post(string url, object data)
        {
            var buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var res = await _httpClient.PostAsync($"https://127.0.0.1:{_clientPort}/{url}", byteContent);
            var stringContent = await res.Content.ReadAsStringAsync();

            if (res.StatusCode == HttpStatusCode.NotFound) return null;

            return (JObject)JsonConvert.DeserializeObject(stringContent);
        }

        public async Task<dynamic> Put(string url, object data)
        {
            var buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var res = await _httpClient.PutAsync($"https://127.0.0.1:{_clientPort}/{url}", byteContent);
            var stringContent = await res.Content.ReadAsStringAsync();

            if (res.StatusCode == HttpStatusCode.NotFound) return null;

            return (JObject)JsonConvert.DeserializeObject(stringContent);
        }
    }
}
